// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Migrations.Model;
using Microsoft.Data.Entity.Relational.Model;
using Microsoft.Data.Entity.SqlServer.Utilities;

namespace Microsoft.Data.Entity.SqlServer
{
    public class SqlServerMigrationOperationPreProcessor : MigrationOperationVisitor<SqlServerMigrationOperationPreProcessor.Context>
    {
        private readonly SqlServerModelDiffer _modelDiffer;

        public SqlServerMigrationOperationPreProcessor([NotNull] SqlServerModelDiffer modelDiffer)
        {
            Check.NotNull(modelDiffer, "modelDiffer");

            _modelDiffer = modelDiffer;
        }

        public virtual SqlServerModelDiffer ModelDiffer
        {
            get { return _modelDiffer; }
        }

        public virtual IReadOnlyList<MigrationOperation> Process(            
            [NotNull] MigrationOperationCollection operations,
            [NotNull] IModel sourceModel,
            [NotNull] IModel targetModel)
        {
            Check.NotNull(operations, "operations");
            Check.NotNull(sourceModel, "sourceModel");
            Check.NotNull(targetModel, "targetModel");

            var context = new Context(operations, sourceModel, targetModel);

            foreach (var operation in operations.Get<DropTableOperation>())
            {
                Visit(operation, context);
            }

            foreach (var operation in operations.Get<DropColumnOperation>())
            {
                Visit(operation, context);
            }

            foreach (var operation in operations.Get<AlterColumnOperation>())
            {
                Visit(operation, context);
            }

            return context.Operations.GetAll();
        }

        public override void Visit(DropTableOperation dropTableOperation, Context context)
        {
            Check.NotNull(dropTableOperation, "dropTableOperation");
            Check.NotNull(context, "context");

            var entityType = context.SourceModel.EntityTypes.Single(
                t => ModelDiffer.NameGenerator.SchemaQualifiedTableName(t) == dropTableOperation.TableName);

            foreach (var foreignKey in context.SourceModel.EntityTypes
                    .SelectMany(t => t.ForeignKeys)
                    .Where(fk => ReferenceEquals(fk.ReferencedEntityType, entityType)))
            {
                context.Operations.Add(ModelDiffer.OperationFactory.DropForeignKeyOperation(foreignKey),
                    (x, y) => x.TableName == y.TableName && x.ForeignKeyName == y.ForeignKeyName);
            }
        }

        public override void Visit(DropColumnOperation dropColumnOperation, Context context)
        {
            Check.NotNull(dropColumnOperation, "dropColumnOperation");
            Check.NotNull(context, "context");

            var entityType = context.SourceModel.EntityTypes.Single(
                t => ModelDiffer.NameGenerator.SchemaQualifiedTableName(t) == dropColumnOperation.TableName);
            var property = entityType.Properties.Single(
                p => p.SqlServer().Column == dropColumnOperation.ColumnName);
            var extensions = property.SqlServer();

            if (extensions.DefaultValue != null || extensions.DefaultExpression != null)
            {
                context.Operations.Add(ModelDiffer.OperationFactory.DropDefaultConstraintOperation(property));
            }
        }

        public override void Visit(AlterColumnOperation alterColumnOperation, Context context)
        {
            Check.NotNull(alterColumnOperation, "alterColumnOperation");
            Check.NotNull(context, "context");

            var entityType = context.SourceModel.EntityTypes.Single(
                t => ModelDiffer.NameGenerator.SchemaQualifiedTableName(t) == alterColumnOperation.TableName);
            var property = entityType.Properties.Single(
                p => p.SqlServer().Column == alterColumnOperation.NewColumn.Name);
            var extensions = property.SqlServer();
            var newColumn = alterColumnOperation.NewColumn;

            string dataType, newDataType;
            GetDataTypes(entityType, property, newColumn, context, out dataType, out newDataType);

            var primaryKey = entityType.GetPrimaryKey();
            if (primaryKey != null
                && primaryKey.Properties.Any(p => ReferenceEquals(p, property)))
            {
                if (context.Operations.Add(ModelDiffer.OperationFactory.DropPrimaryKeyOperation(primaryKey),
                    (x, y) => x.TableName == y.TableName && x.PrimaryKeyName == y.PrimaryKeyName))
                {
                    context.Operations.Add(ModelDiffer.OperationFactory.AddPrimaryKeyOperation(primaryKey));
                }
            }

            // TODO: Changing the length of a variable-length column used in a UNIQUE constraint is allowed.
            foreach (var uniqueConstraint in entityType.Keys.Where(k => k != primaryKey)
                .Where(uc => uc.Properties.Any(p => ReferenceEquals(p, property))))
            {
                if (context.Operations.Add(ModelDiffer.OperationFactory.DropUniqueConstraintOperation(uniqueConstraint),
                    (x, y) => x.TableName == y.TableName && x.UniqueConstraintName == y.UniqueConstraintName))
                {
                    context.Operations.Add(ModelDiffer.OperationFactory.AddUniqueConstraintOperation(uniqueConstraint));
                }
            }

            foreach (var foreignKey in entityType.ForeignKeys
                .Where(fk => fk.Properties.Any(p => ReferenceEquals(p, property)))
                .Concat(context.SourceModel.EntityTypes
                    .SelectMany(t => t.ForeignKeys)
                    .Where(fk => fk.ReferencedProperties.Any(p => ReferenceEquals(p, property)))))
            {
                if (context.Operations.Add(ModelDiffer.OperationFactory.DropForeignKeyOperation(foreignKey),
                    (x, y) => x.TableName == y.TableName && x.ForeignKeyName == y.ForeignKeyName))
                {
                    context.Operations.Add(ModelDiffer.OperationFactory.AddForeignKeyOperation(foreignKey));
                }
            }

            if (dataType != newDataType
                || ((string.Equals(dataType, "varchar", StringComparison.OrdinalIgnoreCase)
                     || string.Equals(dataType, "nvarchar", StringComparison.OrdinalIgnoreCase)
                     || string.Equals(dataType, "varbinary", StringComparison.OrdinalIgnoreCase))
                    && newColumn.MaxLength > property.MaxLength))
            {
                foreach (var index in entityType.Indexes
                    .Where(ix => ix.Properties.Any(p => ReferenceEquals(p, property))))
                {
                    if (context.Operations.Add(ModelDiffer.OperationFactory.DropIndexOperation(index),
                        (x, y) => x.TableName == y.TableName && x.IndexName == y.IndexName))
                    {
                        context.Operations.Add(ModelDiffer.OperationFactory.CreateIndexOperation(index));
                    }
                }
            }

            if (extensions.DefaultValue != null || extensions.DefaultExpression != null)
            {
                context.Operations.Add(ModelDiffer.OperationFactory.DropDefaultConstraintOperation(property));
            }

            if (property.IsConcurrencyToken)
            {
                context.Operations.Remove(alterColumnOperation);
                context.Operations.Add(ModelDiffer.OperationFactory.DropColumnOperation(property));
                context.Operations.Add(ModelDiffer.OperationFactory.AddColumnOperation(property));
            }
        }

        protected virtual void GetDataTypes(
            [NotNull] IEntityType entityType, [NotNull] IProperty property, [NotNull] Column newColumn, [NotNull] Context context,
            out string dataType, out string newDataType)
        {
            Check.NotNull(entityType, "entityType");
            Check.NotNull(property, "property");
            Check.NotNull(newColumn, "newColumn");
            Check.NotNull(context, "context");

            var isKey = property.IsKey() || property.IsForeignKey();
            var extensions = property.SqlServer();

            dataType
                = ModelDiffer.TypeMapper.GetTypeMapping(
                    extensions.ColumnType, extensions.Column, property.PropertyType, isKey, property.IsConcurrencyToken)
                    .StoreTypeName;
            newDataType
                = ModelDiffer.TypeMapper.GetTypeMapping(
                    newColumn.DataType, newColumn.Name, newColumn.ClrType, isKey, newColumn.IsTimestamp)
                    .StoreTypeName;
        }

        public class Context
        {
            private readonly IModel _sourceModel;
            private readonly IModel _targetModel;
            private readonly MigrationOperationCollection _operations;

            public Context(
                [NotNull] MigrationOperationCollection operations,
                [NotNull] IModel sourceModel,
                [NotNull] IModel targetModel)
            {
                Check.NotNull(operations, "operations");
                Check.NotNull(sourceModel, "sourceModel");
                Check.NotNull(targetModel, "targetModel");

                _sourceModel = sourceModel;
                _targetModel = targetModel;
                _operations = operations;
            }

            public virtual IModel SourceModel
            {
                get { return _sourceModel; }
            }

            public virtual IModel TargetModel
            {
                get { return _targetModel; }
            }

            public virtual MigrationOperationCollection Operations
            {
                get { return _operations; }
            }
        }
    }
}
