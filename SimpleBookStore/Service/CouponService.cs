using Microsoft.EntityFrameworkCore;
using SimpleBookStore.Data;
using SimpleBookStore.Models;
using SimpleBookStore.Service.IService;
using SimpleBookStore.Utility.Helper;

namespace SimpleBookStore.Service
{
    public class CouponService : ICouponService
    {
        private readonly AppDbContext _db;
        public CouponService(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<Coupon>> GetAllAsync()
        {
            return await _db.Coupons.ToListAsync();
        }

        public async Task<Coupon?> GetAsync(string code)
        {
            return await _db.Coupons
                .Where(p => p.Code == code)
                .FirstOrDefaultAsync();
        }

        public async Task ToggleActiveAsync(int id, bool isActive)
        {
            var coupon = await _db.Coupons.FirstOrDefaultAsync(p => p.Id == id);
            if (coupon == null)
            {
                throw new Exception("找不到產品");
            }
            coupon.IsActive = isActive;
            await _db.SaveChangesAsync();
        }

        public async Task CreateAsync(Coupon coupon)
        {
            //避免重複
            var existingCoupon = await _db.Coupons
                .FirstOrDefaultAsync(c => c.Code == coupon.Code);
            if (existingCoupon != null)
            {
                throw new Exception("名稱重複");
            }

            _db.Coupons.Add(coupon);
            await _db.SaveChangesAsync();
        }
    }
}
