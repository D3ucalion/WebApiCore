using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApiCore.Models
{
    namespace HygroApi.Models
    {
        public class HygroItem
        {
            public ObjectId Id { get; set; }
            [BsonElement("Key")]
            public string Key { get; set; }
            [BsonElement("Location")]
            public string Name { get; set; }
            [BsonElement("TimeStamp")]
            public DateTime TimeStamp { get; set; }
            [BsonElement("Temp")]
            public float Temp { get; set; }
            [BsonElement("Humidity")]
            public float Humidity { get; set; }
        }
    }
}
