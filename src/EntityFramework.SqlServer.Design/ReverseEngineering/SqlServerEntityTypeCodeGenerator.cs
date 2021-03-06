﻿// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Relational.Design.CodeGeneration;
using Microsoft.Data.Entity.Relational.Design.ReverseEngineering;
using Microsoft.Data.Entity.Utilities;
using Microsoft.Framework.Logging;

namespace Microsoft.Data.Entity.SqlServer.Design.ReverseEngineering
{
    public class SqlServerEntityTypeCodeGenerator : EntityTypeCodeGenerator
    {
        public SqlServerEntityTypeCodeGenerator(
            [NotNull] ReverseEngineeringGenerator generator,
            [NotNull] IEntityType entityType,
            [NotNull] string namespaceName)
            : base(generator, entityType, namespaceName)
        {
            Check.NotNull(generator, nameof(generator));
            Check.NotNull(entityType, nameof(entityType));
            Check.NotNull(namespaceName, nameof(namespaceName));
        }

        public override void Generate(IndentedStringBuilder sb)
        {
            Check.NotNull(sb, nameof(sb));

            var errorMessageAnnotation =
                (string)EntityType[SqlServerMetadataModelProvider.AnnotationNameEntityTypeError];

            if (errorMessageAnnotation != null)
            {
                GenerateCommentHeader(sb);
                Generator.CSharpCodeGeneratorHelper.SingleLineComment(errorMessageAnnotation, sb);

                return;
            }

            base.Generate(sb);
        }

        public override void GenerateZeroArgConstructorContents(IndentedStringBuilder sb)
        {
            Check.NotNull(sb, nameof(sb));

            foreach (var otherEntityType in EntityType.Model.EntityTypes.Where(et => et != EntityType))
            {
                // find navigation properties for foreign keys from another EntityType which reference this EntityType
                foreach (var foreignKey in otherEntityType.GetForeignKeys().Where(fk => fk.PrincipalEntityType == EntityType))
                {
                    var navigationPropertyName =
                        foreignKey[SqlServerMetadataModelProvider.AnnotationNamePrincipalEndNavPropName];
                    if (((EntityType)otherEntityType)
                        .FindAnnotation(SqlServerMetadataModelProvider.AnnotationNameEntityTypeError) == null)
                    {
                        if (!foreignKey.IsUnique)
                        {
                            sb.Append(navigationPropertyName);
                            sb.Append(" = new HashSet<");
                            sb.Append(otherEntityType.Name);
                            sb.AppendLine(">();");
                        }
                    }
                }
            }
        }

        public override void GenerateEntityProperties(IndentedStringBuilder sb)
        {
            Check.NotNull(sb, nameof(sb));

            sb.AppendLine();
            Generator.CSharpCodeGeneratorHelper.SingleLineComment("Properties", sb);
            base.GenerateEntityProperties(sb);
        }

        public override void GenerateEntityProperty(IProperty property, IndentedStringBuilder sb)
        {
            Check.NotNull(property, nameof(property));
            Check.NotNull(sb, nameof(sb));

            GenerateEntityPropertyAttribues(property, sb);

            Generator.CSharpCodeGeneratorHelper.AddProperty(AccessModifier.Public,
                VirtualModifier.None, property.ClrType, property.Name, sb);
        }

        public override void GenerateEntityNavigations(IndentedStringBuilder sb)
        {
            Check.NotNull(sb, nameof(sb));

            sb.AppendLine();
            Generator.CSharpCodeGeneratorHelper.SingleLineComment("Navigation Properties", sb);

            // construct navigations from foreign keys
            foreach (var otherEntityType in EntityType.Model.EntityTypes.Where(et => et != EntityType))
            {
                // set up the navigation properties for foreign keys from another EntityType which reference this EntityType
                foreach (var foreignKey in otherEntityType.GetForeignKeys().Where(fk => fk.PrincipalEntityType == EntityType))
                {
                    var navigationPropertyName 
                        = (string)foreignKey.GetAnnotation(SqlServerMetadataModelProvider.AnnotationNamePrincipalEndNavPropName).Value;

                    if (((EntityType)otherEntityType)
                        .FindAnnotation(SqlServerMetadataModelProvider.AnnotationNameEntityTypeError) != null)
                    {
                        Generator.CSharpCodeGeneratorHelper.SingleLineComment("Unable to add a Navigation Property referencing type "
                            + otherEntityType.Name + " because of errors generating that EntityType.",
                            sb);
                    }
                    else
                    {
                        if (foreignKey.IsUnique)
                        {
                            Generator.CSharpCodeGeneratorHelper.AddProperty(
                                AccessModifier.Public,
                                VirtualModifier.Virtual,
                                otherEntityType.Name,
                                navigationPropertyName,
                                sb);
                        }
                        else
                        {
                            Generator.CSharpCodeGeneratorHelper.AddProperty(
                                AccessModifier.Public,
                                VirtualModifier.Virtual,
                                "ICollection<" + otherEntityType.Name + ">",
                                navigationPropertyName,
                                sb);
                        }
                    }
                }
            }

            foreach (var foreignKey in EntityType.GetForeignKeys())
            {
                // set up the navigation property on this end of foreign keys owned by this EntityType
                var navigationPropertyName 
                    = (string)foreignKey.GetAnnotation(SqlServerMetadataModelProvider.AnnotationNameDependentEndNavPropName).Value;

                Generator.CSharpCodeGeneratorHelper.AddProperty(
                    AccessModifier.Public,
                    VirtualModifier.Virtual,
                    foreignKey.PrincipalEntityType.Name,
                    navigationPropertyName,
                    sb);
            }
        }

        public virtual void GenerateEntityPropertyAttribues(
            [NotNull] IProperty property, [NotNull] IndentedStringBuilder sb)
        {
            Check.NotNull(property, nameof(property));
            Check.NotNull(sb, nameof(sb));

            //TODO: to use when we the runtime recognizes and uses DataAnnotations
        }
    }
}
