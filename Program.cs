using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;

namespace OpenApiCheck
{
    class Program
    {
        static string pin = "844121";
        static string date = "21-05-2021";
        static string URL = "https://cdn-api.co-vin.in/api/v2/appointment/sessions/public/findByPin?pincode="+pin+ "&date="+date;

        public static void Main(string[] args)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            while (true)
            {
                Thread.Sleep(5000);

                HttpResponseMessage response = client.GetAsync(URL).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
                
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = response.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<Root>(responseBody);
                    try
                    {
                        //as per json session[2] is having data for 18+
                        Console.WriteLine(data.sessions[2].min_age_limit + "+ age || Available :" + data.sessions[2].available_capacity_dose1 +" || check at -" +DateTime.Now);
                        //Console.Beep();  //comment it for second time a day
                        if ((data.sessions[2].available_capacity) != 0)
                        {
                            Console.WriteLine("---------------------------------------------------------");
                            Console.Beep();
                            //while (true)
                            //{
                            //    Console.Beep();
                            //    Thread.Sleep(500);
                            //}
                        }
                        if ((data.sessions[2].available_capacity_dose1) != 0)
                        {
                            Console.WriteLine("---------------------------------------------------------");
                            Console.Beep();
                            //while (true)
                            //{
                            //    Console.Beep();
                            //    Thread.Sleep(500);
                            //}
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Available: "+data.sessions.Count+" ||Api Status:"+response.StatusCode+" ||Time-"+DateTime.Now);
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
