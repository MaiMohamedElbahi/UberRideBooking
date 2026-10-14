using System;
using System.ComponentModel.DataAnnotations;

namespace UberRideBooking.Models
{
    public class Ride
    {
        [Key]
        public int RideId { get; set; }
        [Required]
        [StringLength(150)]
        public string PickupLocation { get; set; }
        [Required]
        [StringLength(150)]
        public string DropOffLocation { get; set; }
        [Required]
        [Range(20, 5000)]
        public decimal Fare { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public int RiderId { get; set; }
        public User Rider { get; set; }
        [Required]
        public int DriverId { get; set; }
        public User Driver { get; set; }

        public Payment Payment { get; set; }
        public Rating Rating { get; set; }
    }
}
