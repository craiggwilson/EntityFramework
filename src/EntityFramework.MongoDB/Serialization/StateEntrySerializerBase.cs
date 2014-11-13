using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Data.Entity.ChangeTracking;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.MongoDB.Utilities;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Microsoft.Data.Entity.MongoDB.Serialization
{
    public abstract class StateEntrySerializerBase : SerializerBase<StateEntry>
    {
        private readonly IBsonSerializerRegistry _serializerRegistry;

        public StateEntrySerializerBase([NotNull] IBsonSerializerRegistry serializerRegistry)
        {
            Check.NotNull(serializerRegistry, "registry");

            _serializerRegistry = serializerRegistry;
        }

        protected void SerializeId(BsonSerializationContext context, StateEntry entry)
        {
            Func<IProperty, object> valueGetter = p => entry[p];

            var primaryKey = entry.EntityType.GetPrimaryKey();
            if (primaryKey.Properties.Count > 1)
            {
                throw new NotSupportedException("May only have 1 property as a key. Use a struct or nested class if you wish to use a compound key.");
            }

            if (primaryKey.Properties.Count == 0)
            {
                return;
            }

            var idProperty = primaryKey.Properties[0];

            SerializeProperty(entry.Configuration.StateManager, context, "_id", idProperty, valueGetter);
        }

        protected void SerializeProperty(StateManager stateManager, BsonSerializationContext context, string name, IProperty property, Func<IProperty, object> valueGetter)
        {
            var value = valueGetter(property);
            IEntityType propertyAsEntity = stateManager.Model.TryGetEntityType(property.UnderlyingType);
            IBsonSerializer serializer;
            if (propertyAsEntity != null)
            {
                // talk to a sidecar and get the values...
                return;
            }
            else
            {
                serializer = _serializerRegistry.GetSerializer(property.UnderlyingType);
            }
            context.Writer.WriteName(name);
            context.SerializeWithChildContext(serializer, value);
        }
    }
}
