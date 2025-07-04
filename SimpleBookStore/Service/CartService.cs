using Microsoft.EntityFrameworkCore;
using SimpleBookStore.Data;
using SimpleBookStore.Models;
using SimpleBookStore.Service.IService;

namespace SimpleBookStore.Service
{
    public class CartService : ICartService
    {
        private readonly AppDbContext _db;
        public CartService(AppDbContext db)
        {
            _db = db;
        }

        public IEnumerable<ShoppingCart> GetCart(string userId)
        {
            return _db.ShoppingCarts
                .Include(x => x.Product)
                .Include(x => x.Product).ThenInclude(p => p.Author)
                .Where(x => x.UserId == userId);
        }

        public async Task AddToCartAsync(string userId, int productId, int quantity)
        {
            var cartItem = await _db.ShoppingCarts
                .FirstOrDefaultAsync(x => x.UserId == userId && x.ProductId == productId);

            if (cartItem != null)
            {
                cartItem.Quantity += quantity;
            }
            else
            {
                var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == productId);
                if (product == null) throw new Exception("商品不存在");
                _db.ShoppingCarts.Add(new ShoppingCart
                {
                    UserId = userId,
                    ProductId = productId,
                    Quantity = quantity,
                    UnitPrice = product.Price
                });
            }
            await _db.SaveChangesAsync();
        }


        public async Task RemoveCartItemAsync(int cartId, string userId)
        {
            var item = await _db.ShoppingCarts.FirstOrDefaultAsync(x => x.Id == cartId && x.UserId == userId);
            if (item != null)
            {
                _db.ShoppingCarts.Remove(item);
                await _db.SaveChangesAsync();
            }
        }

        public IEnumerable<ShoppingCart> GetCartItems(string userId)
        {
            return _db.ShoppingCarts
                .Include(x => x.Product)
                .Include(x => x.Product).ThenInclude(p => p.Author)
                .Where(x => x.UserId == userId);
        }

        public async Task ChangeQuantityAsync(int id, int delta)
        {
            var cartItem = await _db.ShoppingCarts.FirstOrDefaultAsync(x => x.Id == id);
            if (cartItem != null)
            {
                cartItem.Quantity += delta;
                if (cartItem.Quantity <= 0)
                {
                    _db.ShoppingCarts.Remove(cartItem);
                }
                await _db.SaveChangesAsync();
            }
            else
            {
                throw new Exception("購物車項目不存在");
            }
        }
    }
}
