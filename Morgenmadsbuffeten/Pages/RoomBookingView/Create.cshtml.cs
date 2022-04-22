#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Morgenmadsbuffeten.Models;

namespace Morgenmadsbuffeten.Pages.RoomBookingView
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
            return Page();
        }

        [BindProperty]
        public RoomBooking RoomBooking { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.RoomBookings.Add(RoomBooking);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
