namespace Microservices.UI.Models.Order
{
    public class OrderCreatedViewModel
    {
        public int OrderId { get; set; } // in sencron case

        public string Error { get; set; }

        public bool IsSuccessful { get; set; }

    }
}
