using System.ComponentModel.DataAnnotations;

namespace UberRideBooking.DTOs.UserDTO
{
    public class RegisterDTO
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; }
    }
}
