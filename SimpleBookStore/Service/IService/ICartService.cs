using Microsoft.CodeAnalysis;
using SimpleBookStore.Models;

namespace SimpleBookStore.Service.IService
{
    public interface ICartService
    {
        IEnumerable<ShoppingCart> GetCart(string userId);
        Task AddToCartAsync(string userId, int productId, int quantity);
        Task RemoveCartItemAsync(int cartId, string userId);
        IEnumerable<ShoppingCart> GetCartItems(string userId);
        Task ChangeQuantityAsync(int id, int delta);
    }
}
