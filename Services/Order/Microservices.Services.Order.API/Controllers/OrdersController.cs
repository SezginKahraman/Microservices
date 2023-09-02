using MediatR;
using Microservices.Services.Order.Application.Commands;
using Microservices.Services.Order.Application.Queries;
using Microservices.Shared.Core_3_1.BaseController;
using Microservices.Shared.Services;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Services.Order.API.Controllers
{
    [Route("api/[controller]")]
    public class OrdersController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly ISharedIdentityService _identityService;

        public OrdersController(IMediator mediator, ISharedIdentityService identityService)
        {
            _identityService = identityService;
            _mediator = mediator;

        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var response = await _mediator.Send(new GetOrdersByUserIdQuery() { UserId = _identityService.GetUserId});

            return CreateActionResultInstance(response);
        }

        [HttpPost]
        public async Task<IActionResult> SaveOrder(CreateOrderCommand command)
        {
            var response = await _mediator.Send(command);

            return CreateActionResultInstance(response);
        }

    }
}
