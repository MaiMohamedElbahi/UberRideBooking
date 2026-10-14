using UberRideBooking.Models;

namespace UberRideBooking.REPOs.Interfaces
{
    public interface IUnitOfWork 
    {
        IGenericRepo<Vehicle> Vehicles { get; }
        IGenericRepo<Ride> Rides { get; }
        IGenericRepo<Rating> Ratings { get; }
        IGenericRepo<Payment> Payments { get; }

        IUserRepo UsersRepo { get; }

        Task SaveAsync();
    }
}
