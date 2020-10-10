using MongoDB.Driver;
using StoryMaker.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoryMaker.Api.Services
{
    public class PhraseService
    {
        private readonly IMongoCollection<Phrase> _phrases;

        public PhraseService(IStoryMakerDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _phrases = database.GetCollection<Phrase>(settings.PhrasesCollectionName);
        }

        public List<Phrase> Get() =>
            _phrases.Find<Phrase>(phrase => true).ToList();

        public Phrase Get(string id) =>
            _phrases.Find<Phrase>(phrase => phrase.Id == id).FirstOrDefault();

        public Phrase Create(Phrase phrase)
        {
            try
            {
                _phrases.InsertOne(phrase);
                return phrase;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        public void Update(string id, Phrase phraseIn) =>
            _phrases.ReplaceOne(phrase => phrase.Id == id, phraseIn);

        public void Remove(Phrase phraseIn) =>
            _phrases.DeleteOne(phrase => phrase.Id == phraseIn.Id);

        public void Remove(string id) =>
            _phrases.DeleteOne(phrase => phrase.Id == id);
    }
}
