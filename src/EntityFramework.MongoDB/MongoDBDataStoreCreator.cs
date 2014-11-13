// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.MongoDB.Utilities;
using Microsoft.Data.Entity.Storage;
using Microsoft.Data.Entity.Utilities;

namespace Microsoft.Data.Entity.MongoDB
{
    public class MongoDBDataStoreCreator : DataStoreCreator
    {
        private readonly LazyRef<MongoDBDatabase> _database;

        /// <summary>
        ///     This constructor is intended only for use when creating test doubles that will override members
        ///     with mocked or faked behavior. Use of this constructor for other purposes may result in unexpected
        ///     behavior including but not limited to throwing <see cref="NullReferenceException" />.
        /// </summary>
        protected MongoDBDataStoreCreator()
        {
        }

        public MongoDBDataStoreCreator([NotNull] DbContextConfiguration configuration)
        {
            Check.NotNull(configuration, "configuration");

            _database = new LazyRef<MongoDBDatabase>(() => (MongoDBDatabase)configuration.Database);
        }

        public override bool EnsureDeleted(IModel model)
        {
            return false;
        }

        public override Task<bool> EnsureDeletedAsync(IModel model, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.FromResult(false);
        }

        public override bool EnsureCreated(IModel model)
        {
            // returns whether anything changed. In MongoDB the database is
            // always ready. No need to create anything for each collection.
            return false;
        }

        public override Task<bool> EnsureCreatedAsync(IModel model, CancellationToken cancellationToken = default(CancellationToken))
        {
            // returns whether anything changed. In MongoDB the database is
            // always ready. No need to create anything for each collection.
            return Task.FromResult(false);
        }
    }
}
