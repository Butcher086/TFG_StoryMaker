using MongoDB.Bson.Serialization.Attributes;

namespace StoryMaker.Core.Models
{
    public class NamedEntity
    {
        [BsonId]        
        [BsonElement("_id")]
        public string Id { get; set; }
        [BsonElement("text")]
        public string Text { get; set; }
        [BsonElement("category")]
        public string Category { get; set; }
        [BsonElement("sub-category")]
        public string SubCategory { get; set; }
        [BsonElement("score")]
        public double Score { get; set; }
    }
}