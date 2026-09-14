using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Data
{
    public class CarRentalDbContext : IdentityDbContext<User>
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=CarRental;Trusted_Connection=True;TrustServerCertificate=True");
        }

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Car>()
                .Property(c => c.PricePerDay)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Reservation>()
                .Property(r => r.TotalPrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Car>()
                .HasOne(c => c.Category)
                .WithMany(cat => cat.Cars)
                .HasForeignKey(c => c.CategoryId);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Car)
                .WithMany(c => c.Reservations)
                .HasForeignKey(r => r.CarId);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reservations)
                .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Sedan" },
                new Category { Id = 2, Name = "SUV" },
                new Category { Id = 3, Name = "Sports" },
                new Category { Id = 4, Name = "Economy" },
                new Category { Id = 5, Name = "Luxury" }
            );

            const int sedan = 1;
            const int suv = 2;
            const int sports = 3;
            const int economy = 4;
            const int luxury = 5;

            Car Seed(
                int id,
                string brand,
                string model,
                int year,
                decimal price,
                int categoryId,
                string imageFile,
                CarStatus status = CarStatus.Available)
            {
                return new Car
                {
                    Id = id,
                    Brand = brand,
                    Model = model,
                    Year = year,
                    PricePerDay = price,
                    Status = status,
                    CategoryId = categoryId,
                    ImageUrl = $"/images/cars/{imageFile}"
                };
            }

            modelBuilder.Entity<Car>().HasData(
                Seed(1, "BMW", "5 Series", 2024, 95, sedan, "bmw-5-series.jpg"),
                Seed(2, "Mercedes", "C Class", 2024, 120, luxury, "mercedes-c-class.jpg"),
                Seed(3, "Toyota", "RAV4", 2024, 65, suv, "toyota-rav4.jpg"),
                Seed(4, "Audi", "A6", 2023, 110, luxury, "audi-a6.jpg", CarStatus.Rented),
                Seed(5, "BMW", "X5", 2024, 105, suv, "bmw-x5.jpg"),
                Seed(6, "Toyota", "Camry", 2024, 55, sedan, "toyota-camry.jpg"),

                Seed(7, "Toyota", "Corolla", 2023, 45, sedan, "toyota-corolla.jpg"),
                Seed(8, "Toyota", "Yaris", 2024, 35, economy, "toyota-yaris.jpg"),
                Seed(9, "Toyota", "Land Cruiser", 2024, 145, suv, "toyota-land-cruiser.jpg"),

                Seed(10, "BMW", "3 Series", 2024, 85, sedan, "bmw-3-series.jpg"),
                Seed(11, "BMW", "X3", 2024, 90, suv, "bmw-x3.jpg"),
                Seed(12, "BMW", "7 Series", 2024, 180, luxury, "bmw-7-series.jpg"),

                Seed(13, "Mercedes", "E-Class", 2024, 140, luxury, "mercedes-e-class.jpg"),
                Seed(14, "Mercedes", "S-Class", 2023, 220, luxury, "mercedes-s-class.jpg"),
                Seed(15, "Mercedes", "GLC", 2024, 125, suv, "mercedes-glc.jpg"),
                Seed(16, "Mercedes", "GLE", 2024, 155, suv, "mercedes-gle.jpg"),

                Seed(17, "Audi", "A3", 2024, 70, sedan, "audi-a3.jpg"),
                Seed(18, "Audi", "A4", 2024, 85, sedan, "audi-a4.jpg"),
                Seed(19, "Audi", "Q3", 2023, 80, suv, "audi-q3.jpg"),
                Seed(20, "Audi", "Q5", 2024, 100, suv, "audi-q5.jpg"),

                Seed(21, "Hyundai", "Elantra", 2024, 42, sedan, "hyundai-elantra.jpg"),
                Seed(22, "Hyundai", "Sonata", 2023, 50, sedan, "hyundai-sonata.jpg"),
                Seed(23, "Hyundai", "Tucson", 2024, 58, suv, "hyundai-tucson.jpg"),
                Seed(24, "Hyundai", "Santa Fe", 2024, 68, suv, "hyundai-santa-fe.jpg"),
                Seed(25, "Hyundai", "Accent", 2023, 32, economy, "hyundai-accent.jpg"),

                Seed(26, "Kia", "Cerato", 2023, 40, sedan, "kia-cerato.jpg"),
                Seed(27, "Kia", "Sportage", 2024, 55, suv, "kia-sportage.jpg"),
                Seed(28, "Kia", "Sorento", 2024, 70, suv, "kia-sorento.jpg"),
                Seed(29, "Kia", "K5", 2024, 52, sedan, "kia-k5.jpg"),
                Seed(30, "Kia", "Picanto", 2023, 28, economy, "kia-picanto.jpg", CarStatus.Rented),

                Seed(31, "Ford", "Focus", 2023, 38, economy, "ford-focus.jpg"),
                Seed(32, "Ford", "Fusion", 2022, 45, sedan, "ford-fusion.jpg", CarStatus.Maintenance),
                Seed(33, "Ford", "Mustang", 2024, 150, sports, "ford-mustang.jpg"),
                Seed(34, "Ford", "Explorer", 2024, 85, suv, "ford-explorer.jpg"),
                Seed(35, "Ford", "Escape", 2023, 60, suv, "ford-escape.jpg"),

                Seed(36, "Honda", "Civic", 2024, 48, sedan, "honda-civic.jpg"),
                Seed(37, "Honda", "Accord", 2024, 55, sedan, "honda-accord.jpg"),
                Seed(38, "Honda", "CR-V", 2024, 62, suv, "honda-cr-v.jpg"),
                Seed(39, "Honda", "City", 2023, 36, economy, "honda-city.jpg", CarStatus.Maintenance),
                Seed(40, "Honda", "HR-V", 2024, 52, suv, "honda-hr-v.jpg"),

                Seed(41, "Nissan", "Sunny", 2023, 30, economy, "nissan-sunny.jpg", CarStatus.Rented),
                Seed(42, "Nissan", "Sentra", 2024, 40, sedan, "nissan-sentra.jpg"),
                Seed(43, "Nissan", "Altima", 2024, 48, sedan, "nissan-altima.jpg"),
                Seed(44, "Nissan", "X-Trail", 2024, 65, suv, "nissan-x-trail.jpg"),
                Seed(45, "Nissan", "Patrol", 2024, 160, suv, "nissan-patrol.jpg"),

                Seed(46, "Chevrolet", "Malibu", 2023, 46, sedan, "chevrolet-malibu.jpg"),
                Seed(47, "Chevrolet", "Cruze", 2022, 38, economy, "chevrolet-cruze.jpg"),
                Seed(48, "Chevrolet", "Tahoe", 2024, 110, suv, "chevrolet-tahoe.jpg"),
                Seed(49, "Chevrolet", "Captiva", 2023, 58, suv, "chevrolet-captiva.jpg"),
                Seed(50, "Chevrolet", "Equinox", 2024, 62, suv, "chevrolet-equinox.jpg")
            );
        }
    }
}
