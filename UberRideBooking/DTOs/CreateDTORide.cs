using System.ComponentModel.DataAnnotations;
using UberRideBooking.Models;

namespace UberRideBooking.DTOs
{
    public class CreateDTORide
    {
        public string PickupLocation { get; set; }
        public string DropOffLocation { get; set; }
        public decimal Fare { get; set; }
        public string Status { get; set; }
        public int RiderId { get; set; }
        public int DriverId { get; set; }
    }
}
