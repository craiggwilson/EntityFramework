using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Data.Entity.MongoDB.Metadata
{
    public static class MongoDBAnnotationNames
    {
        private const string Prefix = "MongoDB:";
        public const string FieldName = Prefix + "FieldName";
        public const string DatabaseName = Prefix + "DatabaseName";
        public const string CollectionName = Prefix + "CollectionName";
    }
}
