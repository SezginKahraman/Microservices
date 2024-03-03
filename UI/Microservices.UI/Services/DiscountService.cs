using Microservices.Shared.Core_3_1.Dtos;
using Microservices.UI.Models.Discount;
using Microservices.UI.Services.Interfaces;

namespace Microservices.UI.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly HttpClient _httpClient;

        public DiscountService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<DiscountViewModel> GetDiscount(string discountCode)
        {
            var response = await _httpClient.GetAsync($"discounts/getbycode/{discountCode}");

            if (!response.IsSuccessStatusCode) return null;

            var discount = await response.Content.ReadFromJsonAsync<Response<DiscountViewModel>>();
            return discount.Data;
        }
    }
}
