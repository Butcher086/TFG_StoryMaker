using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoryMaker.Core.Models
{
    public class StoryMakerDatabaseSettings : IStoryMakerDatabaseSettings
    {
        public string PhrasesCollectionName { get; set; }
        public string FeelingsCollectionName { get; set; }
        public string HistoriesCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IStoryMakerDatabaseSettings
    {
        string PhrasesCollectionName { get; set; }
        string FeelingsCollectionName { get; set; }
        string HistoriesCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
