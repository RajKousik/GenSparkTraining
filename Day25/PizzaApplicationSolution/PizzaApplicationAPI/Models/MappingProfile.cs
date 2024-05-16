using AutoMapper;
using PizzaApplicationAPI.Models.DTOs;

namespace PizzaApplicationAPI.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Pizza, PizzaDTO>().ReverseMap();
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<User, LoginReturnDTO>().ReverseMap();
        }
    }
}
