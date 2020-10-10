using MongoDB.Bson.Serialization.Attributes;

namespace StoryMaker.Core.Models
{
    public class KeyPhrase
    {
        [BsonId]        
        [BsonElement("_id")]
        public string Id { get; set; }
        [BsonElement("text")]
        public string Text { get; set; }
    }
}