#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Morgenmadsbuffeten.Data;
using Morgenmadsbuffeten.Models;

namespace Morgenmadsbuffeten.Pages.RoomBookingView
{
    [Authorize("CanSeeReceptionStuff")]
    public class IndexModel : PageModel
    {
        private readonly Morgenmadsbuffeten.Data.ApplicationDbContext _context;

        public IndexModel(Morgenmadsbuffeten.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        //public IList<RoomBooking> RoomBooking { get;set; }
        public IList<BreakfastBooking> BreakfastBookings { get;set; }

        public async Task OnGetAsync()
        {
            //RoomBooking = await _context.RoomBookings.ToListAsync();
            BreakfastBookings = await _context.BreakfastBookings.Where(bb=> bb.CheckedIn == true).Select(bb=> new
            {
                Date = bb.Date,
                RoomNumber = bb.Room.RoomNumber,
                NumberOfChildren = bb.Room.Children,
                NumberOfAdults = bb.Room.Adults,
            }).ToListAsync();
        }
    }
}
