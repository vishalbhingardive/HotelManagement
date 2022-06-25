using HotelManagement.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext()
        {

        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<CheckInOutSystem> CheckInOut  { get; set; }
    }
}
