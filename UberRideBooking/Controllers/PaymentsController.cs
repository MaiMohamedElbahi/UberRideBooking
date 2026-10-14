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
    public class PaymentsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PaymentsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/<PaymentsController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var entitymodel = await _unitOfWork.Payments.GetAllAsync();
            var entitydto = _mapper.Map<List<CreateDTOPayment>>(entitymodel);
            return Ok(entitydto);
        }

        // GET api/<PaymentsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entitymodel = await _unitOfWork.Payments.GetAsync(id);
            var entitydto = _mapper.Map<CreateDTOPayment>(entitymodel);
            return Ok(entitydto);
        }

        // POST api/<PaymentsController>
        [HttpPost]
        public async Task<IActionResult> Create(CreateDTOPayment entitydto)
        {
            var entitymodel = _mapper.Map<Payment>(entitydto);
           await _unitOfWork.Payments.AddAsync(entitymodel);
            await _unitOfWork.SaveAsync();
            return CreatedAtAction(nameof(GetById), new { id = entitymodel.PaymentId }, entitymodel);
        }

        // PUT api/<PaymentsController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<PaymentsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _unitOfWork.Payments.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}
