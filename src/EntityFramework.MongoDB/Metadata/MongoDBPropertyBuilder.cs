using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.MongoDB.Utilities;

namespace Microsoft.Data.Entity.MongoDB.Metadata
{
    public class MongoDBPropertyBuilder
    {
        private readonly Property _property;

        public MongoDBPropertyBuilder([NotNull] Property property)
        {
            Check.NotNull(property, "property");

            _property = property;
        }

        public virtual MongoDBPropertyBuilder Field([CanBeNull] string fieldName)
        {
            Check.NullButNotEmpty(fieldName, "fieldName");

            _property.MongoDB().Field = fieldName;

            return this;
        }
    }
}
