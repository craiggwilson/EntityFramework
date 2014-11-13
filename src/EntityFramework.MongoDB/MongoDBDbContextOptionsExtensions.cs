// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using JetBrains.Annotations;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.MongoDB;
using Microsoft.Data.Entity.MongoDB.Utilities;
using MongoDB.Driver.Core.Configuration;

namespace Microsoft.Data.Entity.MongoDB
{
    public static class MongoDBDbContextOptionsExtensions
    {
        public static DbContextOptions UseMongoDB([NotNull] this DbContextOptions options,
            [NotNull] string connectionString)
        {
            Check.NotNull(options, "options");

            ((IDbContextOptions)options).AddOrUpdateExtension<MongoDBOptionsExtension>(
                optionsExtension =>
                    {
                        optionsExtension.ConnectionString = new ConnectionString(connectionString);
                    });

            return options;
        }
    }
}
