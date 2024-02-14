using Microservices.Services.ShoppingCartAPI.Models.Dto;

namespace Microservices.Services.ShoppingCartAPI.Service.IService
{
    public interface ICouponService
    {
        Task<CouponDto> GetCoupon(string couponCode);
    }
}
