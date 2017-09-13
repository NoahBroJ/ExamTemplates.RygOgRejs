using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using RygOgRejs.Entities;

namespace RygOgRejs.Services
{
    class WeatherApiHandler
    {
        static HttpClientHandler handler = new HttpClientHandler();
        static HttpClient client;
        private static string apiKey = "32e7d04d86999102feb4439b0dc97b63";

        public static async Task<Temperature> GetTemperature(string city)
        {
            Temperature temp = null;
            using (client = new HttpClient(handler, false))
            {
                string uri = $"http://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}";
                try
                {
                    HttpResponseMessage response = await client.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                        temp = await response.Content.ReadAsAsync<Temperature>();
                }
                catch (Exception ex)
                {

                }
            }
            return temp;
        }
    }
}
