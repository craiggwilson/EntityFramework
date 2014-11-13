// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using JetBrains.Annotations;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.MongoDB.Utilities;
using Microsoft.Data.Entity.Relational.Utilities;

namespace Microsoft.Data.Entity.MongoDB.Metadata
{
    public class MongoDBEntityTypeExtensions : ReadOnlyMongoDBEntityTypeExtensions
    {
        public MongoDBEntityTypeExtensions([NotNull] EntityType entityType)
            : base(entityType)
        {
        }

        [CanBeNull]
        public new virtual string Database
        {
            get { return base.Database; }
            [param: NotNull]
            set
            {
                Check.NullButNotEmpty(value, "value");

                ((EntityType)EntityType)[MongoDBAnnotationNames.DatabaseName] = value;
            }
        }

        [CanBeNull]
        public new virtual string Collection
        {
            get { return base.Collection; }
            [param: NotNull]
            set
            {
                Check.NullButNotEmpty(value, "value");

                ((EntityType)EntityType)[MongoDBAnnotationNames.CollectionName] = value;
            }
        }
    }
}
