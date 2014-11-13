// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using JetBrains.Annotations;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.MongoDB.Utilities;
using Microsoft.Data.Entity.Relational.Utilities;
using MongoDB.Bson.Serialization;

namespace Microsoft.Data.Entity.MongoDB.Metadata
{
    public class MongoDBPropertyExtensions : ReadOnlyMongoDBPropertyExtensions
    {
        public MongoDBPropertyExtensions([NotNull] Property property)
            : base(property)
        { }

        [CanBeNull]
        public new virtual string Field
        {
            get { return base.Field; }
            [param: NotNull]
            set
            {
                Check.NullButNotEmpty(value, "value");

                ((Property)Property)[MongoDBAnnotationNames.FieldName] = value;
            }
        }
    }
}
