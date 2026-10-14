using System.ComponentModel.DataAnnotations;

namespace UberRideBooking.DTOs
{
    public class CreateDTORating
    {
        public int Score { get; set; }
        public string Comment { get; set; }
        public int RideId { get; set; }
    }
}
