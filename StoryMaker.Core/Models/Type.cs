using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoryMaker.Core.Models
{
    public class Type
    {
        [BsonElement("typeName")]
        public string TypeName { get; set; }
    }
}
