using System;
using System.ComponentModel.DataAnnotations;

namespace UberRideBooking.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }
        [Range(20, 5000)]
        public decimal Amount { get; set; }
        [Required]
        public string PaymentMethod { get; set; }
        [Required]
        public DateTime PaymentDate { get; set; }
        [Required]
        public string Status { get; set; }

        public int RideId { get; set; }
        public Ride Ride { get; set; }
    }
}
