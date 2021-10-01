using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MongoDbSimple.Entities
{
    public class Customer
    {
        [BsonId]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
