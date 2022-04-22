#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Morgenmadsbuffeten.Data;
using Morgenmadsbuffeten.Hubs;
using Morgenmadsbuffeten.Models;

namespace Morgenmadsbuffeten.Pages.NewBreakfastBookingView
{
    //[Authorize("CanSeeWaiterStuff")]
    public class CheckInModel : PageModel
    {
        private readonly Morgenmadsbuffeten.Data.ApplicationDbContext _context;
        private readonly IHubContext<LiveReloadHub> _reloadHubContext;

        public CheckInModel(Morgenmadsbuffeten.Data.ApplicationDbContext context, IHubContext<LiveReloadHub> reloadHubContext)
        {
            _context = context;
            _reloadHubContext = reloadHubContext;
        }

        [BindProperty]
        public BreakfastBooking BreakfastBooking { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            BreakfastBooking = await _context.BreakfastBookings
                .Include(b => b.RoomBooking).FirstOrDefaultAsync();

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

            var bb = await _context.BreakfastBookings
                .Where(bb =>
                    bb.Room == BreakfastBooking.Room && bb.Date == BreakfastBooking.Date)
                .SingleOrDefaultAsync();


            if (bb == null)
                return Page();

            if ((bb.ChildrenCheckedIn + BreakfastBooking.ChildrenCheckedIn > bb.ChildrenOrdered)
                || (bb.AdultsCheckedIn + BreakfastBooking.AdultsCheckedIn > bb.AdultsOrdered))
            {
                return BadRequest("Tried to check-in more adults or children than there are orders.");
            }

            bb.ChildrenCheckedIn += BreakfastBooking.ChildrenCheckedIn;
            bb.AdultsCheckedIn += BreakfastBooking.AdultsCheckedIn;

            _context.Attach(bb).State = EntityState.Modified;

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
            return RedirectToPage("./CheckIn");
        }

        private bool BreakfastBookingExists(long id)
        {
            return _context.BreakfastBookings.Any(e => e.BreakfastBookingId == id);
        }
    }
}
