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
    public class IdOnlyStateEntrySerializer : StateEntrySerializerBase
    {
        public IdOnlyStateEntrySerializer([NotNull] IBsonSerializerRegistry serializerRegistry)
            : base(serializerRegistry)
        { }

        public override void Serialize(BsonSerializationContext context, StateEntry entry)
        {
            if (entry == null)
            {
                context.Writer.WriteNull();
            }

            context.Writer.WriteStartDocument();

            SerializeId(context, entry);

            context.Writer.WriteEndDocument();
        }

    }
}
