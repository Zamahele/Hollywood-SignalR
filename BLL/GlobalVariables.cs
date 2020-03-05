using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace BLL
{
    public static class GlobalVariables 
    {
        public static readonly HttpClient WebApiClient = new HttpClient(new HttpClientHandler { UseDefaultCredentials = true });

        static GlobalVariables()
        {
            WebApiClient.BaseAddress = new Uri("http://localhost:62098/api/");
            WebApiClient.DefaultRequestHeaders.Clear();
            WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}   