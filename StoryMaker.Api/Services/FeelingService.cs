using MongoDB.Driver;
using StoryMaker.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoryMaker.Api.Services
{
    public class FeelingService
    {

        private readonly IMongoCollection<Feeling> _feeling;

        public FeelingService(IStoryMakerDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _feeling = database.GetCollection<Feeling>(settings.FeelingsCollectionName);
        }

        public List<Feeling> Get() =>
            _feeling.Find<Feeling>(feeling => true).ToList();

        public Feeling Get(string id) =>
            _feeling.Find<Feeling>(feeling => feeling.Id == id).FirstOrDefault();

        public Feeling Create(Feeling feeling)
        {
            _feeling.InsertOne(feeling);
            return feeling;
        }

        public void Update(string id, Feeling feelingIn) =>
            _feeling.ReplaceOne(phrase => phrase.Id == id, feelingIn);

        public void Remove(Feeling feelingIn) =>
            _feeling.DeleteOne(phrase => phrase.Id == feelingIn.Id);

        public void Remove(string id) =>
            _feeling.DeleteOne(feeling => feeling.Id == id);
    }
}
