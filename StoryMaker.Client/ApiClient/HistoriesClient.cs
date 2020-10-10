using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StoryMaker.Core.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace StoryMaker.Client.ApiClient
{
    public class HistoriesClient
    {
        private string apiBase = "https://storymakerapi.azurewebsites.net";
        //private string apiBase = "https://localhost:44378";
        private static readonly HttpClient client = new HttpClient();

        public HistoriesClient()
        {

        }

        //Recupera todas las historias
        public async Task<List<History>> GetHistories()
        {
            List<History> histories = new List<History>();
            try
            {                
                var response = client.GetAsync(apiBase + "/api/Histories").Result;
                var json = await response.Content.ReadAsStringAsync();
                histories = JsonConvert.DeserializeObject<List<History>>(json);                

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Success");
                }
                else
                {
                    Console.WriteLine("Fail");
                }

                return histories;
            }
            catch (Exception ex)
            {

                throw ex;
            }            
        }

        //Crea una historia nueva
        public async Task<string> AddHistory(History history)
        {
            try
            {
                var dataToSend = JsonConvert.SerializeObject(history);
                var buffer = System.Text.Encoding.UTF8.GetBytes(dataToSend);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = client.PostAsync(apiBase + "/api/Histories", byteContent).Result;

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("History added");
                }
                else
                {
                    Console.WriteLine("Add history fail.");
                }

                return response.StatusCode.ToString();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //Crea una historia nueva segun las frases tipo Phrase
        public async Task<string> AddHistory(List<Phrase> phrases)
        {
            try
            {
                var dataToSend = JsonConvert.SerializeObject(phrases);
                var buffer = System.Text.Encoding.UTF8.GetBytes(dataToSend);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = client.PostAsync(apiBase + "/api/Histories/CreateHistoryByPhrases", byteContent).Result;

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("History added");
                }
                else
                {
                    Console.WriteLine("Add history fail.");
                }

                return response.StatusCode.ToString();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //Crea una historia nueva segun las frases tipo texto
        public async Task<string> AddHistory(List<string> phrases)
        {
            try
            {
                var dataToSend = JsonConvert.SerializeObject(phrases);
                var buffer = System.Text.Encoding.UTF8.GetBytes(dataToSend);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = client.PostAsync(apiBase + "/api/Histories/AddHistoryByPhrases", byteContent).Result;

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("History added");
                }
                else
                {
                    Console.WriteLine("Add history fail.");
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
