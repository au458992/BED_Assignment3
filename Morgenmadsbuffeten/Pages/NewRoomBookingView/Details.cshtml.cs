#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Morgenmadsbuffeten.Data;
using Morgenmadsbuffeten.Models;

namespace Morgenmadsbuffeten.Pages.NewRoomBookingView
{
    public class DetailsModel : PageModel
    {
        private readonly Morgenmadsbuffeten.Data.ApplicationDbContext _context;

        public DetailsModel(Morgenmadsbuffeten.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public RoomBooking RoomBooking { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RoomBooking = await _context.RoomBookings.FirstOrDefaultAsync(m => m.RoomNumber == id);

            if (RoomBooking == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
