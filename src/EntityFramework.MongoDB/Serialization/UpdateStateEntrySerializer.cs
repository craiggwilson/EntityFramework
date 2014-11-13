using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Data.Entity.ChangeTracking;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.MongoDB.Utilities;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Microsoft.Data.Entity.MongoDB.Serialization
{
    public class UpdateStateEntrySerializer : StateEntrySerializerBase
    {
        public UpdateStateEntrySerializer([NotNull] IBsonSerializerRegistry serializerRegistry)
            : base(serializerRegistry)
        { }

        public override void Serialize(BsonSerializationContext context, StateEntry entry)
        {
            if (entry == null)
            {
                context.Writer.WriteNull();
            }

            context.Writer.WriteStartDocument();

            var changedProperties = GetChangedProperties(entry);
            context.Writer.WriteName("$set");
            context.Writer.WriteStartDocument();
            foreach (var property in changedProperties)
            {
                SerializeProperty(entry.Configuration.StateManager, context, property.MongoDB().Field, property, p => entry[p]);
            }
            context.Writer.WriteEndDocument();
            context.Writer.WriteEndDocument();
        }

        private List<IProperty> GetChangedProperties(StateEntry entry)
        {
            return entry.EntityType.Properties
                .Except(entry.EntityType.GetPrimaryKey().Properties) // _id cannot change in MongoDB
                .Where(x => entry.IsPropertyModified(x))
                .ToList();
        }
    }
}
