#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Morgenmadsbuffeten.Models;

namespace Morgenmadsbuffeten.Pages.BreakfastBookingView
{
    public class DeleteModel : PageModel
    {
        private readonly Morgenmadsbuffeten.Data.ApplicationDbContext _context;

        public DeleteModel(Morgenmadsbuffeten.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            BreakfastBooking = await _context.BreakfastBookings.FindAsync(id);

            if (BreakfastBooking != null)
            {
                _context.BreakfastBookings.Remove(BreakfastBooking);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
