using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Utilities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Microsoft.Data.Entity.MongoDB.Serialization
{
    public class EFSerializerRegistry : IBsonSerializerRegistry
    {
        private readonly ConcurrentDictionary<Type, IBsonSerializer> _cache;
        private readonly List<IBsonSerializationProvider> _serializationProviders;

        public EFSerializerRegistry(LazyRef<IModel> model)
        {
            _cache = new ConcurrentDictionary<Type, IBsonSerializer>();
            _serializationProviders = new List<IBsonSerializationProvider>
            {
                new CollectionsSerializationProvider(this),
                new PrimitiveSerializationProvider(),
                new BsonObjectModelSerializationProvider()
            };
        }

        public IBsonSerializer<T> GetSerializer<T>()
        {
            return (IBsonSerializer<T>)GetSerializer(typeof(T));
        }

        public IBsonSerializer GetSerializer(Type type)
        {
            return _cache.GetOrAdd(type, GetSerializerFromProviders);
        }

        private IBsonSerializer GetSerializerFromProviders(Type type)
        {
            foreach (var provider in _serializationProviders)
            {
                var serializer = provider.GetSerializer(type);
                if (serializer != null)
                {
                    return serializer;
                }
            }

            throw new InvalidOperationException("No serializer could be found.");
        }
    }
}
