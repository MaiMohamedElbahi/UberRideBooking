using Microsoft.EntityFrameworkCore;
using UberRideBooking.Models;

namespace UberRideBooking.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Ride> Rides { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

          

            modelBuilder.Entity<User>().HasOne(u => u.Vehicle)
                .WithOne(v => v.Driver)
                .HasForeignKey<Vehicle>(v => v.DriverId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<User>().HasMany(u => u.RidesAsDriver)
                .WithOne(r => r.Driver)
                .HasForeignKey(r => r.DriverId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<User>().HasMany(u => u.RidesAsRider)
                .WithOne(r => r.Rider)
                .HasForeignKey(r => r.RiderId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Ride>().HasOne(r => r.Payment)
                .WithOne(p => p.Ride)
                .HasForeignKey<Payment>(p => p.RideId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Ride>().HasOne(r => r.Rating)
                .WithOne(rt => rt.Ride)
                .HasForeignKey<Rating>(rt => rt.RideId)
                .OnDelete(DeleteBehavior.Restrict);


            // 1. Seed Users (Drivers and Riders)
            modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, FullName = "John Doe", Email = "john.doe@example.com", Password = "hashed_password_123", Phone = "+15551234567", Role = "Driver" },
                new User { UserId = 2, FullName = "Jane Smith", Email = "jane.smith@example.com", Password = "hashed_password_456", Phone = "+15559876543", Role = "Rider" },
                new User { UserId = 3, FullName = "Bob Johnson", Email = "bob.johnson@example.com", Password = "hashed_password_789", Phone = "+15551112233", Role = "Driver" },
                new User { UserId = 4, FullName = "Alice Brown", Email = "alice.brown@example.com", Password = "hashed_password_abc", Phone = "+15554445566", Role = "Rider" },

                // --- Additional Seeded Users ---
                new User { UserId = 5, FullName = "Carlos Mendez", Email = "carlos.m@example.com", Password = "hashed_password_def", Phone = "+15552223344", Role = "Driver" },
                new User { UserId = 6, FullName = "Sarah Jenkins", Email = "sarah.j@example.com", Password = "hashed_password_ghi", Phone = "+15553334455", Role = "Rider" },
                new User { UserId = 7, FullName = "David Kim", Email = "david.k@example.com", Password = "hashed_password_jkl", Phone = "+15554448899", Role = "Driver" },
                new User { UserId = 8, FullName = "Emily Watson", Email = "emily.w@example.com", Password = "hashed_password_mno", Phone = "+15557778811", Role = "Rider" }
            );

            // 2. Seed Vehicles (1:1 with Drivers)
            modelBuilder.Entity<Vehicle>().HasData(
                new Vehicle { VehicleId = 1, PlateNumber = "XYZ-1234", Color = "Silver", DriverId = 1 },
                new Vehicle { VehicleId = 2, PlateNumber = "ABC-5678", Color = "Black", DriverId = 3 },

                // --- Additional Seeded Vehicles ---
                new Vehicle { VehicleId = 3, PlateNumber = "EV-999X", Color = "White", DriverId = 5 },
                new Vehicle { VehicleId = 4, PlateNumber = "HYU-4421", Color = "Blue", DriverId = 7 }
            );

            // 3. Seed Rides (1:M with Riders and Drivers)
            modelBuilder.Entity<Ride>().HasData(
                new Ride { RideId = 1, PickupLocation = "123 Main St, Downtown", DropOffLocation = "456 Oak Ave, Suburbs", Fare = 25.50m, Status = "Completed", RiderId = 2, DriverId = 1 },
                new Ride { RideId = 2, PickupLocation = "789 Pine Rd, Airport", DropOffLocation = "101 Maple Blvd, University", Fare = 45.00m, Status = "Completed", RiderId = 4, DriverId = 3 },
                new Ride { RideId = 3, PickupLocation = "555 Elm St, Mall", DropOffLocation = "222 Birch Ln, Hospital", Fare = 30.00m, Status = "Pending", RiderId = 2, DriverId = 1 },

                // --- Additional Seeded Rides ---
                // Completed ride with high fare
                new Ride { RideId = 4, PickupLocation = "Corporate Plaza, Tech District", DropOffLocation = "Golf Club Estates", Fare = 120.00m, Status = "Completed", RiderId = 6, DriverId = 5 },
                // Cancelled ride scenario
                new Ride { RideId = 5, PickupLocation = "Central Station", DropOffLocation = "Arts Museum", Fare = 20.00m, Status = "Cancelled", RiderId = 8, DriverId = 7 },
                // Repeat rider scenario (Jane Smith rides again)
                new Ride { RideId = 6, PickupLocation = "456 Oak Ave, Suburbs", DropOffLocation = "123 Main St, Downtown", Fare = 28.00m, Status = "Completed", RiderId = 2, DriverId = 7 },
                // In-progress active ride
                new Ride { RideId = 7, PickupLocation = "Stadium Gate A", DropOffLocation = "Hotel District", Fare = 35.50m, Status = "Ongoing", RiderId = 6, DriverId = 3 }
            );

            // 4. Seed Payments (1:1 with Rides)
            modelBuilder.Entity<Payment>().HasData(
                new Payment { PaymentId = 1, Amount = 25.50m, PaymentMethod = "Credit Card", PaymentDate = new DateTime(2023, 10, 25, 14, 30, 0, DateTimeKind.Utc), Status = "Paid", RideId = 1 },
                new Payment { PaymentId = 2, Amount = 45.00m, PaymentMethod = "Digital Wallet", PaymentDate = new DateTime(2023, 10, 25, 16, 15, 0, DateTimeKind.Utc), Status = "Paid", RideId = 2 },

                // --- Additional Seeded Payments ---
                new Payment { PaymentId = 3, Amount = 120.00m, PaymentMethod = "Credit Card", PaymentDate = new DateTime(2023, 10, 26, 09, 10, 0, DateTimeKind.Utc), Status = "Paid", RideId = 4 },
                // Failed payment mapping to the cancelled ride
                new Payment { PaymentId = 4, Amount = 20.00m, PaymentMethod = "Digital Wallet", PaymentDate = new DateTime(2023, 10, 26, 11, 00, 0, DateTimeKind.Utc), Status = "Failed", RideId = 5 },
                new Payment { PaymentId = 5, Amount = 28.00m, PaymentMethod = "Cash", PaymentDate = new DateTime(2023, 10, 26, 18, 45, 0, DateTimeKind.Utc), Status = "Paid", RideId = 6 }
            // Note: RideId 3 (Pending) and RideId 7 (Ongoing) do not have payments yet to reflect realistic state flow.
            );

            // 5. Seed Ratings (1:1 with Rides)
            modelBuilder.Entity<Rating>().HasData(
                new Rating { RatingId = 1, Score = 5, Comment = "Excellent service, very clean car.", RideId = 1 },
                new Rating { RatingId = 2, Score = 4, Comment = "Smooth ride, arrived on time.", RideId = 2 },

                // --- Additional Seeded Ratings ---
                new Rating { RatingId = 3, Score = 5, Comment = "Amazing electric vehicle experience! Very professional driver.", RideId = 4 },
                // Lower rating scenario to test filtering
                new Rating { RatingId = 4, Score = 2, Comment = "Driver took a very long route and didn't speak English well.", RideId = 6 }
            // Note: Cancelled, Pending, and Ongoing rides purposefully do not receive ratings.
            );
        }
    }
}
