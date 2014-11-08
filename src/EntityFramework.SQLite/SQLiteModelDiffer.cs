// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Migrations.Model;
using Microsoft.Data.Entity.SQLite.Utilities;

namespace Microsoft.Data.Entity.SQLite
{
    public class SQLiteModelDiffer : ModelDiffer
    {
        public SQLiteModelDiffer([NotNull] MigrationOperationFactory operationFactory)
            : base(operationFactory)
        {
        }

        public SQLiteModelDiffer([NotNull] SQLiteDatabaseBuilder databaseBuilder)
            : base(databaseBuilder)
        {
        }

        protected override IReadOnlyList<MigrationOperation> Process(
            MigrationOperationCollection operations,
            IModel source,
            IModel target)
        {
            Check.NotNull(operations, "operations");
            Check.NotNull(source, "source");
            Check.NotNull(target, "target");

            return
                new SQLiteMigrationOperationPreProcessor(this)
                    .Process(operations, source, target);
        }
    }
}
