namespace Microservices.UI.Models.Order
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        public DateTime Created { get; set; }

        public string BuyerId { get; set; }

        public List<OrderItemViewModel> OrderItems { get; set; }
    }
}
