// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using JetBrains.Annotations;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.MongoDB.Utilities;
using Microsoft.Data.Entity.Relational.Utilities;

namespace Microsoft.Data.Entity.MongoDB.Metadata
{
    public class ReadOnlyMongoDBEntityTypeExtensions : IMongoDBEntityTypeExtensions
    {
        private readonly IEntityType _entityType;

        public ReadOnlyMongoDBEntityTypeExtensions([NotNull] IEntityType entityType)
        {
            Check.NotNull(entityType, "entityType");

            _entityType = entityType;
        }

        public virtual string Database
        {
            get { return _entityType[MongoDBAnnotationNames.DatabaseName] ?? _entityType.Model[MongoDBAnnotationNames.DatabaseName]; }
        }

        public virtual string Collection
        {
            get { return _entityType[MongoDBAnnotationNames.CollectionName] ?? _entityType.SimpleName; }
        }

        protected virtual IEntityType EntityType
        {
            get { return _entityType; }
        }
    }
}
