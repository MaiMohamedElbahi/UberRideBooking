using AutoMapper;
using UberRideBooking.DTOs;
using UberRideBooking.DTOs.UserDTO;
using UberRideBooking.Models;

namespace UberRideBooking.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User,LoginDTO>().ReverseMap();
            CreateMap<User,ResponseDTO>().ReverseMap();
            CreateMap<User,RegisterDTO>().ReverseMap();

            CreateMap<Ride, CreateDTORide>().ReverseMap();
            CreateMap<Ride, UpdateDTORide>().ReverseMap();
            CreateMap<Vehicle, CreateDTOVehicle>().ReverseMap();
            CreateMap<Payment, CreateDTOPayment>().ReverseMap();
            CreateMap<Rating, CreateDTORating>().ReverseMap();
        }
    }
}
