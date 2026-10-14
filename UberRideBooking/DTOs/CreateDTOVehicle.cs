using System.ComponentModel.DataAnnotations;
using UberRideBooking.Models;

namespace UberRideBooking.DTOs
{
    public class CreateDTOVehicle
    {
        public string PlateNumber { get; set; }
        public string Color { get; set; }
        public int DriverId { get; set; }
    }
}
