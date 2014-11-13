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
using Microsoft.Data.Entity.Storage;
using Microsoft.Data.Entity.Utilities;
using Microsoft.Framework.Logging;
using Remotion.Linq;

namespace Microsoft.Data.Entity.MongoDB
{
    public class MongoDBDataStore : DataStore
    {
        private readonly LazyRef<MongoDBDatabase> _database;

        public MongoDBDataStore(
             [NotNull] StateManager stateManager,
             [NotNull] LazyRef<IModel> model,
             [NotNull] EntityKeyFactorySource entityKeyFactorySource,
             [NotNull] EntityMaterializerSource entityMaterializerSource,
             [NotNull] ClrCollectionAccessorSource collectionAccessorSource,
             [NotNull] ClrPropertySetterSource propertySetterSource,
             [NotNull] LazyRef<Database> database,
             [NotNull] ILoggerFactory loggerFactory)
            : base(stateManager, model, entityKeyFactorySource, entityMaterializerSource,
                collectionAccessorSource, propertySetterSource, loggerFactory)
        {
            _database = new LazyRef<MongoDBDatabase>(() => (MongoDBDatabase)database.Value);
        }

        public override int SaveChanges(IReadOnlyList<StateEntry> stateEntries)
        {
            return SaveChangesAsync(stateEntries).GetAwaiter().GetResult();
        }

        public override Task<int> SaveChangesAsync(IReadOnlyList<StateEntry> stateEntries, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _database.Value.SaveChangesAsync(stateEntries, cancellationToken);
        }

        public override IEnumerable<TResult> Query<TResult>(QueryModel queryModel)
        {
            throw new NotImplementedException();
        }

        public override IAsyncEnumerable<TResult> AsyncQuery<TResult>(QueryModel queryModel, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
