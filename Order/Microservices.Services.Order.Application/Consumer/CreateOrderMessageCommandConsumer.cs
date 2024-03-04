using MassTransit;
using Microservices.Services.Order.Domain.OrderAggregate;
using Microservices.Services.Order.Infrastructure;
using Microservices.Shared.Core_3_1.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Services.Order.Application.Consumer
{
    public class CreateOrderMessageCommandConsumer : IConsumer<CreateOrderMessageCommand>
    {
        private readonly OrderDbContext _dbContext;

        public CreateOrderMessageCommandConsumer(OrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Consume(ConsumeContext<CreateOrderMessageCommand> context)
        {
            var newAddress = new Domain.OrderAggregate.Address(context.Message.Address.Province, context.Message.Address.District, context.Message.Address.Street, context.Message.Address.ZipCode, context.Message.Address.Line);
            Domain.OrderAggregate.Order order = new Domain.OrderAggregate.Order(newAddress, context.Message.BuyerId);
            context.Message.OrderItems.ForEach(t =>
            {
                order.AddOrderItem(t.ProductId, t.ProductName, t.Price, t.PictureUrl);
            });
            await _dbContext.Orders.AddAsync(order);

            await _dbContext.SaveChangesAsync();
        }
    }
}
