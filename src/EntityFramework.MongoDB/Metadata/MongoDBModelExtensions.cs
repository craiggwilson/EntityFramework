// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using JetBrains.Annotations;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.MongoDB.Utilities;
using Microsoft.Data.Entity.Relational.Utilities;

namespace Microsoft.Data.Entity.MongoDB.Metadata
{
    public class MongoDBModelExtensions : ReadOnlyMongoDBModelExtensions
    {
        public MongoDBModelExtensions([NotNull] Model model)
            : base(model)
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

                ((Model)Model)[MongoDBAnnotationNames.DatabaseName] = value;
            }
        }
    }
}
