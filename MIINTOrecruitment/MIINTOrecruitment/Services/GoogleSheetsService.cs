using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using MIINTOrecruitment.Models;
using OpenQA.Selenium.DevTools.V120.WebAuthn;
using System.Text.Json;

namespace MIINTOrecruitment.Services
{
    
    public class GoogleSheetsService : IGoogleSheetsService
    {
        private readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
        private readonly string _AplicationName = "MyApp";
        private readonly string _SpreadsheetId;
        private readonly string _sheet = "Arkusz1";
        private readonly GoogleCredentials _googleCredentials;
        private SheetsService service;
        public GoogleSheetsService(IConfiguration configuration)
        {
            _SpreadsheetId = configuration["SpreadsheetId"];

            _googleCredentials = new GoogleCredentials()
            {
                auth_uri = configuration["GoogleCredentials:auth_uri"],
                type = configuration["GoogleCredentials:type"],
                project_id = configuration["GoogleCredentials:project_id"],
                private_key_id = configuration["GoogleCredentials:private_key_id"],
                private_key = configuration["GoogleCredentials:private_key"],
                client_email = configuration["GoogleCredentials:client_email"],
                client_id = configuration["GoogleCredentials:client_id"],
                token_uri = configuration["GoogleCredentials:token_uri"],
                auth_provider_x509_cert_url = configuration["GoogleCredentials:auth_provider_x509_cert_url"],
                client_x509_cert_url = configuration["GoogleCredentials:client_x509_cert_url"],
                universe_domain = configuration["universe_domain"]
            };

            GoogleCredential credential = GoogleCredential.FromJson(JsonSerializer.Serialize(_googleCredentials));
            
            service = new SheetsService(new Google.Apis.Services.BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = _AplicationName,
            });
        }
        public async Task AppendDataToSheet(Order order)
        {
            var range = $"{_sheet}!A:C"; 
            var valueRange = new ValueRange();
          
            var objectList = new List<object>() { order.order_id, order.order_date, order.order_status };
            valueRange.Values = new List<IList<object>> { objectList };

            var appendRequest = service.Spreadsheets.Values.Append(valueRange, _SpreadsheetId, range);
            appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
            appendRequest.InsertDataOption = SpreadsheetsResource.ValuesResource.AppendRequest.InsertDataOptionEnum.INSERTROWS;

            await appendRequest.ExecuteAsync();

        }

    }
}
