using MIINTOrecruitment.Models;

namespace MIINTOrecruitment.Services
{
    public interface IOrderService
    {
        public Task<Order?> GetOrderAsync(int orderId);
        
    }
}
