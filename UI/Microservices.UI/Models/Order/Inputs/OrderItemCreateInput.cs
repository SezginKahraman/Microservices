namespace Microservices.UI.Models.Order.Inputs
{
    public class OrderItemCreateInput
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public Decimal Price { get; set; }

        public string PictureUrl { get; set; }
    }
}
