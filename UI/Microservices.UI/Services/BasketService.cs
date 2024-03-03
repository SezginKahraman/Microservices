using Microservices.Shared.Core_3_1.Dtos;
using Microservices.UI.Models.Basket;
using Microservices.UI.Services.Interfaces;

namespace Microservices.UI.Services
{
    public class BasketService : IBasketService
    {
        private readonly HttpClient _httpClient;
        private readonly IDiscountService _discountService;

        public BasketService(HttpClient httpClient, IDiscountService discountService)
        {
            _httpClient = httpClient;
            _discountService = discountService;
        }

        public async Task<bool> SaveOrUpdate(BasketViewModel basketViewModel)
        {
            var response = await _httpClient.PostAsJsonAsync<BasketViewModel>("basket", basketViewModel);

            return response.IsSuccessStatusCode;
        }

        public async Task<BasketViewModel> Get()
        {
            var response = await _httpClient.GetAsync("basket");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
           var result = await response.Content.ReadFromJsonAsync<Response<BasketViewModel>>();
           return result.Data;
        }

        public async Task<bool> Delete()
        {
            var result = await _httpClient.DeleteAsync("basket");

            return result.IsSuccessStatusCode;
        }

        public async Task AddBasketItem(BasketItemViewModel basketItemView)
        {
            var basket = await Get();

            if (basket != null)
            {
                if (!basket.BasketItems.Any(t => t.CourseId == basketItemView.CourseId))
                {
                    basket.BasketItems.Add(basketItemView);
                }
            }
            else
            {
                basket = new BasketViewModel();
                basket.BasketItems = new();
                basket.BasketItems.Add(basketItemView);
            }

            await SaveOrUpdate(basket);
        }

        public async Task<bool> RemoveBasketItem(string courseId)
        {
            var basket = await Get();
            if (basket == null) return false;
            var deleteBasketItem = basket.BasketItems.FirstOrDefault(t => t.CourseId == courseId);
            if (deleteBasketItem == null) return false;
            var deleteResult =
                basket.BasketItems.Remove(deleteBasketItem);

            if (!deleteResult) return false;

            if (!basket.BasketItems.Any())
            {
                basket.DiscountCode = null;
            }

            return await SaveOrUpdate(basket);
        }

        public async Task<bool> ApplyDiscount(string discountCode)
        {
            await CancelApplyDiscount();

            var basket = await Get();

            if (basket == null) return false;

            var hasDiscount = await _discountService.GetDiscount(discountCode);
            if (hasDiscount == null) return false;

            basket.ApplyDiscount(hasDiscount.Code, hasDiscount.Rate);
            await SaveOrUpdate(basket);
            return true;
        }

        public async Task<bool> CancelApplyDiscount()
        {
            var basket =  await Get();
            if(basket == null || basket.DiscountCode == null) return false;

            basket.CancelDiscount();

            await SaveOrUpdate(basket);
            return true;
        }
    }
}
