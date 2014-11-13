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
    public class InsertStateEntrySerializer : StateEntrySerializerBase
    {
        public InsertStateEntrySerializer([NotNull] IBsonSerializerRegistry serializerRegistry)
            : base(serializerRegistry)
        { }

        public override void Serialize(BsonSerializationContext context, StateEntry entry)
        {
            if (entry == null)
            {
                context.Writer.WriteNull();
            }

            context.Writer.WriteStartDocument();
            var entityType = entry.EntityType;
            Func<IProperty, object> valueGetter = p => entry[p];

            var primaryKey = entityType.GetPrimaryKey();
            if (primaryKey.Properties.Count > 1)
            {
                throw new NotSupportedException("May only have 1 property as a key. Use a struct or nested class if you wish to use a compound key.");
            }

            var idProperty = primaryKey.Properties[0];

            SerializeProperty(entry.Configuration.StateManager, context, "_id", idProperty, valueGetter);

            foreach (var property in entityType.Properties.Except(primaryKey.Properties))
            {
                var name = property.MongoDB().Field;
                SerializeProperty(entry.Configuration.StateManager, context, name, property, valueGetter);
            }

            context.Writer.WriteEndDocument();
        }

    }
}
