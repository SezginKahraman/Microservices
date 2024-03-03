using Microservices.UI.Models.FakePayments;

namespace Microservices.UI.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<bool> ReceivePayment(PaymentInfoInput paymentInfoInput);
    }
}
