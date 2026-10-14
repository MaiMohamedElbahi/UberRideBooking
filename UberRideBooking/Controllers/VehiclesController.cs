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
    public class VehiclesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VehiclesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/<VehiclesController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var entitymodel = await _unitOfWork.Vehicles.GetAllAsync();
            var entitydto = _mapper.Map<List<CreateDTOVehicle>>(entitymodel);
            return Ok(entitydto);
        }

        // GET api/<VehiclesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entitymodel = await _unitOfWork.Vehicles.GetAsync(id);
            var entitydto = _mapper.Map<CreateDTOVehicle>(entitymodel);
            return Ok(entitydto);
        }

        // POST api/<VehiclesController>
        [HttpPost]
        public async Task<IActionResult> Create(CreateDTOVehicle entitydto)
        {
            var entitymodel = _mapper.Map<Vehicle>(entitydto);
           await _unitOfWork.Vehicles.AddAsync(entitymodel);
            await _unitOfWork.SaveAsync();
            return CreatedAtAction(nameof(GetById), new { id = entitymodel.VehicleId }, entitymodel);
        }

       // PUT api/<VehiclesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, CreateDTOVehicle entitydto)
        {
            var entitymodel = await _unitOfWork.Vehicles.GetAsync(id);
            _mapper.Map(entitydto, entitymodel);
          await _unitOfWork.Vehicles.UpdateAsync(entitymodel);
            await _unitOfWork.SaveAsync();
            return Ok();
        }

        // DELETE api/<VehiclesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _unitOfWork.Vehicles.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}
