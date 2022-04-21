#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Morgenmadsbuffeten.Data;
using Morgenmadsbuffeten.Models;

namespace Morgenmadsbuffeten.Pages.NewBreakfastBookingView
{
    [Authorize("CanSeeWaiterStuff")]
    public class CheckInModel : PageModel
    {
        private readonly Morgenmadsbuffeten.Data.ApplicationDbContext _context;

        public CheckInModel(Morgenmadsbuffeten.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BreakfastBooking BreakfastBooking { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            BreakfastBooking = await _context.BreakfastBookings
                .Include(b => b.RoomBooking).FirstOrDefaultAsync(m => m.BreakfastBookingId == 1);

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

            return RedirectToPage("./Index");
        }

        private bool BreakfastBookingExists(long id)
        {
            return _context.BreakfastBookings.Any(e => e.BreakfastBookingId == id);
        }
    }
}
