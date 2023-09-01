using Microservices.Services.Order.Domain.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Services.Order.Application.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }

        public DateTime Created { get; set; }

        public AddressDto Address { get; set; } // if address is marked as [Owned], ef core is going to create entire new table for the address. Else, every property of address will be created a column for order.

        public string BuyerId { get; set; }

        private List<OrderItemDto> OrderItems { get; set; }
    }
}
