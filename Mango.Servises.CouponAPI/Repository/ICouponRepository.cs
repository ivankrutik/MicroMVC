using Mango.Servises.CouponAPI.Model.Dto;

namespace Mango.Servises.CouponAPI.Repository
{
    public interface ICouponRepository
    {
        Task<CouponDto> GetCouponByCode(string couponCode);
    }
}
