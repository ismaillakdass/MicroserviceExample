﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CourseServices.Catalog.Models
{
    public class Courses
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        
        [BsonRepresentation(BsonType.ObjectId)]
        public string CategoryId { get; set; }
        public string Picture { get; set; }

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Price { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        public DateTime CreatedDate { get; set; }
        public string  Description { get; set; }
        public Feature Feature { get; set; }
        
        [BsonIgnore]
        public Category Category { get; set; }
    }
}
