using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.AI.Luis;
using Newtonsoft.Json.Linq;
using StoryMaker.Core.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace StoryMaker.SmartCenter.LUIS
{
    public class LanguageService
    {        
        string LuisAppId = "38bd59a9-c2a5-4cca-bb80-3bdf5c117e6e";
        string LuisAPIKey = "e932e202fb0647bbbb99e6b38c35c2de";
        string LuisAPIHostName = "https://westus.api.cognitive.microsoft.com/";

        public LanguageService()
        {                                             
                       
        }       

        private async Task<string> MakeRequest(string predictionKey, string predictionEndpoint, string appId, string phraseText)
        {
            try
            {
                var client = new HttpClient();
                var queryString = HttpUtility.ParseQueryString(string.Empty);

                // The request header contains your subscription key
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", predictionKey);

                // The "q" parameter contains the utterance to send to LUIS
                queryString["query"] = phraseText;

                // These optional request parameters are set to their default values
                queryString["verbose"] = "true";
                queryString["show-all-intents"] = "true";
                queryString["staging"] = "false";
                queryString["timezoneOffset"] = "0";

                var predictionEndpointUri = String.Format("{0}luis/prediction/v3.0/apps/{1}/slots/production/predict?{2}", predictionEndpoint, appId, queryString);
               
                var response = await client.GetAsync(predictionEndpointUri);

                return await response.Content.ReadAsStringAsync();                
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }        

        public string ManageIntentions(string intent)
        {
            var intents = MakeRequest(LuisAPIKey, LuisAPIHostName, LuisAppId, intent);
            JObject json = JObject.Parse(intents.Result);
            var topIntent = json["prediction"]["topIntent"].ToString();
            switch (topIntent)
            {
                case "Inicio":
                    break;
                case "Fin":
                    break;
                case "Pregunta":
                    break;
                case "None":
                    topIntent = "Normal";
                    break;
                case "Respuesta":
                    break;
                default:
                    break;
            }

            return topIntent;
        }

    }
}
