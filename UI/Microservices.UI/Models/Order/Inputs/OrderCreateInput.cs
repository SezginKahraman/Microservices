namespace Microservices.UI.Models.Order.Inputs
{
    public class OrderCreateInput
    {
        public string BuyerId { get; set; }

        public List<OrderItemCreateInput> OrderItems { get; set; }

        public AddressCreateInput Address { get; set; }
    }
}
