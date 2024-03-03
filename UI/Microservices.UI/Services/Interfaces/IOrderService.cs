using Microservices.UI.Models.Order;
using Microservices.UI.Models.Order.Inputs;

namespace Microservices.UI.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderCreatedViewModel> CreateOrder(CheckoutInfoInput checkoutInfoInput); // senkron case

        Task SuspentOrder(CheckoutInfoInput checkoutInfoInput); // asenkron case, baseket info will be send to the rabbit mq.

        Task<List<OrderViewModel>> GetOrder();
    }
}
