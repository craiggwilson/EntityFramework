// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Migrations.Model;
using Microsoft.Data.Entity.Relational.Metadata;
using Microsoft.Data.Entity.SqlServer.Utilities;

namespace Microsoft.Data.Entity.SqlServer
{
    public class SqlServerModelDiffer : ModelDiffer
    {
        public SqlServerModelDiffer([NotNull] MigrationOperationFactory operationFactory)
            : base(operationFactory)
        {
        }

        public SqlServerModelDiffer([NotNull] SqlServerDatabaseBuilder databaseBuilder)
            : base(databaseBuilder)
        {
        }

        public virtual new SqlServerTypeMapper TypeMapper
        {
            get { return (SqlServerTypeMapper)base.TypeMapper; }
        }

        protected override IReadOnlyList<MigrationOperation> Process(
            MigrationOperationCollection operations, 
            IModel sourceModel, 
            IModel targetModel)
        {
            Check.NotNull(operations, "operations");
            Check.NotNull(sourceModel, "sourceModel");
            Check.NotNull(targetModel, "targetModel");

            return
                new SqlServerMigrationOperationPreProcessor(this)
                    .Process(operations, sourceModel, targetModel);
        }

        protected override IReadOnlyList<ISequence> GetSequences(IModel model)
        {
            Check.NotNull(model, "model");

            return
                model.EntityTypes
                    .SelectMany(t => t.Properties)
                    .Select(p => p.SqlServer().TryGetSequence())
                    .Where(s => s != null)
                    .Distinct((x, y) => x.Name == y.Name && x.Schema == y.Schema)
                    .ToList();
        }
    }
}
