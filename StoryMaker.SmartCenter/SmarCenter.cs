using StoryMaker.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StoryMaker.SmartCenter
{
    public class SmarCenter
    {
        public StoryMaker.SmartCenter.TextAnalytics.TextAnalytics textAnalytics;
        public StoryMaker.SmartCenter.LUIS.LanguageService languageService;

        public SmarCenter()
        {
            textAnalytics = new StoryMaker.SmartCenter.TextAnalytics.TextAnalytics();
            languageService = new StoryMaker.SmartCenter.LUIS.LanguageService();
        }

        public List<Phrase> StartPhraseProcess(List<string> phrases)
        {
            List<Phrase> analisis = new List<Phrase>();
            string completeText = "";
            if (phrases.Count>0)
            {
                foreach (var phrase in phrases)
                {
                    completeText += phrase + ". ";
                }

                analisis = textAnalytics.SentimentAnalysis(completeText);

                foreach (var p in analisis)
                {
                    p.PhraseType = languageService.ManageIntentions(p.PhraseText).ToString();
                }
            }

            return analisis;
            
        }

        public string CalculateTypePhrases(List<Phrase> phrases)
        {
            var negSum = 0.0;
            var posSum = 0.0;
            var neuSum = 0.0;
            foreach (var phrase in phrases)
            {
                negSum += phrase.NegativeScore;
                neuSum += phrase.NeutralScore;
                posSum += phrase.PositiveScore;
            }
            var total = posSum - negSum;

            if (total <= 0.5)
            {
                //Drama
                if (total >= neuSum)
                {
                    return "Drama";
                }
                else //Terror
                {
                    return "Terror";
                }
                
            }
            else
            {
                //Comedia
                if (total >= neuSum)
                {
                    return "Comedia";
                }
                else //Suspense
                {
                    return "Suspense";
                }
                
            }
        }
    }
}
