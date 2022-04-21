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

namespace Morgenmadsbuffeten.Pages.NewBreakfastBookingView
{
    public class KitchenGetModel : PageModel
    {
        private readonly Morgenmadsbuffeten.Data.ApplicationDbContext _context;

        public KitchenGetModel(Morgenmadsbuffeten.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<BreakfastBooking> BreakfastBooking { get; set; }
        [BindProperty]
        public DateTime Date { get; set; } = DateTime.Today;
        public int TotalOrders { get; set; }
        public int TotalNotCheckedIn { get; set; }
        public int TotalAdultsNotCheckedIn { get; set; }
        public int TotalChildrenNotCheckedIn { get; set; }
        public int TotalOrdersAdults { get; set; }
        public int TotalOrdersChildren { get; set; }
        public int TotalCheckedInAdults { get; set; }
        public int TotalCheckedInChildren { get; set; }

        public async Task OnGetAsync()
        {
            BreakfastBooking = await _context.BreakfastBookings.Where(bb=>bb.Date==Date)
                .Include(b => b.RoomBooking).ToListAsync();
            foreach (var bb in BreakfastBooking)
            {
                TotalOrdersAdults += bb.AdultsOrdered;
                TotalOrdersChildren += bb.ChildrenOrdered;
                TotalCheckedInAdults += bb.AdultsCheckedIn;
                TotalCheckedInChildren += bb.ChildrenCheckedIn;

            }
            TotalOrders = TotalOrdersAdults + TotalOrdersChildren;
            TotalNotCheckedIn = TotalOrders - (TotalCheckedInAdults + TotalCheckedInChildren);
            TotalAdultsNotCheckedIn = TotalOrdersAdults - TotalCheckedInAdults;
            TotalChildrenNotCheckedIn = TotalOrdersChildren - TotalCheckedInChildren;


        }
    }
}
