using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoryMaker.Core.Models
{
    public class History
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        [BsonElement("_id")]
        public string Id { get; set; }
        [BsonElement("type")]
        public string Type { get; set; }
        [BsonElement("phrases")]
        public List<HistoryPhrase> Phrases { get; set; }
    }

    public class HistoryText
    {
        public string Id { get; set; }
        public List<string> textPhrases { get; set; }
    }
}
