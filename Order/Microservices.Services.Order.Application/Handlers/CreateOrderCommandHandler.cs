using MediatR;
using Microservices.Services.Order.Application.Commands;
using Microservices.Services.Order.Application.Dtos;
using Microservices.Services.Order.Application.Queries;
using Microservices.Services.Order.Domain.OrderAggregate;
using Microservices.Services.Order.Infrastructure;
using Microservices.Shared.Core_3_1.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Microservices.Services.Order.Application.Handlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Response<CreatedOrderDto>>
    {
        private readonly OrderDbContext _context;

        public CreateOrderCommandHandler(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<Response<CreatedOrderDto>> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            var address = new Address(command.AddressDto.Province, command.AddressDto.District, command.AddressDto.Street, command.AddressDto.ZipCode, command.AddressDto.Line);
            Domain.OrderAggregate.Order newOrder = new Domain.OrderAggregate.Order(address, command.BuyerId);
            command.OrderItems.ForEach(x =>
            {
                newOrder.AddOrderItem(x.ProductId, x.ProductName, x.Price, x.PictureUrl);
            });
            await _context.Orders.AddAsync(newOrder);
            var result = await _context.SaveChangesAsync();

            return Response<CreatedOrderDto>.Success(new CreatedOrderDto { OrderId = newOrder.Id }, 200);
        }
    }
}
