using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Identity;
using Microsoft.Data.Entity.MongoDB;
using Microsoft.Data.Entity.MongoDB.Serialization;
using Microsoft.Data.Entity.MongoDB.Utilities;
using Microsoft.Data.Entity.Storage;
using Microsoft.Framework.DependencyInjection;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Microsoft.Data.Entity.MongoDB
{
    public static class MongoDBEntityServicesBuilderExtensions
    {
        public static EntityServicesBuilder AddMongoDB([NotNull] this EntityServicesBuilder builder)
        {
            Check.NotNull(builder, "builder");

            builder.ServiceCollection
                .AddSingleton<MongoDBValueGeneratorSelector>()
                .AddSingleton<MongoDBValueGeneratorCache>()
                .AddSingleton<SimpleValueGeneratorFactory<ObjectIdValueGenerator>>()
                .AddScoped<DataStoreSource, MongoDBDataStoreSource>()
                .AddScoped<MongoDBOptionsExtension>()
                .AddScoped<MongoDBDataStore>()
                .AddScoped<MongoDBDataStoreServices>()
                .AddScoped<MongoDBConnection>()
                .AddScoped<MongoDBDataStoreCreator>()
                .AddScoped<MongoDBDatabase>();

            return builder;
        }
    }
}
