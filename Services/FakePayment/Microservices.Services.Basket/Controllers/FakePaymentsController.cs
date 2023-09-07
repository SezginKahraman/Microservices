using Microservices.Shared.Core_3_1.BaseController;
using Microservices.Shared.Core_3_1.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Services.FakePayment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakePaymentsController : BaseController
    {
        [HttpPost]
        public IActionResult ReceivePayment()
        {
            return CreateActionResultInstance(Response<NoContent>.Success(200));
        }
    }
}
