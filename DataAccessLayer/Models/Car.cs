using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Models
{
    public class Car
    {
        public int Id { get; set; }

        public string? Brand { get; set; }

        public string? Model { get; set; }

        public int Year { get; set; }

        public decimal PricePerDay { get; set; }

        public CarStatus Status { get; set; }

        public int CategoryId { get; set; }

        public string? ImageUrl { get; set; }

        public Category? Category { get; set; }

        public List<Reservation>? Reservations { get; set; }
    }
}
