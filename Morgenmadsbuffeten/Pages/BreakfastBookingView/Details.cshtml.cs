#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Morgenmadsbuffeten.Models;

namespace Morgenmadsbuffeten.Pages.BreakfastBookingView
{
    public class DetailsModel : PageModel
    {
        private readonly Morgenmadsbuffeten.Data.ApplicationDbContext _context;

        public DetailsModel(Morgenmadsbuffeten.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public BreakfastBooking BreakfastBooking { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            BreakfastBooking = await _context.BreakfastBookings
                .Include(b => b.RoomBooking).FirstOrDefaultAsync(m => m.BreakfastBookingId == id);

            if (BreakfastBooking == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
