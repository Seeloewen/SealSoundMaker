using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Net;

namespace SealSoundMaker.Networking
{
    public static class NetworkHandler
    {
        public static readonly HttpClient client = new HttpClient()
        {
            BaseAddress = new Uri("https://jsonplaceholder.typicode.com")
        };

        public static async void Post(string endpoint, string content)
        {
            HttpResponseMessage mes = await client.PostAsync(endpoint, new StringContent(content));

            if (mes.StatusCode != HttpStatusCode.OK)
            {
                
            }
        }

        public static async void Get(string endpoint)
        {
            HttpResponseMessage mes = await client.GetAsync(endpoint);
            Debug.WriteLine(await mes.Content.ReadAsStringAsync());
        }
    }
}
