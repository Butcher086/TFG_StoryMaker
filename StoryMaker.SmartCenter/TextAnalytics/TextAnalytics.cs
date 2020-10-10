using System;
using System.Collections.Generic;
using System.Text;
using Azure;
using System.Globalization;
using Azure.AI.TextAnalytics;
using StoryMaker.Core.Models;

namespace StoryMaker.SmartCenter.TextAnalytics
{
    public class TextAnalytics
    {
        private static readonly AzureKeyCredential credentials = new AzureKeyCredential("848c03c60f3c4dd48c6b3b3c0bfcba29");
        private static readonly Uri endpoint = new Uri("https://textanalyticsstorymaker.cognitiveservices.azure.com/");
        private TextAnalyticsClient client;
        

        public TextAnalytics()
        {
            if (Login())
            {
                Console.WriteLine("Login done correctly!");                
            }
            else
            {
                Console.WriteLine("Error Login :_(");
            }
            
        }

        private bool Login()
        {
            bool result = false;
            try
            {
                client = new TextAnalyticsClient(endpoint, credentials);
                result = true;
            }
            catch (Exception ex)
            {
                result = false;                                
            }                        

            return result;
        }        

        //Sentiments
        public List<Phrase> SentimentAnalysis(string textInput)
        {
            List<Phrase> phrases = new List<Phrase>();
            if (!String.IsNullOrEmpty(textInput))
            {
                DocumentSentiment documentSentiment = client.AnalyzeSentiment(textInput);
                Console.WriteLine($"Document sentiment: {documentSentiment.Sentiment}\n");

                foreach (var sentence in documentSentiment.Sentences)
                {
                    Phrase phrase = new Phrase();

                    phrase.Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
                    Console.WriteLine($"\tText: \"{sentence.Text}\"");
                    phrase.PhraseText = sentence.Text;
                    Console.WriteLine($"\tSentence sentiment: {sentence.Sentiment}");
                    phrase.Sentiment = sentence.Sentiment.ToString();
                    phrase.Lenght = sentence.Text.Length;
                    Console.WriteLine($"\tPositive score: {sentence.ConfidenceScores.Positive:0.00}");
                    phrase.PositiveScore = sentence.ConfidenceScores.Positive;
                    Console.WriteLine($"\tNegative score: {sentence.ConfidenceScores.Negative:0.00}");
                    phrase.NegativeScore = sentence.ConfidenceScores.Negative;
                    Console.WriteLine($"\tNeutral score: {sentence.ConfidenceScores.Neutral:0.00}\n");
                    phrase.NeutralScore = sentence.ConfidenceScores.Neutral;

                    phrase.Language = LanguageDetection(phrase.PhraseText);
                    phrase.NamedEntities = EntityRecognition(phrase.PhraseText);
                    phrase.KeyPhrases = KeyPhraseExtraction(phrase.PhraseText);

                    phrases.Add(phrase);
                }
            }
            return phrases;

        }        

        //Language
        private string LanguageDetection(string textInput)
        {
            DetectedLanguage detectedLanguage = client.DetectLanguage(textInput);
            Console.WriteLine("Language:");
            Console.WriteLine($"\t{detectedLanguage.Name},\tISO-6391: {detectedLanguage.Iso6391Name}\n");

            return $"\t{detectedLanguage.Name},\tISO-6391: {detectedLanguage.Iso6391Name}\n";
        }

        //Entity Recognition
        private List<NamedEntity> EntityRecognition(string textInput)
        {
            List<NamedEntity> entities = new List<NamedEntity>();
            var response = client.RecognizeEntities(textInput);
            Console.WriteLine("Named Entities:");
            foreach (var entity in response.Value)
            {
                NamedEntity namedEntity = new NamedEntity();
                Console.WriteLine($"\tText: {entity.Text},\tCategory: {entity.Category},\tSub-Category: {entity.SubCategory}");
                Console.WriteLine($"\t\tScore: {entity.ConfidenceScore:F2}\n");

                namedEntity.Id = entity.Text;
                namedEntity.Text = entity.Text;
                namedEntity.Category = entity.Category.ToString();
                namedEntity.SubCategory = entity.SubCategory;
                namedEntity.Score = entity.ConfidenceScore;


                entities.Add(namedEntity);
            }

            return entities;
        }

        private List<KeyPhrase> KeyPhraseExtraction(string textInput)
        {
            List<KeyPhrase> keys = new List<KeyPhrase>();
            var response = client.ExtractKeyPhrases(textInput);

            // Printing key phrases
            Console.WriteLine("Key phrases:");

            foreach (string keyphrase in response.Value)
            {
                KeyPhrase key = new KeyPhrase();
                Console.WriteLine($"\t{keyphrase}");
                key.Id = keyphrase;
                key.Text= keyphrase;

                keys.Add(key);
            }

            return keys;
        }   
        
        private string SetPhraseType()
        {
            return "";
        }

    }
}
