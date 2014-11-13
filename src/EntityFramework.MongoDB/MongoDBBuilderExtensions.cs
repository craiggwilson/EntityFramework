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
    public static class MongoDBBuilderExtensions
    {
        public static MongoDBModelBuilder ForMongoDB<TModelBuilder>(
            [NotNull] this IModelBuilder<TModelBuilder> modelBuilder)
            where TModelBuilder : IModelBuilder<TModelBuilder>
        {
            Check.NotNull(modelBuilder, "modelBuilder");

            return new MongoDBModelBuilder(modelBuilder.Metadata);
        }

        public static MongoDBEntityBuilder ForMongoDB<TEntityBuilder>(
            [NotNull] this IEntityBuilder<TEntityBuilder> entityBuilder)
            where TEntityBuilder : IEntityBuilder<TEntityBuilder>
        {
            Check.NotNull(entityBuilder, "entityBuilder");

            return new MongoDBEntityBuilder(entityBuilder.Metadata);
        }

        public static MongoDBEntityBuilder ForMongoDB<TEntity, TEntityBuilder>(
            [NotNull] this IEntityBuilder<TEntity, TEntityBuilder> entityBuilder)
            where TEntityBuilder : IEntityBuilder<TEntity, TEntityBuilder>
        {
            Check.NotNull(entityBuilder, "entityBuilder");

            return new MongoDBEntityBuilder(entityBuilder.Metadata);
        }

        public static MongoDBPropertyBuilder ForMongoDB<TPropertyBuilder>(
            [NotNull] this IPropertyBuilder<TPropertyBuilder> propertyBuilder)
            where TPropertyBuilder : IPropertyBuilder<TPropertyBuilder>
        {
            Check.NotNull(propertyBuilder, "propertyBuilder");

            return new MongoDBPropertyBuilder(propertyBuilder.Metadata);
        }
    }
}
