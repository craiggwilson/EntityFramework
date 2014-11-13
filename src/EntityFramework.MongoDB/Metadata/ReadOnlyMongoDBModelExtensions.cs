// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using JetBrains.Annotations;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.MongoDB.Utilities;
using Microsoft.Data.Entity.Relational.Utilities;

namespace Microsoft.Data.Entity.MongoDB.Metadata
{
    public class ReadOnlyMongoDBModelExtensions : IMongoDBModelExtensions
    {
        private readonly IModel _model;

        public ReadOnlyMongoDBModelExtensions([NotNull] IModel model)
        {
            Check.NotNull(model, "model");

            _model = model;
        }

        public virtual string Database
        {
            get { return _model[MongoDBAnnotationNames.DatabaseName]; }
        }

        protected virtual IModel Model
        {
            get { return _model; }
        }
    }
}
