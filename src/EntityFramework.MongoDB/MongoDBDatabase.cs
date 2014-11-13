using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Data.Entity.ChangeTracking;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.MongoDB.Serialization;
using Microsoft.Data.Entity.MongoDB.Utilities;
using Microsoft.Data.Entity.Utilities;
using Microsoft.Framework.Logging;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Core.Bindings;
using MongoDB.Driver.Core.Clusters;
using MongoDB.Driver.Core.Configuration;
using MongoDB.Driver.Core.Operations;
using MongoDB.Driver.Core.WireProtocol.Messages.Encoders;

namespace Microsoft.Data.Entity.MongoDB
{
    public class MongoDBDatabase : Database
    {
        private readonly LazyRef<ICluster> _cluster;
        private readonly IBsonSerializerRegistry _serializerRegistry;

        /// <summary>
        ///     This constructor is intended only for use when creating test doubles that will override members
        ///     with mocked or faked behavior. Use of this constructor for other purposes may result in unexpected
        ///     behavior including but not limited to throwing <see cref="NullReferenceException" />.
        /// </summary>
        protected MongoDBDatabase()
        {
        }

        public MongoDBDatabase(
            [NotNull] LazyRef<IModel> model,
            [NotNull] MongoDBDataStoreCreator dataStoreCreator,
            [NotNull] MongoDBConnection connection,
            [NotNull] ILoggerFactory loggerFactory)
            : base(model, dataStoreCreator, connection, loggerFactory)
        {
            // this certainly shouldn't be done here and should be cached...
            _cluster = new LazyRef<ICluster>(() => ClusterCache.GetOrCreate(connection.ConnectionString));

            // property a better way to handle this
            _serializerRegistry = new EFSerializerRegistry(model);
        }

        public new virtual MongoDBConnection Connection
        {
            get { return (MongoDBConnection)base.Connection; }
        }

        public virtual ICluster GetUnderlyingCluster()
        {
            return _cluster.Value;
        }

        public virtual async Task<int> SaveChangesAsync(
            [NotNull] IReadOnlyList<StateEntry> stateEntries,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            Check.NotNull(stateEntries, "stateEntries");

            cancellationToken.ThrowIfCancellationRequested();

            var perCollection = stateEntries.GroupBy(x => new CollectionNamespace(new DatabaseNamespace(x.EntityType.MongoDB().Database), x.EntityType.MongoDB().Collection));

            var databaseNamespace = new DatabaseNamespace("test");
            var entitiesProcessed = 0;

            using (var binding = new WritableServerBinding(_cluster.Value))
            {
                foreach (var stateEntryGroup in perCollection)
                {
                    var writeRequests = CreateWriteRequests(stateEntries);

                    var operation = new BulkMixedWriteOperation(
                        stateEntryGroup.Key,
                        writeRequests,
                        new MessageEncoderSettings());

                    await operation.ExecuteAsync(binding, cancellationToken);
                }
            }

            return entitiesProcessed;
        }

        private IEnumerable<WriteRequest> CreateWriteRequests(IReadOnlyList<StateEntry> stateEntries)
        {
            var list = new List<WriteRequest>();
            foreach (var entry in stateEntries)
            {
                switch (entry.EntityState)
                {
                    case EntityState.Added:
                        list.Add(CreateInsertRequest(entry));
                        break;
                    case EntityState.Modified:
                        list.Add(CreateUpdateRequest(entry));
                        break;
                    case EntityState.Deleted:
                        list.Add(CreateDeleteRequest(entry));
                        break;
                    case EntityState.Unknown:
                        throw new NotSupportedException();
                }
            }

            return list;
        }

        private WriteRequest CreateInsertRequest(StateEntry entry)
        {
            var serializer = new InsertStateEntrySerializer(_serializerRegistry);
            return new InsertRequest(new BsonDocumentWrapper(entry, serializer));
        }

        private WriteRequest CreateUpdateRequest(StateEntry entry)
        {
            var filterSerializer = new IdOnlyStateEntrySerializer(_serializerRegistry);
            var updateSerializer = new UpdateStateEntrySerializer(_serializerRegistry);
            return new UpdateRequest(UpdateType.Update, 
                new BsonDocumentWrapper(entry, filterSerializer),
                new BsonDocumentWrapper(entry, updateSerializer));
        }

        private WriteRequest CreateDeleteRequest(StateEntry entry)
        {
            var serializer = new IdOnlyStateEntrySerializer(_serializerRegistry);
            return new DeleteRequest(new BsonDocumentWrapper(entry, serializer));
        }
    }
}
