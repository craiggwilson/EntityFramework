// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Identity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.MongoDB.Utilities;
using MongoDB.Bson;

namespace Microsoft.Data.Entity.MongoDB
{
    public class MongoDBValueGeneratorSelector : ValueGeneratorSelector
    {
        private readonly SimpleValueGeneratorFactory<ObjectIdValueGenerator> _objectIdValueGeneratorFactory;

        public MongoDBValueGeneratorSelector(
            [NotNull] SimpleValueGeneratorFactory<GuidValueGenerator> guidFactory,
            [NotNull] SimpleValueGeneratorFactory<ObjectIdValueGenerator> objectIdFactory)
            : base(guidFactory)
        {
            Check.NotNull(objectIdFactory, "objectIdFactory");

            _objectIdValueGeneratorFactory = objectIdFactory;
        }

        public override IValueGeneratorFactory Select(IProperty property)
        {
            Check.NotNull(property, "property");

            if (property.GenerateValueOnAdd
                && (property.PropertyType == typeof(ObjectId)))
            {
                return _objectIdValueGeneratorFactory;
            }

            return base.Select(property);
        }
    }
}
