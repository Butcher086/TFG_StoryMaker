using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoryMaker.Core.Models
{
    public class HistoryPhrase
    {
        [BsonElement("phraseId")]
        public string PhraseID { get; set; }
        [BsonElement("order")]
        public int Order { get; set; }
    }
}
