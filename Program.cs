using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;

namespace OpenApiCheck
{
    class Program
    {
        private const string URL = "https://cdn-api.co-vin.in/api/v2/appointment/sessions/calendarByPin?pincode=844121&date=17-05-2021";

        public static void Main(string[] args)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            while (true)
            {
                Thread.Sleep(5000);
                // List data response.
                HttpResponseMessage response = client.GetAsync(URL).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = response.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<Root>(responseBody);
                    Console.WriteLine(data.centers[0].sessions[0].min_age_limit +" "+ data.centers[0].sessions[0].slots[0]);

                    //Console.Beep();
                }
                else
                {
                    Console.WriteLine("Error XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
                }

            }
        }
    }
}
