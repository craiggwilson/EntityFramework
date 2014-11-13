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
    public class MongoDBModelBuilder
    {
        private readonly Model _model;

        public MongoDBModelBuilder([NotNull] Model model)
        {
            Check.NotNull(model, "model");

            _model = model;
        }

        public virtual MongoDBModelBuilder Database([CanBeNull] string databaseName)
        {
            Check.NullButNotEmpty(databaseName, "databaseName");

            _model.MongoDB().Database = databaseName;

            return this;
        }
    }
}
