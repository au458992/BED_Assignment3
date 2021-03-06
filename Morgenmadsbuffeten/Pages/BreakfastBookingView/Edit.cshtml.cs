#nullable disable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Morgenmadsbuffeten.Hubs;
using Morgenmadsbuffeten.Models;

namespace Morgenmadsbuffeten.Pages.BreakfastBookingView
{
    [Authorize("CanSeeWaiterStuff")]
    public class EditModel : PageModel
    {
        private readonly Morgenmadsbuffeten.Data.ApplicationDbContext _context;
        private readonly IHubContext<LiveReloadHub> _reloadHubContext;
        public EditModel(Morgenmadsbuffeten.Data.ApplicationDbContext context, IHubContext<LiveReloadHub> reloadHubContext)
        {
            _context = context;
            _reloadHubContext = reloadHubContext;
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
           ViewData["Room"] = new SelectList(_context.RoomBookings, "RoomNumber", "RoomNumber");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(BreakfastBooking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BreakfastBookingExists(BreakfastBooking.BreakfastBookingId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            await _reloadHubContext.Clients.All.SendAsync("Reload");
            return RedirectToPage("./Index");
        }

        private bool BreakfastBookingExists(long id)
        {
            return _context.BreakfastBookings.Any(e => e.BreakfastBookingId == id);
        }
    }
}
