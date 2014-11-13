using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.MongoDB.Utilities;
using Microsoft.Framework.DependencyInjection;
using MongoDB.Driver.Core.Configuration;

namespace Microsoft.Data.Entity.MongoDB
{
    public class MongoDBOptionsExtension : DbContextOptionsExtension
    {
        public virtual ConnectionString ConnectionString { get; internal set; }

        public static MongoDBOptionsExtension Extract([NotNull] IDbContextOptions options)
        {
            Check.NotNull(options, "options");

            var mongoOptionsExtensions = options.Extensions
                .OfType<MongoDBOptionsExtension>()
                .ToArray();

            if (mongoOptionsExtensions.Length == 0)
            {
                throw new InvalidOperationException( /* TODO add message */);
            }

            if (mongoOptionsExtensions.Length > 1)
            {
                throw new InvalidOperationException( /* TODO add message */);
            }

            return mongoOptionsExtensions[0];
        }

        protected override void ApplyServices(EntityServicesBuilder builder)
        {
            builder.AddMongoDB();
        }
    }
}
