using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using MIINTOrecruitment.Models;


namespace MIINTOrecruitment.Services
{
    public interface IGoogleSheetsService
    {
        public Task AppendDataToSheet(Order order);
    }
    public class GoogleSheetsService : IGoogleSheetsService
    {
        private readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
        private readonly string AplicationName = "MyApp";
        private readonly string SpreadsheetId = "Spreadsheet ID here";
        private readonly string sheet = "Sheet name here";
        private  SheetsService service;
        public GoogleSheetsService()
        {
            GoogleCredential credential;
            using (var stream = new FileStream(@"Resources/credentials.json", FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream).CreateScoped(Scopes);
            }
            service = new SheetsService(new Google.Apis.Services.BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = AplicationName,
            });
        }
        public async Task AppendDataToSheet(Order order)
        {
            var range = $"{sheet}!A:C"; 
            var valueRange = new ValueRange();
          
            var objectList = new List<object>() { order.order_id, order.order_date, order.order_status };
            valueRange.Values = new List<IList<object>> { objectList };

            var appendRequest = service.Spreadsheets.Values.Append(valueRange, SpreadsheetId, range);
            appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
            appendRequest.InsertDataOption = SpreadsheetsResource.ValuesResource.AppendRequest.InsertDataOptionEnum.INSERTROWS;

            var response = await appendRequest.ExecuteAsync();

        }
    }
}
