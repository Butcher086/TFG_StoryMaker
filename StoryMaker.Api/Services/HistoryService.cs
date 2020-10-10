using MongoDB.Driver;
using StoryMaker.Core.Models;
using StoryMaker.SmartCenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoryMaker.Api.Services
{
    public class HistoryService
    {
        private readonly IMongoCollection<History> _histories;
        private SmarCenter smartCenter;
        private PhraseService _phraseService;

        public HistoryService(IStoryMakerDatabaseSettings settings, PhraseService phraseService)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _histories = database.GetCollection<History>(settings.HistoriesCollectionName);
            _phraseService = phraseService;

            smartCenter = new SmarCenter();
        }

        public List<History> Get() =>
            _histories.Find<History>(history => true).ToList();

        public History Get(string id) =>
            _histories.Find<History>(history => history.Id == id).FirstOrDefault();

        public History Create(History history)
        {
            try
            {
                _histories.InsertOne(history);
                return history;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public void Update(string id, History historyIn) =>
            _histories.ReplaceOne(history => history.Id == id, historyIn);

        public void Remove(History historyIn) =>
            _histories.DeleteOne(history => history.Id == historyIn.Id);

        public void Remove(string id) =>
            _histories.DeleteOne(history => history.Id == id);

        public History GetRandomHistory()
        {
            var histories = _histories.Find<History>(history => true).ToList();
            Random rnd = new Random();
            return histories[rnd.Next(0, histories.Count-1)];
        }

        public History CreateHistoryByPhrases(List<Phrase> phrases)
        {
            List<HistoryPhrase> historyPhrases = new List<HistoryPhrase>();

            for (int i = 0; i < phrases.Count; i++)
            {
                HistoryPhrase hp = new HistoryPhrase()
                {
                    PhraseID = phrases[i].Id,
                    Order = i
                };
                historyPhrases.Add(hp);
            }

            History history = new History()
            {
                Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString(),
                Type = smartCenter.CalculateTypePhrases(phrases),
                Phrases = historyPhrases
            };

            if (!String.IsNullOrEmpty(history.ToString()))
            {
                _histories.InsertOne(history);
                
            }
            return history;
        }

        public History AddHistoryByPhrases(List<string> phrases)
        {            
            var newPhrases = smartCenter.StartPhraseProcess(phrases);
            return CreateHistoryByPhrases(newPhrases);
        }

        public List<string> JoinHistoryPhrases(History history)
        {
            List<string> result = new List<string>();
            if (history.Phrases.Count>0)
            {
                foreach (var h in history.Phrases)
                {
                    var phrase =_phraseService.Get(h.PhraseID);
                    if (phrase != null)
                    {
                        result.Add(phrase.PhraseText);
                    }                    
                }
            }            

            return result;
        }
    }
}
