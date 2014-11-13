// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using JetBrains.Annotations;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.MongoDB.Utilities;
using Microsoft.Data.Entity.Relational.Utilities;
using MongoDB.Bson.Serialization;

namespace Microsoft.Data.Entity.MongoDB.Metadata
{
    public class ReadOnlyMongoDBPropertyExtensions : IMongoDBPropertyExtensions
    {
        private readonly IProperty _property;

        public ReadOnlyMongoDBPropertyExtensions([NotNull] IProperty property)
        {
            Check.NotNull(property, "property");

            _property = property;
        }

        public virtual string Field
        {
            get { return _property[MongoDBAnnotationNames.FieldName] ?? _property.Name; }
        }

        protected virtual IProperty Property
        {
            get { return _property; }
        }
    }
}
