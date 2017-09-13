using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace RygOgRejs.Services
{
    class WeatherApiHandler
    {
        private static readonly string apiKey = "32e7d04d86999102feb4439b0dc97b63";

        public static async string GetTemperature(string city)
        {
            string uri = $"http://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}";
            WebRequest request = WebRequest.Create(uri);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string jsonString = response.Content.Re
            }
        }
    }
}
