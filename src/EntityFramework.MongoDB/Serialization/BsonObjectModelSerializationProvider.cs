using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Microsoft.Data.Entity.MongoDB.Serialization
{
    public class BsonObjectModelSerializationProvider : IBsonSerializationProvider
    {
        private static readonly Dictionary<Type, IBsonSerializer> _serializers;

        static BsonObjectModelSerializationProvider()
        {
            _serializers = new Dictionary<Type, IBsonSerializer>
            {
                { typeof(BsonArray), BsonArraySerializer.Instance },
                { typeof(BsonBinaryData), BsonBinaryDataSerializer.Instance },
                { typeof(BsonBoolean), BsonBooleanSerializer.Instance },
                { typeof(BsonDateTime), BsonDateTimeSerializer.Instance },
                { typeof(BsonDocument), BsonDocumentSerializer.Instance },
                { typeof(BsonDocumentWrapper), BsonDocumentWrapperSerializer.Instance },
                { typeof(BsonDouble), BsonDoubleSerializer.Instance },
                { typeof(BsonInt32), BsonInt32Serializer.Instance },
                { typeof(BsonInt64), BsonInt64Serializer.Instance },
                { typeof(BsonJavaScript), BsonJavaScriptSerializer.Instance },
                { typeof(BsonJavaScriptWithScope), BsonJavaScriptWithScopeSerializer.Instance },
                { typeof(BsonMaxKey), BsonMaxKeySerializer.Instance },
                { typeof(BsonMinKey), BsonMinKeySerializer.Instance },
                { typeof(BsonNull), BsonNullSerializer.Instance },
                { typeof(BsonObjectId), BsonObjectIdSerializer.Instance },
                { typeof(BsonRegularExpression), BsonRegularExpressionSerializer.Instance },
                { typeof(BsonString), BsonStringSerializer.Instance },
                { typeof(BsonSymbol), BsonSymbolSerializer.Instance },
                { typeof(BsonTimestamp), BsonTimestampSerializer.Instance },
                { typeof(BsonUndefined), BsonUndefinedSerializer.Instance },
                { typeof(BsonValue), BsonValueSerializer.Instance }
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
