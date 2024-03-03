using Microservices.Services.Order.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Services.Order.Domain.OrderAggregate
{
    public class Order : Entity, IAggregateRoot
    {
        public Order()
        {

        }
        public DateTime Created { get; private set; }
        
        public Address Address{ get; private set; } // if address is marked as [Owned], ef core is going to create entire new table for the address. Else, every property of address will be created a column for order.
       
        public string BuyerId{ get; private set; }

        private readonly List<OrderItem> _orderItems;

        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

        public Order(Address address, string buyerId)
        {
            Created = DateTime.Now;
            Address = address;
            BuyerId = buyerId;
            _orderItems = new List<OrderItem>();
        }

        public void AddOrderItem(string productId, string productName, decimal price, string pictureUrl)
        {
            var existsProduct = _orderItems.Any(x => x.ProductId == productId);

            if (!existsProduct)
            {
                var newOrderItem = new OrderItem(productId, productName, price, pictureUrl);
                _orderItems.Add(newOrderItem);
            }
        }

        public decimal GetTotalPrice => _orderItems.Sum(t => t.Price);
    }
}
