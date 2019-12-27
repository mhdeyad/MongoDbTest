using Companies.Lib;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Companies.Models
{
    /// <summary>
    /// An Entity that contain contact data
    /// </summary>
    public class Contact
    {
        [BsonId]
        public ObjectId ID { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("Companies")]
        public List<ObjectId> Companies { get; set; }
     
        [BsonExtraElements]
        public BsonDocument Properties { get; set; }
    }
}
