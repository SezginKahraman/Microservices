using MassTransit;
using Microservices.Services.FakePayment.Models;
using Microservices.Shared.Core_3_1.BaseController;
using Microservices.Shared.Core_3_1.Dtos;
using Microservices.Shared.Core_3_1.Messages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Services.FakePayment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakePaymentsController : BaseController
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public FakePaymentsController(ISendEndpointProvider sendEndpointProvider)
        {
            _sendEndpointProvider = sendEndpointProvider;
        }

        [HttpPost]
        public async Task<IActionResult> ReceivePayment(PaymentDto paymentDto)
        {
            // payment process then send to queue

            var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:create-order-service"));

            var createOrderMessageCommand = new CreateOrderMessageCommand()
            {
                BuyerId = paymentDto.Order.BuyerId,
                Address = new Address()
                {
                    District = paymentDto.Order.Address.District,
                    Line = paymentDto.Order.Address.Line,
                    Province = paymentDto.Order.Address.Province,
                    Street = paymentDto.Order.Address.Street,
                    ZipCode = paymentDto.Order.Address.ZipCode
                },
                OrderItems = paymentDto.Order.OrderItems.Select(x => new OrderItem()
                {
                    Price = x.Price,
                    PictureUrl = x.PictureUrl,
                    ProductId = x.ProductId,
                    ProductName = x.ProductName
                }).ToList()
            };

            await sendEndpoint.Send<CreateOrderMessageCommand>(createOrderMessageCommand);

            return CreateActionResultInstance(Shared.Core_3_1.Dtos.Response<NoContent>.Success(200));
        }
    }
}
