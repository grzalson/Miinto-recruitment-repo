using MIINTOrecruitment.Models;

namespace MIINTOrecruitment.Services
{
    public interface IGoogleSheetsService
    {
        public Task AppendDataToSheet(Order order);
    }
}
