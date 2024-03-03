using Microservices.Services.Order.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Services.Order.Domain.OrderAggregate
{
    public class OrderItem : Entity
    {
        public string ProductId { get; private set; }

        public string ProductName { get; private set; }

        public Decimal Price { get; private set; }

        public string PictureUrl { get; private set; }

        //public int OrderId { get; private set; } // shadow property, there is a equaliviliant in database but not in the code.

        public OrderItem(string productId, string productName, decimal price, string pictureUrl)
        {
            ProductId = productId;
            ProductName = productName;
            Price = price;
            PictureUrl = pictureUrl;
        }

        public void UpdateOrderItem(string productName, string pictureUrl, decimal price)
        {
            ProductName = productName;
            PictureUrl = pictureUrl;
            Price = price;
        }
    }
}
