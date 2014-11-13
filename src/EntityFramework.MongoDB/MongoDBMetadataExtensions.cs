using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.MongoDB.Metadata;
using Microsoft.Data.Entity.MongoDB.Utilities;

namespace Microsoft.Data.Entity.MongoDB
{
    public static class MongoDBMetadataExtensions
    {
        public static MongoDBModelExtensions MongoDB([NotNull] this Model model)
        {
            Check.NotNull(model, "model");

            return new MongoDBModelExtensions(model);
        }

        public static ReadOnlyMongoDBModelExtensions MongoDB([NotNull] this IModel model)
        {
            Check.NotNull(model, "model");

            return new ReadOnlyMongoDBModelExtensions(model);
        }

        public static MongoDBEntityTypeExtensions MongoDB([NotNull] this EntityType entityType)
        {
            Check.NotNull(entityType, "entityType");

            return new MongoDBEntityTypeExtensions(entityType);
        }

        public static ReadOnlyMongoDBEntityTypeExtensions MongoDB([NotNull] this IEntityType entityType)
        {
            Check.NotNull(entityType, "entityType");

            return new ReadOnlyMongoDBEntityTypeExtensions(entityType);
        }

        public static MongoDBPropertyExtensions MongoDB([NotNull] this Property property)
        {
            Check.NotNull(property, "property");

            return new MongoDBPropertyExtensions(property);
        }

        public static ReadOnlyMongoDBPropertyExtensions MongoDB([NotNull] this IProperty property)
        {
            Check.NotNull(property, "property");

            return new ReadOnlyMongoDBPropertyExtensions(property);
        }
    }
}
