namespace Morgenmadsbuffeten.DTOs
{
    public class RoomsBreakfastDTO
    {
        public long BreakfastBookingId { get; set; }
        public long RoomNumber { get; set; }
        public int NumberOfChildren { get; set; }
        public int NumberOfAdults { get; set; }
    }
}
