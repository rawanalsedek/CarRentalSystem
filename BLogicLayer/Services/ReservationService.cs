using BLogicLayer.Interfaces;
using DataAccessLayer.Data;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLogicLayer.Services
{
    public class ReservationService : IReservationService
    {
        private readonly CarRentalDbContext _context;

        public ReservationService(CarRentalDbContext context)
        {
            _context = context;
        }

        public List<Reservation> GetAll()
        {
            return _context.Reservations
                .Include(r => r.Car)
                .ThenInclude(c => c!.Category)
                .Include(r => r.User)
                .OrderByDescending(r => r.Id)
                .ToList();
        }

        public Reservation? GetById(int id)
        {
            return _context.Reservations
                .Include(r => r.Car)
                .ThenInclude(c => c!.Category)
                .Include(r => r.User)
                .FirstOrDefault(r => r.Id == id);
        }

        public List<Reservation> GetByUserId(string userId)
        {
            return _context.Reservations
                .Include(r => r.Car)
                .ThenInclude(c => c!.Category)
                .Where(r => r.UserId == userId)
                .OrderByDescending(r => r.Id)
                .ToList();
        }

        public int GetCount()
        {
            return _context.Reservations.Count();
        }

        public int GetPendingCount()
        {
            return _context.Reservations
                .Count(r => r.Status == ReservationStatus.Pending);
        }

        public bool HasOverlap(int carId, DateTime startDate, DateTime endDate, int? excludeReservationId = null)
        {
            var start = startDate.Date;
            var end = endDate.Date;

            return _context.Reservations.Any(r =>
                r.CarId == carId &&
                (r.Status == ReservationStatus.Pending || r.Status == ReservationStatus.Approved) &&
                (excludeReservationId == null || r.Id != excludeReservationId) &&
                start < r.EndDate.Date &&
                end > r.StartDate.Date);
        }

        public decimal CalculateTotalPrice(decimal pricePerDay, DateTime startDate, DateTime endDate)
        {
            var days = (endDate.Date - startDate.Date).Days;

            if (days <= 0)
            {
                days = 1;
            }

            return days * pricePerDay;
        }

        public void Add(Reservation entity)
        {
            var car = _context.Cars.FirstOrDefault(c => c.Id == entity.CarId);

            if (car == null)
            {
                throw new InvalidOperationException("Car must exist.");
            }

            if (car.Status == CarStatus.Maintenance)
            {
                throw new InvalidOperationException("This car is under maintenance and cannot be reserved.");
            }

            if (entity.StartDate.Date >= entity.EndDate.Date)
            {
                throw new InvalidOperationException("Start Date must be before End Date.");
            }

            if (HasOverlap(entity.CarId, entity.StartDate, entity.EndDate))
            {
                throw new InvalidOperationException("This car is already reserved during the selected dates.");
            }

            entity.TotalPrice = CalculateTotalPrice(car.PricePerDay, entity.StartDate, entity.EndDate);
            entity.Status = ReservationStatus.Pending;

            _context.Reservations.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Reservation entity)
        {
            _context.Reservations.Update(entity);
            _context.SaveChanges();
        }

        public void ChangeStatus(int id, ReservationStatus status)
        {
            var reservation = _context.Reservations
                .Include(r => r.Car)
                .FirstOrDefault(r => r.Id == id);

            if (reservation == null)
            {
                return;
            }

            reservation.Status = status;

            if (reservation.Car != null && reservation.Car.Status != CarStatus.Maintenance)
            {
                if (status == ReservationStatus.Approved)
                {
                    reservation.Car.Status = CarStatus.Rented;
                }
                else if (status == ReservationStatus.Completed ||
                         status == ReservationStatus.Cancelled ||
                         status == ReservationStatus.Rejected)
                {
                    var hasOtherApproved = _context.Reservations.Any(r =>
                        r.CarId == reservation.CarId &&
                        r.Id != reservation.Id &&
                        r.Status == ReservationStatus.Approved);

                    if (!hasOtherApproved)
                    {
                        reservation.Car.Status = CarStatus.Available;
                    }
                }
            }

            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var reservation = _context.Reservations
                .FirstOrDefault(r => r.Id == id);

            if (reservation == null)
            {
                return;
            }

            _context.Reservations.Remove(reservation);
            _context.SaveChanges();
        }
    }
}
