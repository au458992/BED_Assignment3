namespace Morgenmadsbuffeten.Models
{
    public class BreakfastBooking
    {
        public long BreakfastBookingId { get; set; }
        public DateTime Date { get; set; }
        public int AdultsCheckedIn { get; set; }
        public int ChildrenCheckedIn { get; set; }
        public int AdultsOrdered { get; set; }
        public int ChildrenOrdered { get; set; }
        public RoomBooking? RoomBooking { get; set; }
        public long Room { get; set; }
    }
}
