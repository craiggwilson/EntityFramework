using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Identity;

namespace Microsoft.Data.Entity.MongoDB
{
    public class MongoDBValueGeneratorCache : ValueGeneratorCache
    {
        public MongoDBValueGeneratorCache(
            [NotNull] MongoDBValueGeneratorSelector selector,
            [NotNull] ForeignKeyValueGenerator foreignKeyValueGenerator)
            : base(selector, foreignKeyValueGenerator)
        {
        }
    }
}
