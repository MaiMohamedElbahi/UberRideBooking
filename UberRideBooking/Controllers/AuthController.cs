using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UberRideBooking.DTOs.UserDTO;
using UberRideBooking.REPOs.Interfaces;

namespace UberRideBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepo _userRepo;
        private readonly IUnitOfWork _unitOfWork;

        public AuthController(IUserRepo userRepo, IUnitOfWork unitOfWork)
        {
            _userRepo = userRepo;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterDTO user)
        {
            if (user == null)
            {
                return BadRequest("User data is null.");
            }

            var u = await _userRepo.Register(user);
            await _unitOfWork.SaveAsync();
            return Ok(u);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginDTO user)
        {
            if (user == null)
            {
                return BadRequest("User data is null.");
            }

            var u = await _userRepo.Login(user);
            return Ok(u);
        }
    }
}
