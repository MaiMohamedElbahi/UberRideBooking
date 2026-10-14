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
    public class RidesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RidesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/<RidesController>
        [HttpGet()]
        public async Task<IActionResult> GetAll( int pageunm, int pagesize)
        {
            var entitymodel = await _unitOfWork.Rides.GetAllAsync();
            entitymodel = entitymodel.Skip((pageunm - 1) * pagesize).Take(pagesize).ToList();
            var entitydto = _mapper.Map<List<CreateDTORide>>(entitymodel);
            return Ok(entitydto);
        }

        // GET: api/<RidesController>
        [HttpGet("Search {pickup}")]
        public async Task<IActionResult> GetAllsearch( string pick)
        {
            var entitymodel = await _unitOfWork.Rides.GetAllAsync();
            entitymodel= entitymodel.Where(x => x.PickupLocation.Contains(pick)).ToList();
            var entitydto = _mapper.Map<List<CreateDTORide>>(entitymodel);
            return Ok(entitydto);
        }

        // GET: api/<RidesController>
        [HttpGet("Filter")]
        public async Task<IActionResult> GetAllfilter([FromQuery] string? status, [FromQuery] decimal? minFare, [FromQuery] decimal? maxFare)
        {
            var entitymodel = await _unitOfWork.Rides.GetAllAsync();
            if(status != null)
            {
                entitymodel = entitymodel.Where(x => x.Status == status).ToList();
            }
            if(minFare != null)
            {
                entitymodel = entitymodel.Where(x => x.Fare >= minFare).ToList();
            }
            if (maxFare != null)
            {
                entitymodel = entitymodel.Where(x => x.Fare <= maxFare).ToList();
            }
            var entitydto = _mapper.Map<List<CreateDTORide>>(entitymodel);
            return Ok(entitydto);
        }

        // GET api/<RidesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entitymodel = await _unitOfWork.Rides.GetAsync(id);
            var entitydto = _mapper.Map<CreateDTORide>(entitymodel);
            return Ok(entitydto);
        }

        // POST api/<RidesController>
        [HttpPost]
        public async Task<IActionResult> Create(CreateDTORide entitydto)
        {
            var entitymodel = _mapper.Map<Ride>(entitydto);
           await _unitOfWork.Rides.AddAsync(entitymodel);
            await _unitOfWork.SaveAsync();
            return CreatedAtAction(nameof(GetById), new { id = entitymodel.RideId }, entitymodel);
        }

        // PUT api/<RidesController>/5
        [HttpPatch("{id}/Accept")]
        public async Task<IActionResult> Patch(int id, UpdateDTORide entitydto)
        {
            var entitymodel = await _unitOfWork.Rides.GetAsync(id);
            entitymodel.Status = "Accept";
           await _unitOfWork.Rides.UpdateAsync(entitymodel);
            await _unitOfWork.SaveAsync();
            return Ok();
        }
        // PUT api/<RidesController>/5
        [HttpPatch("{id}/Complete")]
        public async Task<IActionResult> Patchcomp(int id, UpdateDTORide entitydto)
        {
            var entitymodel = await _unitOfWork.Rides.GetAsync(id);
            entitymodel.Status = "Complete";
           await _unitOfWork.Rides.UpdateAsync(entitymodel);
            await _unitOfWork.SaveAsync();
            return Ok();
        }

        // DELETE api/<RidesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _unitOfWork.Rides.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}
