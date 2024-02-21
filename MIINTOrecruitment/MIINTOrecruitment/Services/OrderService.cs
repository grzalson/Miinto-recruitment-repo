using MIINTOrecruitment.Models;
using Newtonsoft.Json;

namespace MIINTOrecruitment.Services
{

    public class OrderService : IOrderService
    {
        private readonly IExternalApiService _externalApiService;
        public OrderService(IExternalApiService externalApiService)
        {
            _externalApiService = externalApiService;
        }

        public async Task<Order?> GetOrderAsync(int orderId)
        {
            var response = await _externalApiService.RetrieveOrder(orderId);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var order = JsonConvert.DeserializeObject<Order>(await response.Content.ReadAsStringAsync());

            return order;
        }


    }
}
