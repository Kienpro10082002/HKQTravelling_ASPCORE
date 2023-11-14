using Microsoft.EntityFrameworkCore;
using System.Data;

namespace HKQTravelling.Models
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Add the unique constraint into RoleName property of Role Entity
            modelBuilder.Entity<Roles>()
                .HasIndex(u => u.RoleName)
                .IsUnique();
            //1-1 Relationship
            modelBuilder.Entity<Rules>()
                .HasKey(r => r.TourId);
            modelBuilder.Entity<Rules>()
                .HasOne(r => r.tours)
                .WithOne()
                .HasForeignKey<Rules>(r => r.TourId);
            //Same the unique code above
            modelBuilder.Entity<Users>()
                .HasIndex(u => u.Username)
                .IsUnique();
            modelBuilder.Entity<UserDetails>()
                .HasIndex(u => u.Email)
                .IsUnique();
            modelBuilder.Entity<UserDetails>()
                .HasIndex(u => u.PhoneNumber)
                .IsUnique();
            modelBuilder.Entity<UserDetails>()
                .HasIndex(u => u.NiNumber)
                .IsUnique();
            modelBuilder.Entity<StartLocations>()
                .HasIndex(u => u.StartLocationName)
                .IsUnique();
            modelBuilder.Entity<EndLocations>()
                .HasIndex(u => u.EndLocationName)
                .IsUnique();
        }

        public DbSet<Roles> roles { get; set; }
        public DbSet<Users> users { get; set; }
        public DbSet<UserDetails> userDetails { get; set; }
        public DbSet<StartLocations> startLocations { get; set; }
        public DbSet<EndLocations> endLocations { get; set; }
        public DbSet<Discounts> discounts { get; set; }
        public DbSet<Rules> rules{ get; set; }
        public DbSet<Tours> tours { get; set; }
        public DbSet<TourImages> tourImages { get; set; }
        public DbSet<TourDays> tourDays { get; set; }
        public DbSet<Bookings> bookings { get; set; }
        public DbSet<Payments> payments { get; set; }
    }
}