using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.MongoDB.Utilities;

namespace Microsoft.Data.Entity.MongoDB.Metadata
{
    public class MongoDBEntityBuilder
    {
        private readonly EntityType _entityType;

        public MongoDBEntityBuilder([NotNull] EntityType entityType)
        {
            Check.NotNull(entityType, "entityType");

            _entityType = entityType;
        }

        public virtual MongoDBEntityBuilder Database([CanBeNull] string databaseName)
        {
            Check.NullButNotEmpty(databaseName, "databaseName");

            _entityType.MongoDB().Database = databaseName;

            return this;
        }

        public virtual MongoDBEntityBuilder Collection([CanBeNull] string collectionName)
        {
            Check.NullButNotEmpty(collectionName, "collectionName");

            _entityType.MongoDB().Collection = collectionName;

            return this;
        }
    }
}
