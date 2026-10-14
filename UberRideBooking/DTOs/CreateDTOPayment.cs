using System.ComponentModel.DataAnnotations;

namespace UberRideBooking.DTOs
{
    public class CreateDTOPayment
    {
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Status { get; set; }
        public int RideId { get; set; }
    }
}
