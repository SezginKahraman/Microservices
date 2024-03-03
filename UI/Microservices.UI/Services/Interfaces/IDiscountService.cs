using Microservices.UI.Models.Discount;

namespace Microservices.UI.Services.Interfaces
{
    public interface IDiscountService
    {
        Task<DiscountViewModel> GetDiscount(string discountCode);
    }
}
