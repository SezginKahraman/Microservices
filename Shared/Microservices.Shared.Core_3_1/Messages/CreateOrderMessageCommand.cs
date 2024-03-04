using System;
using System.Collections.Generic;
using System.Text;

namespace Microservices.Shared.Core_3_1.Messages
{
    public class CreateOrderMessageCommand
    {
        public string BuyerId { get; set; }

        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        public Address Address { get; set; }
    }

    public class OrderItem
    {
        public string ProductId { get; set; }

        public string ProductName { get; set; }

        public Decimal Price { get; set; }

        public string PictureUrl { get; set; }
    }

    public class Address
    {
        public string Province { get; set; }

        public string District { get; set; }

        public string Street { get; set; }

        public string ZipCode { get; set; }

        public string Line { get; set; }
    }
}
