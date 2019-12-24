using Companies.Lib;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Companies.Models
{
    public class Contact
    {
        [BsonId]
        public ObjectId ID { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }
        //[BsonIgnore]
        //public FieldType[] Properties { get; set; }

        public List<Company> Companeis { get; set; }
     
        [BsonExtraElements]
        public BsonDocument Properties { get; set; }
    }
}
