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
        private const string URL = "https://cdn-api.co-vin.in/api/v2/appointment/sessions/public/calendarByPin?pincode=844121&date=18-05-2021";

        public static void Main(string[] args)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            while (true)
            {
                Thread.Sleep(10000);
                // List data response.
                HttpResponseMessage response = client.GetAsync(URL).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = response.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<Root>(responseBody);
                    try
                    {
                        Console.WriteLine(data.centers[0].sessions[0].min_age_limit + "+ age || Available :" + data.centers[0].sessions[0].available_capacity_dose1);

                        if ((data.centers[0].sessions[0].available_capacity) != 0)
                        {
                            while (true)
                            {
                                Console.Beep();
                                Thread.Sleep(500);
                            }
                        }
                        if ((data.centers[0].sessions[0].available_capacity_dose1) != 0)
                        {
                            while (true)
                            {
                                Console.Beep();
                                Thread.Sleep(500);
                            }
                        }
                    }
                    catch
                    {
                        Console.WriteLine("200 Response Empty");
                    }
                    
                    //Console.Beep();
                }
                else
                {
                    Console.WriteLine("Error XXXXXXXXX---Wait this may be due to server issue--XXXXX");
                }

            }
        }
    }
}
