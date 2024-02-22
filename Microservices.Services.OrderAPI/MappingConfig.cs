using AutoMapper;
using Microservices.Services.OrderAPI.Models;
using Microservices.Services.OrderAPI.Models.Dto;

namespace Microservices.Services.OrderAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<OrderHeader, OrderHeaderDto>().ReverseMap();
                config.CreateMap<OrderDetails, OrderDetailsDto>().ReverseMap();

                config.CreateMap<OrderHeaderDto, CartHeaderDto>()
                .ForMember(dest => dest.CartTotal , u=>u.MapFrom(src=> src.OrderTotal)).ReverseMap();
                config.CreateMap<CartDetailsDto, OrderDetailsDto>()
                .ForMember(dest => dest.ProductName, u => u.MapFrom(src => src.MyProperty.Name))
                .ForMember(dest => dest.Price, u => u.MapFrom(src => src.MyProperty.Price));

                config.CreateMap<OrderDetailsDto, CartDetailsDto>();
            });
            return mappingConfig;
        }
    }
}