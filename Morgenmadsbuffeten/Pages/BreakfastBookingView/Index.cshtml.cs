#nullable disable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Morgenmadsbuffeten.DTOs;

namespace Morgenmadsbuffeten.Pages.BreakfastBookingView
{
    [Authorize("CanSeeReceptionStuff")]
    public class IndexModel : PageModel
    {
        private readonly Morgenmadsbuffeten.Data.ApplicationDbContext _context;

        public IndexModel(Morgenmadsbuffeten.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public List<RoomsBreakfastDTO> BreakfastBookings { get;set; }

        public async Task OnGetAsync()
        {
            BreakfastBookings = await _context.BreakfastBookings
                 .Where(bb => (bb.AdultsCheckedIn > 0 || bb.ChildrenCheckedIn > 0) && bb.Date == DateTime.Today)
                .Select(bb => new RoomsBreakfastDTO
                {
                    BreakfastBookingId = bb.BreakfastBookingId,
                    RoomNumber = bb.RoomBooking.RoomNumber,
                    NumberOfChildren = bb.ChildrenCheckedIn,
                    NumberOfAdults = bb.AdultsCheckedIn,
                }).ToListAsync();

            Console.WriteLine(BreakfastBookings);
        }
    }
}
