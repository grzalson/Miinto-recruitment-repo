
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using MIINTOrecruitment.Models;

using static System.Net.WebRequestMethods;

namespace MIINTOrecruitment.Services
{
    public interface IOrderService
    {
        public Task<Order> GetOrderAsync(int orderId);
        public string CreateUuid(int Id);
        public Task<HttpResponseMessage> RetrieveOrder(int orderId);
    }
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;
  
        private readonly string _prefix = "8637e025-ae91-48de-7061";
        private readonly string _apiKey;
        public OrderService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["X-API-Key"];
        }

        public string CreateUuid(int Id)
        {
            var hexOrderId = string.Format("{0:x}", Id);
            return _prefix + "-" + new string('0', 12 - hexOrderId.Length) + hexOrderId;
        }
        public async Task<HttpResponseMessage> RetrieveOrder(int orderId)
        {
            var orderUuid = CreateUuid(orderId);
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("X-API-Key", _apiKey);
            var response = await _httpClient.GetAsync($"https://process-automation-test-qi27l3np3q-uc.a.run.app/api/Orders/{orderUuid}");
            return response;
        }

        public async Task<Order> GetOrderAsync(int orderId)
        {
            var response = await RetrieveOrder(orderId);
            if(!response.IsSuccessStatusCode)
            {
                return null;
            }
            var order = JsonConvert.DeserializeObject<Order>(await response.Content.ReadAsStringAsync());
            
            return order;
        }

        
    }
}
