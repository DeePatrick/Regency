using AutoMapper;
using Regency.Dtos;
using Regency.Models;

namespace Regency.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Domain to Dto
            Mapper.CreateMap<Customer, CustomerDto>();
            Mapper.CreateMap<Actor, ActorDto>();
            Mapper.CreateMap<MembershipType, MembershipTypeDto>();
            Mapper.CreateMap<Booking, BookingDto>();

            //Dto to Domain
            Mapper.CreateMap<CustomerDto, Customer>()
                .ForMember(c => c.Id, opt => opt.Ignore());

            Mapper.CreateMap<ActorDto, Actor>()
                .ForMember(c => c.Id, opt => opt.Ignore());

            Mapper.CreateMap<BookingDto, Booking>()
                .ForMember(c => c.Id, opt => opt.Ignore());



        }
    }
}