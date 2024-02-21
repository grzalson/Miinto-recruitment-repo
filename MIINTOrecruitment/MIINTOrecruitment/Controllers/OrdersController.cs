using Microsoft.AspNetCore.Mvc;
using MIINTOrecruitment.Models;
using MIINTOrecruitment.Services;

namespace MIINTOrecruitment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IGoogleSheetsService _googleSheetsService;
        private readonly IFormSubmissionService _formSubmissionService;

        public OrdersController(IOrderService orderService, IGoogleSheetsService googleSheetsService, IFormSubmissionService formSubmissionService)
        {
            _orderService = orderService;
            _googleSheetsService = googleSheetsService;
            _formSubmissionService = formSubmissionService;
        }

        [HttpGet("getOrder/{orderId}")]
        public async Task<ActionResult<Order>> GetOrder(int orderId)
        {
            var order = await _orderService.GetOrderAsync(orderId);
            if (order == null)
            {
                return NotFound("Order with given ID was not found.");
            }
            await _googleSheetsService.AppendDataToSheet(order);
            await _formSubmissionService.SubmitForm(order);
            return Ok(order);
        }
    }
}
