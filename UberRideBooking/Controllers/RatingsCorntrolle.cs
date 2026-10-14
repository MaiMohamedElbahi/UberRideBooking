using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UberRideBooking.DTOs;
using UberRideBooking.Models;
using UberRideBooking.REPOs.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UberRideBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RatingsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/<RatingsController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var entitymodel = await _unitOfWork.Ratings.GetAllAsync();
            var entitydto = _mapper.Map<List<CreateDTORating>>(entitymodel);
            return Ok(entitydto);
        }

        // GET api/<RatingsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entitymodel = await _unitOfWork.Ratings.GetAsync(id);
            var entitydto = _mapper.Map<CreateDTORating>(entitymodel);
            return Ok(entitydto);
        }

        // POST api/<RatingsController>
        [HttpPost]
        public async Task<IActionResult> Create(CreateDTORating entitydto)
        {
            var entitymodel = _mapper.Map<Rating>(entitydto);
           await _unitOfWork.Ratings.AddAsync(entitymodel);
            await _unitOfWork.SaveAsync();
            return CreatedAtAction(nameof(GetById), new { id = entitymodel.RatingId }, entitymodel);
        }

        // PUT api/<RatingsController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<RatingsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _unitOfWork.Ratings.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}
