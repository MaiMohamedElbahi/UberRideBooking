using UberRideBooking.Data;
using UberRideBooking.Models;
using UberRideBooking.REPOs.Interfaces;

namespace UberRideBooking.REPOs.Implementaions
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IConfiguration _configuration;
        public IGenericRepo<Vehicle> Vehicles { get;  set; }
        public IGenericRepo<Ride> Rides { get; set; }
        public IGenericRepo<Rating> Ratings { get; set; }
        public IGenericRepo<Payment> Payments { get; set; }
        public IUserRepo UsersRepo { get; set; }


        public UnitOfWork(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            Vehicles = new GenericRepo<Vehicle>(_context);
            Rides = new GenericRepo<Ride>(_context);
            Ratings = new GenericRepo<Rating>(_context);
            Payments = new GenericRepo<Payment>(_context);
            UsersRepo = new UserRepo(_context, _configuration);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
