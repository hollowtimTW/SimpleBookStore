using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SimpleBookStore.Models;
using SimpleBookStore.Service.IService;
using SimpleBookStore.Utility;
using Stripe;

namespace SimpleBookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await _orderService.GetOrdersHeaderAsync();
            return View(orders);
        }

        public async Task<IActionResult> Detail(int orderId)
        {
            var order = await _orderService.GetOrderAsync(orderId);

            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> ToggleStatus(int orderId)
        {
            var order = await _orderService.GetOrderAsync(orderId);

            // 依現有狀態決定下一步
            var nextStatus = order.Status;
            switch (order.Status)
            {
                case SD.Status_Pending:
                    nextStatus = SD.Status_Paid;
                    break;
                case SD.Status_Paid:
                    nextStatus = SD.Status_Processing;
                    break;
                case SD.Status_Processing:
                    nextStatus = SD.Status_Shipped;
                    break;
                case SD.Status_Shipped:
                    nextStatus = SD.Status_Delivered;
                    break;
                case SD.Status_Delivered:
                    nextStatus = SD.Status_Completed;
                    break;
                // 完成、取消、退款等不再自動流轉
                default:
                    TempData["Error"] = "此狀態無法再自動切換";
                    return RedirectToAction("Detail", new { orderId });
            }

            await _orderService.UpdateOrderStatus(orderId, nextStatus);

            return RedirectToAction("Detail", new { orderId });
        }

        [HttpPost]
        public async Task<IActionResult> Cancel(int orderId)
        {
            var order = await _orderService.GetOrderAsync(orderId);
            if (order == null || order.Status != SD.Status_Pending)
            {
                TempData["Error"] = "操作錯誤";
                return RedirectToAction("Detail", new { orderId });
            }
            else if (order.Status == SD.Status_Paid)
            {
                var options = new RefundCreateOptions
                {
                    Reason = RefundReasons.RequestedByCustomer,
                    PaymentIntent = order.PaymentIntentId
                };

                var service = new RefundService();
                Refund refund = service.Create(options);
            }

            await _orderService.UpdateOrderStatus(orderId, SD.Status_Cancelled);
            TempData["Success"] = "訂單已取消";
            return RedirectToAction("Detail", new { orderId });
        }
    }
}
