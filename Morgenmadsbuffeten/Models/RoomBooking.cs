namespace Morgenmadsbuffeten.Models
{
    public class RoomBooking
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo{ get; set; }
        public long RoomNumber { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public List<BreakfastBooking>? BreakfastBookings { get; set; }
    }
}
