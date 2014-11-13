using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Microsoft.Data.Entity.MongoDB.Serialization
{
    public class PrimitiveSerializationProvider : IBsonSerializationProvider
    {
        private static readonly Dictionary<Type, IBsonSerializer> _serializers;

        static PrimitiveSerializationProvider()
        {
            _serializers = new Dictionary<Type, IBsonSerializer>
            {
                { typeof(Boolean), new BooleanSerializer() },
                { typeof(Byte), new ByteSerializer() },
                { typeof(Byte[]), new ByteArraySerializer() },
                { typeof(DateTime), new DateTimeSerializer() },
                { typeof(DateTimeOffset), new DateTimeOffsetSerializer() },
                { typeof(Decimal), new DecimalSerializer() },
                { typeof(Double), new DoubleSerializer() },
                { typeof(Guid), new GuidSerializer() },
                { typeof(Int32), new Int32Serializer() },
                { typeof(Int64), new Int64Serializer() },
                { typeof(ObjectId), new ObjectIdSerializer() },
                { typeof(SByte), new SByteSerializer() },
                { typeof(Single), new SingleSerializer() },
                { typeof(String), new StringSerializer() },
                { typeof(TimeSpan), new TimeSpanSerializer() },
                { typeof(UInt16), new UInt16Serializer() },
                { typeof(UInt32), new UInt32Serializer() },
                { typeof(UInt64), new UInt64Serializer() },
            };
        }

        public IBsonSerializer GetSerializer(Type type)
        {
            IBsonSerializer value;
            if (_serializers.TryGetValue(type, out value))
                return value;

            return null;
        }
    }
}
