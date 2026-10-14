using System.ComponentModel.DataAnnotations;

namespace UberRideBooking.Models
{
    public class Vehicle
    {
        [Key]
        public int VehicleId { get; set; }

        [Required]
        [StringLength(20)]
        public string PlateNumber { get; set; }
        [Required]
        public string Color { get; set; }
        [Required]
        public int DriverId { get; set; }
        public User Driver { get; set; }
    
    }
}
