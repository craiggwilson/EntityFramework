using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Entity.ChangeTracking;
using Microsoft.Data.Entity.Identity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.MongoDB.Utilities;
using MongoDB.Bson;

namespace Microsoft.Data.Entity.MongoDB
{
    public class ObjectIdValueGenerator : SimpleValueGenerator
    {
        public override void Next(StateEntry stateEntry, IProperty property)
        {
            Check.NotNull(stateEntry, "stateEntry");
            Check.NotNull(property, "property");

            stateEntry[property] = ObjectId.GenerateNewId();
        }
    }
}
