using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Morgenmadsbuffeten.Models;

namespace Morgenmadsbuffeten.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<RoomBooking> RoomBookings { get; set; }
        public DbSet<BreakfastBooking> BreakfastBookings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<RoomBooking>()
                .HasKey(rb => rb.RoomNumber);

            //Many breakfastbookings to one room
            builder.Entity<BreakfastBooking>()
                .HasOne(bb => bb.RoomBooking)
                .WithMany(rb => rb.BreakfastBookings)
                .HasForeignKey(bb => bb.Room);

            //SeedData(builder);
        }

        //public void SeedData(ModelBuilder modelBuilder)
        //{
        //    RoomBooking roomBooking = new RoomBooking()
        //    {
        //        DateFrom = new DateTime(2022, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
        //        DateTo = new DateTime(2022, 4, 22, 0, 0, 0, 0, DateTimeKind.Unspecified),
        //        RoomNumber = 1,
        //        Adults = 2,
        //        Children = 3,
        //    };

        //    modelBuilder.Entity<RoomBooking>().HasData(roomBooking);
        //}
    }
}