namespace MIINTOrecruitment.Services
{
    public class ExternalApiService : IExternalApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _prefix;
        private readonly string _apiKey;
        private readonly string _endpointUri;
        public ExternalApiService(HttpClient httpClient, IConfiguration configuration)
        {
            _prefix = configuration["Prefix"];
            _httpClient = httpClient;
            _apiKey = configuration["X-API-Key"];
            _endpointUri = configuration["EndpointUri"];
        }

        private string CreateUuid(int Id)
        {
            var hexOrderId = string.Format("{0:x}", Id);
            return _prefix + "-" + new string('0', 12 - hexOrderId.Length) + hexOrderId;
        }
        public async Task<HttpResponseMessage> RetrieveOrder(int orderId)
        {
            var orderUuid = CreateUuid(orderId);
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("X-API-Key", _apiKey);
            var response = await _httpClient.GetAsync($"{_endpointUri}{orderUuid}");
            return response;
        }
    }
}
