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
using Morgenmadsbuffeten.DTOs;
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
        public IList<RoomsBreakfastDTO> BreakfastBookings { get;set; }

        public async Task OnGetAsync()
        {
            //RoomBooking = await _context.RoomBookings.ToListAsync();
            BreakfastBookings = await _context.BreakfastBookings.Where(bb=> bb.CheckedIn == true && bb.Date == DateTime.Today).Select(bb=> new RoomsBreakfastDTO
            {
                RoomNumber = bb.RoomBooking.RoomNumber,
                NumberOfChildren = bb.RoomBooking.Children,
                NumberOfAdults = bb.RoomBooking.Adults,
            }).ToListAsync();
        }
    }
}
