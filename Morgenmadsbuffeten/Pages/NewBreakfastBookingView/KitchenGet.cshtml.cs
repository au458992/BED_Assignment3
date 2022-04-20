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

namespace Morgenmadsbuffeten.Pages.BreakfastBookingView
{
    public class KitchenGetModel : PageModel
    {
        private readonly Morgenmadsbuffeten.Data.ApplicationDbContext _context;

        public KitchenGetModel(Morgenmadsbuffeten.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<BreakfastBooking> BreakfastBooking { get;set; }
        public IList<RoomBooking> RoomBooking { get;set; }

        public async Task OnGetAsync()
        {
            RoomBooking = await _context.RoomBookings.ToListAsync();
            
            BreakfastBooking = await _context.BreakfastBookings
                .Include(b => b.RoomBooking).ToListAsync();
        }
    }
}
