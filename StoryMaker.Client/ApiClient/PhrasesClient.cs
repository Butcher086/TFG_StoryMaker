using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StoryMaker.Core.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace StoryMaker.Client.ApiClient
{
    public class PhrasesClient
    {
        private string apiBase = "https://storymakerapi.azurewebsites.net";
        //private string apiBase = "https://localhost:44378";
        private static readonly HttpClient client = new HttpClient();

        //Recupera todas las frases
        public async Task<List<Phrase>> GetPhrases()
        {
            List<Phrase> phrases = new List<Phrase>();
            try
            {
                var response = client.GetAsync(apiBase + "/api/Phrases").Result;
                var json = await response.Content.ReadAsStringAsync();
                phrases = JsonConvert.DeserializeObject<List<Phrase>>(json);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Success");
                }
                else
                {
                    Console.WriteLine("Fail");
                }

                return phrases;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //Añade una frase normal sin analizar
        public async Task<string> AddPhraseAsync(Phrase phrase)
        {
            try
            {
                var dataToSend = JsonConvert.SerializeObject(phrase);
                var buffer = System.Text.Encoding.UTF8.GetBytes(dataToSend);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = client.PostAsync(apiBase + "/api/Phrases", byteContent).Result;

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Phrase added");
                }
                else
                {
                    Console.WriteLine("Add phrase fail.");
                }

                return response.StatusCode.ToString();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<string> AddPhraseAnalitizeAsync(string phrase)
        {
            try
            {
                var dataToSend = JsonConvert.SerializeObject(phrase);
                var buffer = System.Text.Encoding.UTF8.GetBytes(dataToSend);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = client.PostAsync(apiBase + "/api/Phrases/AddAnalycedPhrase", byteContent).Result;
                
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Phrase added");                    
                }
                else
                {
                    Console.WriteLine("Add phrase fail.");
                }

                return response.StatusCode.ToString();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        public async Task AddPhrasesAsync(List<Phrase> phrases)
        {
            foreach (var phrase in phrases)
            {
                await AddPhraseAsync(phrase);
            }
        }

        public string DeletePhrase(string id)
        {
            try
            {
                var dataToSend = JsonConvert.SerializeObject(id);
                var buffer = System.Text.Encoding.UTF8.GetBytes(dataToSend);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = client.DeleteAsync(apiBase + "/api/Phrases/"+id).Result;

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Phrase added");
                }
                else
                {
                    Console.WriteLine("Add phrase fail.");
                }

                return response.StatusCode.ToString();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
