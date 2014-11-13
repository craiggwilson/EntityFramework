using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Data.Entity.MongoDB.Utilities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Microsoft.Data.Entity.MongoDB.Serialization
{
    public class CollectionsSerializationProvider : IBsonSerializationProvider
    {
        private static readonly Dictionary<Type, IBsonSerializer> _serializers;
        private readonly IBsonSerializerRegistry _registry;

        static CollectionsSerializationProvider()
        {
            _serializers = new Dictionary<Type, IBsonSerializer>
            {
                { typeof(ExpandoObject), new ExpandoObjectSerializer() }
            };
        }

        public CollectionsSerializationProvider([NotNull] IBsonSerializerRegistry registry)
        {
            Check.NotNull(registry, "registry");
            _registry = registry;
        }

        public IBsonSerializer GetSerializer(Type type)
        {
            IBsonSerializer value;
            if (_serializers.TryGetValue(type, out value))
                return value;

            if (type.IsGenericType)
            {
                var gtd = type.GetGenericTypeDefinition();
                if (gtd == typeof(List<>))
                {
                    var itemType = type.GetGenericArguments()[0];
                    var serializerType = typeof(EnumerableInterfaceImplementerSerializer<>).MakeGenericType(itemType);

                    // TODO: we have a cycle problem
                    var itemSerializer = _registry.GetSerializer(itemType);
                    return (IBsonSerializer)Activator.CreateInstance(serializerType, itemSerializer);
                }
            }

            return null;
        }
    }
}
