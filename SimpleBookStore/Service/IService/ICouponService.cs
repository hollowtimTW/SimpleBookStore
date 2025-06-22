using SimpleBookStore.Models;

namespace SimpleBookStore.Service.IService
{
    public interface ICouponService
    {
        Task<IEnumerable<Coupon>> GetAllAsync();
        Task<Coupon?> GetAsync(string code);
        Task ToggleActiveAsync(int id, bool isActive);
        Task CreateAsync(Coupon Coupon);
    }
}
