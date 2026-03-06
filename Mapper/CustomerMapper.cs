using AutoMapper;
using CustomerManagementSystem.Application;

namespace CustomerManagementSystem.Mapper
{
    public class CustomerMapper : Profile
    {
        public CustomerMapper()
        {
            CreateMap<DTO.CustomerDto, CreateCustomerCommand>()
                .ForMember(dest => dest.Money, opt => opt.MapFrom(src => decimal.Parse(src.Amount)));
            CreateMap<DTO.AddressDto, CreateCustomerCommand>();
        }
    }
}
