#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Morgenmadsbuffeten.Data;
using Morgenmadsbuffeten.Models;

namespace Morgenmadsbuffeten.Pages.NewBreakfastBookingView
{
    public class CreateModel : PageModel
    {
        private readonly Morgenmadsbuffeten.Data.ApplicationDbContext _context;

        public CreateModel(Morgenmadsbuffeten.Data.ApplicationDbContext context)
        {
            _context = context;
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
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Checked in is 0 by default
            BreakfastBooking.ChildrenCheckedIn = 0;
            BreakfastBooking.AdultsCheckedIn = 0;

            _context.BreakfastBookings.Add(BreakfastBooking);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
