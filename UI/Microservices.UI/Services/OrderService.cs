using System.Drawing.Text;
using Microservices.Shared.Core_3_1.Dtos;
using Microservices.Shared.Services;
using Microservices.UI.Models.FakePayments;
using Microservices.UI.Models.Order;
using Microservices.UI.Models.Order.Inputs;
using Microservices.UI.Services.Interfaces;

namespace Microservices.UI.Services
{
    public class OrderService : IOrderService
    {
        private readonly IPaymentService _paymentService;
        private readonly IBasketService _basketService;
        private readonly HttpClient _httpClient;
        private readonly ISharedIdentityService _sharedIdentityService;

        public OrderService(IPaymentService paymentService, IBasketService basketService, HttpClient httpClient, ISharedIdentityService sharedIdentityService)
        {
            _paymentService = paymentService;
            _basketService = basketService;
            _httpClient = httpClient;
            _sharedIdentityService = sharedIdentityService;
        }
        public async Task<OrderCreatedViewModel> CreateOrder(CheckoutInfoInput checkoutInfoInput)
        {
            var basket = await _basketService.Get();
            var payment = new PaymentInfoInput()
            {
                CardName = checkoutInfoInput.CardName,
                CardNumber = checkoutInfoInput.CardNumber,
                CVV = checkoutInfoInput.CVV,
                Expiration = checkoutInfoInput.Expiration,
                TotalPrice = basket.TotalPrice
            };

            var responsePayment = await _paymentService.ReceivePayment(payment);

            if (!responsePayment) return new OrderCreatedViewModel() { Error = "Ödeme Alınamadı", IsSuccessful = false };

            var orderCreateInput = new OrderCreateInput()
            {
                BuyerId = _sharedIdentityService.GetUserId,
                Address = new AddressCreateInput()
                {
                    Province = checkoutInfoInput.Province,
                    District = checkoutInfoInput.District,
                    Line = checkoutInfoInput.Line,
                    Street = checkoutInfoInput.Street,
                    ZipCode = checkoutInfoInput.ZipCode
                },
                OrderItems = basket.BasketItems.Select(t => new OrderItemCreateInput()
                {
                    ProductId = int.Parse(t.CourseId),
                    Price = t.GetCurrentPrice, // discounted price on total price
                    PictureUrl = "",
                    ProductName = t.CourseName
                }).ToList()
            };
            var orderResponse = await _httpClient.PostAsJsonAsync<OrderCreateInput>("orders", orderCreateInput);

            if(!orderResponse.IsSuccessStatusCode)return new OrderCreatedViewModel() { Error = "Sipariş oluşturulamadı.", IsSuccessful = false };

            var orderCreatedViewModel = await orderResponse.Content.ReadFromJsonAsync<Response<OrderCreatedViewModel>>();
            orderCreatedViewModel.Data.IsSuccessful = true;
            _basketService.Delete();
            return orderCreatedViewModel.Data;
        }

        public async Task SuspentOrder(CheckoutInfoInput checkoutInfoInput)
        {
            throw new NotImplementedException();
        }

        public async Task<List<OrderViewModel>> GetOrder()
        {
            var response = await _httpClient.GetFromJsonAsync<Response<List<OrderViewModel>>>("orders");
            return response.Data;
        }
    }
}
