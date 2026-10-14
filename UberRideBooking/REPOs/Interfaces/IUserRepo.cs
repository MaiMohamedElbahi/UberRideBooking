using UberRideBooking.DTOs.UserDTO;
using UberRideBooking.Models;

namespace UberRideBooking.REPOs.Interfaces
{
    public interface IUserRepo : IGenericRepo<User>
    {
        Task<ResponseDTO> Login(LoginDTO loginDTO);
        Task<ResponseDTO> Register(RegisterDTO loginDTO);
    }
}
