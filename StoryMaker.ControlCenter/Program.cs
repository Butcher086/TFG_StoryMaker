using StoryMaker.Client.ApiClient;
using StoryMaker.Core.Models;
using StoryMaker.SmartCenter.TextAnalytics;
using System;
using System.Collections.Generic;

namespace StoryMaker.ControlCenter
{
    public class Program
    {
        private static PhrasesClient phraseClient = new PhrasesClient();
        private static HistoriesClient historiesClient = new HistoriesClient();
        private static TextAnalytics textAnalytics = new TextAnalytics();
        static void Main(string[] args)
        {
            Console.WriteLine("Starting the text analysis");


            //textAnalytics.SentimentAnalysis(inputText);
            //textAnalytics.LanguageDetection(inputText);
            //textAnalytics.EntityRecognition(inputText);
            //textAnalytics.KeyPhraseExtraction(inputText);

            //AddPhrase
            //Console.WriteLine("Please, insert frase: ");
            //var phrase = Console.ReadLine();
            //AddPhraseAnalizeTest(phrase);

            //Delete phrase
            //Console.WriteLine("Please, insert frase id para delete: ");
            //var id = Console.ReadLine();
            //DeletePhrase(id);

            //Get Phrases
            //GetPhrases();

            //Get Histories
            //GetHistories();

            //AddHistory
            List<string> phrases = new List<string>();
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("Please, insert frase: ");
                var phrase = Console.ReadLine();
                phrases.Add(phrase);
            }                        
            AddHistory(phrases);
            //DeleteHistory
        }        

        private async static void AddPhraseTest()
        {
            String texto = @"Siento comunicarte que la protagonista ha muerto.";
            var result = textAnalytics.SentimentAnalysis(texto);

            await phraseClient.AddPhrasesAsync(result);
        }

        private async static void AddPhraseAnalizeTest()
        {
            String texto = @"Por ahora todo va bien. No te preocues, volveré sano y salvo";            

            await phraseClient.AddPhraseAnalitizeAsync(texto);
        }

        private static void DeletePhrase(string id)
        {
            phraseClient.DeletePhrase(id);
        }

        private static void GetPhrases()
        {
            var phrases = phraseClient.GetPhrases().Result;

            foreach (var p in phrases)
            {
                Console.WriteLine(p.Id);
                Console.WriteLine(p.PhraseText);
            }
        }

        private static void GetHistories()
        {
            var histories = historiesClient.GetHistories().Result;

            foreach (var h in histories)
            {
                Console.WriteLine(h.Id);
                Console.WriteLine(h.Type);
            }
        }

        private static void AddHistory(List<string> phrases)
        {
            var result = historiesClient.AddHistory(phrases);

            Console.WriteLine(result);
        }
    }
}
