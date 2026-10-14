using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UberRideBooking.DTOs;
using UberRideBooking.DTOs.UserDTO;
using UberRideBooking.Models;
using UberRideBooking.REPOs.Implementaions;
using UberRideBooking.REPOs.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UberRideBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserRepo _userRepo;


        public UsersController(IUnitOfWork unitOfWork, IMapper mapper, IUserRepo userRepo)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userRepo = userRepo;
        }

        // GET: api/<UsersController>
        [HttpGet("Drivers")]
        public async Task<IActionResult> GetAll()
        {
            var entitymodel = await _userRepo.GetAllAsync();
            entitymodel.Where(e => e.Role == "Driver").ToList();
            var entitydto = _mapper.Map<List<RegisterDTO>>(entitymodel);
            return Ok(entitydto);
        }

        // GET api/<UsersController>/5
        [HttpGet("Search {name}")]
        public async Task<IActionResult> GetById(string name)
        {
            var entitymodel = await _userRepo.GetAllAsync();
            entitymodel.Where(e => e.FullName == name && e.Role == "Rider").FirstOrDefault();
            var entitydto = _mapper.Map<RegisterDTO>(entitymodel);
            return Ok(entitydto);
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<IActionResult> Create(RegisterDTO entitydto)
        {
            var entitymodel = _mapper.Map<User>(entitydto);
           await _userRepo.AddAsync(entitymodel);
            await _unitOfWork.SaveAsync();
            return CreatedAtAction(nameof(GetById), new { id = entitymodel.UserId }, entitymodel);
        }

        // PUT api/<UsersController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _userRepo.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}
