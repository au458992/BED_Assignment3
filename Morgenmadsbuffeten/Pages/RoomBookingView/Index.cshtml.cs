#nullable disable
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Morgenmadsbuffeten.Models;

namespace Morgenmadsbuffeten.Pages.RoomBookingView
{
    public class IndexModel : PageModel
    {
        private readonly Morgenmadsbuffeten.Data.ApplicationDbContext _context;

        public IndexModel(Morgenmadsbuffeten.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<RoomBooking> RoomBooking { get;set; }

        public async Task OnGetAsync()
        {
            RoomBooking = await _context.RoomBookings.ToListAsync();
        }
    }
}
