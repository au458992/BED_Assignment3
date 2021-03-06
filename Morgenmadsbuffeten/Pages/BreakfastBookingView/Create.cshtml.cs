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
    [Authorize("CanSeeReceptionStuff")]
    public class CreateModel : PageModel
    {
        private readonly Morgenmadsbuffeten.Data.ApplicationDbContext _context;
        private readonly IHubContext<LiveReloadHub> _reloadHubContext;
        public CreateModel(Morgenmadsbuffeten.Data.ApplicationDbContext context, IHubContext<LiveReloadHub> reloadHubContext )
        {
            _context = context;
            _reloadHubContext = reloadHubContext;
        }

        public IActionResult OnGet()
        {
        ViewData["Room"] = new SelectList(_context.RoomBookings, "RoomNumber", "RoomNumber");
            return Page();
        }

        [BindProperty]
        public BreakfastBooking BreakfastBooking { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            // Checked in is 0 by default
            BreakfastBooking.ChildrenCheckedIn = 0;
            BreakfastBooking.AdultsCheckedIn = 0;

            var bbDate = await _context.BreakfastBookings
                .Where(bb => bb.Date == BreakfastBooking.Date && bb.Room == BreakfastBooking.Room)
                .FirstOrDefaultAsync();
            if (bbDate != null)
            {
                return BadRequest("There is already a breakfast booking for the chosen date");
            }

            var adultsInRoom = await _context.RoomBookings
                .Where(rb => rb.RoomNumber == BreakfastBooking.Room)
                .Select(rb => rb.Adults).FirstOrDefaultAsync();
            var childrenInRoom = await _context.RoomBookings
                .Where(rb => rb.RoomNumber == BreakfastBooking.Room)
                .Select(rb => rb.Children).FirstOrDefaultAsync();

            var RoomBookingTimeSpan = await _context.RoomBookings
                .Where(rb => rb.DateFrom <= BreakfastBooking.Date && rb.DateTo >= BreakfastBooking.Date)
                .FirstOrDefaultAsync();
            if (RoomBookingTimeSpan == null)
            {
                return BadRequest("Tried to book breakfast outside of room booking timespan");
            }

            if (BreakfastBooking.AdultsOrdered > adultsInRoom ||
                BreakfastBooking.ChildrenOrdered > childrenInRoom)
            {
                return BadRequest("Tried to order breakfast for more adults or/and children than the room contains");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.BreakfastBookings.Add(BreakfastBooking);
            await _context.SaveChangesAsync();

            await _reloadHubContext.Clients.All.SendAsync("Reload");

            return RedirectToPage("./Index");
        }
    }
}
