using MIINTOrecruitment.Models;

namespace MIINTOrecruitment.Services
{
    public interface IFormSubmissionService
    {
        public Task SubmitForm(Order order);
    }
}
