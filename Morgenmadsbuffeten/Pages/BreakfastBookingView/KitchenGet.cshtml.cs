#nullable disable
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IActionResult> OnGetAsync(string date)
        {
            string[] dateFormats = {
            "dd-MM-yyyy", "yyyy-MM-dd"
            };

            if(string.IsNullOrEmpty(date))
            {
                //Date = DateTime.Today;
                string url = "/NewBreakfastBookingView/KitchenGet?date=" +
                             DateTime.Today.ToString(dateFormats[0]);
                return Redirect(url);
            }

            if (DateTime.TryParseExact(date, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var formattedDate))
            {
                Date = formattedDate;
            }

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
            return Page();
        }

    }
}
