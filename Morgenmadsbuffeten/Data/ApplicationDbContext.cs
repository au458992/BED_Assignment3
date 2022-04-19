using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Morgenmadsbuffeten.Models;

namespace Morgenmadsbuffeten.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        DbSet<RoomBooking> RoomBookings { get; set; }
        DbSet<BreakfastBooking> BreakfastBookings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<RoomBooking>()
                .HasKey(rb => rb.RoomNumber);

            builder.Entity<BreakfastBooking>()
                .HasOne(bb => bb.RoomBooking)
                .WithMany(rb => rb.BreakfastBookings);
        }
    }
}