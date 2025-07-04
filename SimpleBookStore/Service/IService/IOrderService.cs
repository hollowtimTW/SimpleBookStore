using SimpleBookStore.Models;
using SimpleBookStore.Models.ViewModels;

namespace SimpleBookStore.Service.IService
{
    public interface IOrderService
    {
        Task<OrderHeader> GetOrderAsync(int orderId);
        Task<OrderHeader> GetOrderHeaderAsync(int orderId);
        Task<IEnumerable<OrderHeader>> GetOrdersHeaderAsync(string userId);
        Task<int> CreateOrderAsync(string userId, RecipientInfo recipient, string? couponCode = null);
        Task<string> CreateStripeSession(int orderHeaderId, string? couponCode);
        Task<bool> ValidateStripeSession(int orderHeaderId);
    }
}
