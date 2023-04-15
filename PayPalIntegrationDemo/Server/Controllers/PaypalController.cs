using Microsoft.AspNetCore.Mvc;
using PayPalIntegrationDemo.Server.Model;
using PayPalIntegrationDemo.Server.Services;

namespace PayPalIntegrationDemo.Server.Controllers
{
    [ApiController]
    [Route("paypal")]
    public class PaypalController : ControllerBase
    {
        private readonly PaypalService paypalService;

        public PaypalController(PaypalService paypalService)
        {
            this.paypalService = paypalService;
        }

        [HttpPost("create-order")]
        public async Task<ActionResult> CreateOrder(CreateOrderRequest order)
        {
            var res = await paypalService.CreateOrder(order);

            return Ok(res);
        }

        [HttpPost("capture-order")]
        public async Task<ActionResult> CaptureOrder(CaptureOrderRequest capture)
        {
            var res = await paypalService.CaptureOrder(capture);

            return Ok(res);
        }
    }
}
