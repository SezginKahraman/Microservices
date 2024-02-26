using Microservices.UI.Models.Basket;
using Microservices.UI.Services.Interfaces;

namespace Microservices.UI.Services
{
    public class BasketService : IBasketService
    {
        private readonly HttpClient _httpClient;

        public BasketService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<bool> SaveOrUpdate(BasketViewModel basketViewModel)
        {
            throw new NotImplementedException();
        }

        public Task<BasketViewModel> Get()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete()
        {
            throw new NotImplementedException();
        }

        public Task AddBasketItem(BasketItemViewModel basketItemView)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveBasketItem(string courseId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ApplyDiscount(string discountCode)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CancelApplyDiscount()
        {
            throw new NotImplementedException();
        }
    }
}
