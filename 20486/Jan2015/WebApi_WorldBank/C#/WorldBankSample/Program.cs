using Newtonsoft.Json.Linq;
using System;
//using System.Json;
using System.Net.Http;

namespace WorldBankSample
{
    /// <summary>
    /// Sample download list of countries from the World Bank Data sources at http://data.worldbank.org/
    /// http://blogs.msdn.com/b/henrikn/archive/2012/02/11/httpclient-is-here.aspx
    /// http://code.msdn.microsoft.com/Introduction-to-HttpClient-4a2d9cee
    /// </summary>
    class Program
    {
        static string _address = "http://api.worldbank.org/countries?format=json";

        static void Run1()
        {
            // Create an HttpClient instance
            HttpClient client = new HttpClient();

            // Send a request asynchronously continue when complete
            client.GetAsync(_address).ContinueWith(
                (requestTask) =>
                {
                    // Get HTTP response from completed task.
                    HttpResponseMessage response = requestTask.Result;

                    // Check that response was successful or throw exception
                    response.EnsureSuccessStatusCode();

                    // Read response asynchronously as JsonValue and write out top facts for each country
                    response.Content.ReadAsAsync<JArray>().ContinueWith(
                        (readTask) =>
                        {
                            Console.WriteLine("First 50 countries listed by The World Bank...");
                            foreach (var country in readTask.Result[1])
                            {
                                Console.WriteLine("   {0}, Country Code: {1}, Capital: {2}, Latitude: {3}, Longitude: {4}",
                                    country["name"],
                                    country["iso2Code"],
                                    country["capitalCity"],
                                    country["latitude"],
                                    country["longitude"]);
                            }
                        });
                });

            Console.WriteLine("Hit ENTER to exit...");
            Console.ReadLine();
        }

        static async void Run2()
        {
            // Create an HttpClient instance
            HttpClient client = new HttpClient();
            // Send a request asynchronously continue when complete
            HttpResponseMessage response = await client.GetAsync(_address);
            // Check that response was successful or throw exception
            response.EnsureSuccessStatusCode();
            // Read response asynchronously as JsonValue and write out top facts for each country
            JArray content = await response.Content.ReadAsAsync<JArray>();
            Console.WriteLine("First 50 countries listed by The World Bank...");
            foreach (var country in content[1])
            {
                Console.WriteLine("   {0}, Country Code: {1}, Capital: {2}, Latitude: {3}, Longitude: {4}",
                country["name"],
                country["iso2Code"],
                country["capitalCity"],
                country["latitude"],
                country["longitude"]);
            }
        }

        static void Main(string[] args)
        {
            //Run1();
            Run2();
            Console.WriteLine("Hit ENTER to exit...");
            Console.ReadLine();
        }
    }
}
