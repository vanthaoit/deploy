using LogixHealth.Eligibility.DataAccess.Common;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace LogixHealth.Eligibility.Core.Https
{
    public class HttpProviderService<T> : IHttpProviderService<T> where T : class
    {
        public HttpProviderService()
        {
        }

        public async Task<IEnumerable<T>> GetAsync(string Uri)
        {
            HttpClient client = HttpClientService(Uri);
            string apiURL = MapURL(Uri);
            HttpResponseMessage responseMessage = await client.GetAsync(apiURL);
            var responseGetAsync = DeserializeIEnumerableResponse(responseMessage);
            return responseGetAsync;
        }

        public async Task<T> GetAsync(string Uri, int id)
        {
            HttpClient client = HttpClientService(Uri);
            string apiURL = MapURL(Uri);
            HttpResponseMessage responseMessage = await client.GetAsync(apiURL);
            var responseGetAsync = DeserializeSingleResponse(responseMessage);
            return responseGetAsync;
        }

        public async Task<IEnumerable<T>> GetIEnumerableByValueAsync(string Uri)
        {
            HttpClient client = HttpClientService(Uri);
            string apiURL = MapURL(Uri);
            HttpResponseMessage responseMessage = await client.GetAsync(apiURL);
            if (responseMessage != null)
            {
                var responseGetAsync = DeserializeIEnumerableResponse(responseMessage);
                return responseGetAsync;
            }
            return Enumerable.Empty<T>();
        }

        public async Task<T> PostAsync(string Uri, T data)
        {
            HttpClient client = HttpClientService(Uri);
            string apiURL = MapURL(Uri);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(apiURL, data);
            var responsePostAsync = DeserializeSingleResponse(responseMessage);
            return responsePostAsync;
        }

        public async Task<T> PutAsync(string Uri, T data)
        {
            HttpClient client = HttpClientService(Uri);
            string apiURL = MapURL(Uri);
            HttpResponseMessage responseMessage = await client.PutAsJsonAsync(apiURL, data);
            var responsePostAsync = DeserializeSingleResponse(responseMessage);
            return responsePostAsync;
        }

        public async Task<T> DeleteSingleAsync(string Uri, T data)
        {
            HttpClient client = HttpClientService(Uri);
            string apiURL = MapURL(Uri);
            HttpResponseMessage responseMessage = await client.PutAsJsonAsync(apiURL, data);
            var responseDeleteAsync = DeserializeSingleResponse(responseMessage);
            return responseDeleteAsync;
        }

        public async Task<bool> DeleteMultiAsync(string Uri, IEnumerable<T> data)
        {
            HttpClient client = HttpClientService(Uri);
            string apiURL = MapURL(Uri);
            var lengthData = (data != null) ? data.Count() : 0;
            foreach (var item in data)
            {
                try
                {
                    await DeleteSingleAsync(Uri, item);
                }
                catch (Exception e)
                {
                    Console.WriteLine("error when delete " + e);
                    return false;
                }
            }

            return true;
        }

        private HttpClient HttpClientService(string Uri)
        {
            string apiURL = MapURL(Uri);
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(apiURL);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(SystemConstants.HEADER_CONTENT_TYPE));
            return client;
        }

        private string MapURL(string Uri)
        {
            IConfiguration configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile(SystemConstants.APP_SETTING).Build();

            string apiURL = configuration.GetValue<string>(SystemConstants.URL_API_ENDPOINT_NAME) + Uri;
            return apiURL;
        }

        private IEnumerable<T> DeserializeIEnumerableResponse(HttpResponseMessage response)
        {
            IEnumerable<T> responseMessage = Enumerable.Empty<T>();
            if (response.IsSuccessStatusCode)
            {
                var responseData = response.Content.ReadAsStringAsync().Result;
                responseMessage = JsonConvert.DeserializeObject<IEnumerable<T>>(responseData);
            }

            return responseMessage;
        }

        private T DeserializeSingleResponse(HttpResponseMessage response)
        {
            T responseMessage = (T)Activator.CreateInstance(typeof(T));
            if (response.IsSuccessStatusCode)
            {
                var responseData = response.Content.ReadAsStringAsync().Result;
                responseMessage = JsonConvert.DeserializeObject<T>(responseData);
            }

            return responseMessage;
        }
    }
}