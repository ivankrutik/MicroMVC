using AutoMapper;
using Mango.Servises.CouponAPI.Model;
using Mango.Servises.CouponAPI.Model.Dto;

namespace Mango.Servises.CouponAPI.Mapping
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CouponDto, Coupon>().ReverseMap();
            });

            return mappingConfig;
        }
    }
}
