using Microservices.UI.Models.Order.Inputs;
using Microservices.UI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.UI.Controllers
{
    public class OrderController : Controller
    {
        private readonly IBasketService _basketService;
        private readonly IOrderService _orderService;

        public OrderController(IBasketService basketService, IOrderService orderService)
        {
            _basketService = basketService;
            _orderService = orderService;
        }

        public async Task<IActionResult> Checkout()
        {
            var basket = await _basketService.Get();
            ViewBag.basket = basket;

            return View(new CheckoutInfoInput());
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutInfoInput checkoutInfo)
        {
            // senkron payment procces, first create payment, then create order
            var orderStatus = await _orderService.CreateOrder(checkoutInfo); 
            if (!orderStatus.IsSuccessful)
            {
                var basket = await _basketService.Get();
                ViewBag.basket = basket;
                ViewBag.error = orderStatus.Error;
                return RedirectToAction(nameof(Checkout));
            }

            // asenkron process, first take payment, then send order to the rabbitmq, whenever the order service is available, create order.
            var orderSuspend = await _orderService.SuspentOrder(checkoutInfo);

            return RedirectToAction(nameof(SuccesfulCheckout), new{orderId = orderStatus.OrderId});
        }

        public IActionResult SuccesfulCheckout(int orderId)
        {
            ViewBag.orderId = orderId;
            return View();
        }

        public async Task<IActionResult> CheckoutHistory()
        {
            return View(await _orderService.GetOrder());
        }
    }
}
