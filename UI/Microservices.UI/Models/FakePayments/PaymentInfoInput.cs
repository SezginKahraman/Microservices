using Microservices.UI.Models.Order.Inputs;

namespace Microservices.UI.Models.FakePayments
{
    public class PaymentInfoInput
    {
        public string CardName { get; set; }

        public string CardNumber { get; set; }

        public string Expiration { get; set; }

        public string CVV { get; set; }

        public decimal TotalPrice{ get; set; }

        public OrderCreateInput Order { get; set; }
    }
}
