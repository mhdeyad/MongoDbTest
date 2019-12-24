using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Companies.Models
{
    public class User
    {
        [BsonId]
        public ObjectId ID { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }
    }
}
