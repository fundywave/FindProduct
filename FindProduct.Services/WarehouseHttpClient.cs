using FindProduct.Services.Interfaces;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace FindProduct.Services
{
    public class WarehouseHttpClient<T> : IWarehouseHttpClient<T>
    {
        public async Task<T> GetAsync(string url, string parameter)
        {
            try
            {
                using var _client = new RestClient(url);
                var request = new RestRequest(parameter);
                var response = await _client.ExecuteGetAsync(request);
                if (!response.IsSuccessful)
                {
                    return Task.FromResult((T)Activator.CreateInstance(typeof(T))).Result;
                }
                return JsonConvert.DeserializeObject<T>(response.Content);
            }
            catch (AggregateException ex)
            {
                return await Task.FromResult((T)Activator.CreateInstance(typeof(T)));
            }
        }
    }
}
