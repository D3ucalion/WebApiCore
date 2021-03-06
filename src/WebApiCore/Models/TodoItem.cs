﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApiCore.Models
{
    namespace TodoApi.Models
    {
        public class TodoItem
        {
            public ObjectId Id { get; set; }
            [BsonElement("Key")]
            public string Key { get; set; }
            [BsonElement("Name")]
            public string Name { get; set; }
            [BsonElement("IsComplete")]
            public bool IsComplete { get; set; }
        }
    }
}
