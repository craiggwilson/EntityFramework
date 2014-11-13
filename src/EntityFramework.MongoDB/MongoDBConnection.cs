using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Storage;
using Microsoft.Data.Entity.Utilities;
using Microsoft.Data.Entity.MongoDB.Utilities;
using Microsoft.Framework.Logging;
using MongoDB.Driver.Core.Configuration;

namespace Microsoft.Data.Entity.MongoDB
{
    public class MongoDBConnection : DataStoreConnection
    {
        private readonly ConnectionString _connectionString;

        /// <summary>
        ///     This constructor is intended only for use when creating test doubles that will override members
        ///     with mocked or faked behavior. Use of this constructor for other purposes may result in unexpected
        ///     behavior including but not limited to throwing <see cref="NullReferenceException" />.
        /// </summary>
        protected MongoDBConnection()
        {
        }

        public MongoDBConnection([NotNull] LazyRef<IDbContextOptions> options, [NotNull] ILoggerFactory loggerFactory)
            : base(loggerFactory)
        {
            Check.NotNull(options, "options");

            var extracted = MongoDBOptionsExtension.Extract(options.Value);
            _connectionString = extracted.ConnectionString;
        }

        public virtual ConnectionString ConnectionString
        {
            get { return _connectionString; }
        }
    }
}
