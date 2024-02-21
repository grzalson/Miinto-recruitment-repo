namespace MIINTOrecruitment.Services
{
    public interface IExternalApiService
    {
        public Task<HttpResponseMessage> RetrieveOrder(int orderId);
    }
}
