using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoryMaker.Core.Models
{
    public class Phrase
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        [BsonElement("_id")]
        public string Id { get; set; }        
        [BsonElement("text")]
        public string PhraseText { get; set; }
        [BsonElement("sentiment")]
        public string Sentiment { get; set; }
        [BsonElement("lenght")]
        public double Lenght { get; set; }
        [BsonElement("positive_score")]
        public double PositiveScore { get; set; }
        [BsonElement("neutral_score")]
        public double NeutralScore { get; set; }
        [BsonElement("negative_score")]
        public double NegativeScore { get; set; }
        [BsonElement("language")]
        public string Language { get; set; }        
        [BsonElement("phrase_type")]
        public string PhraseType { get; set; }
        [BsonElement("named_entities")]
        public IEnumerable<NamedEntity> NamedEntities { get; set; }
        [BsonElement("key_phrases")]
        public IEnumerable<KeyPhrase> KeyPhrases { get; set; }
    }
}
