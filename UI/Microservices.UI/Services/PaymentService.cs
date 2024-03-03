using Microservices.UI.Models.FakePayments;
using Microservices.UI.Services.Interfaces;

namespace Microservices.UI.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly HttpClient _httpClient;

        public PaymentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> ReceivePayment(PaymentInfoInput paymentInfoInput)
        {
            var response = await _httpClient.PostAsJsonAsync<PaymentInfoInput>("fakepayments", paymentInfoInput);
            return response.IsSuccessStatusCode;
        }
    }
}
