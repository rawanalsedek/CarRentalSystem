using BLogicLayer.Interfaces;
using BLogicLayer.ViewModels;
using DataAccessLayer.Data;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLogicLayer.Services
{
    public class CarService : ICarService
    {
        private readonly CarRentalDbContext _context;

        public CarService(CarRentalDbContext context)
        {
            _context = context;
        }

        public List<Car> GetAll()
        {
            return _context.Cars
                .Include(c => c.Category)
                .ToList();
        }

        public Car? GetById(int id)
        {
            return _context.Cars
                .Include(c => c.Category)
                .FirstOrDefault(c => c.Id == id);
        }

        public List<Car> Search(CarSearchViewModel search)
        {
            var query = _context.Cars
                .Include(c => c.Category)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search.Brand))
            {
                query = query.Where(c => c.Brand != null && c.Brand.Contains(search.Brand));
            }

            if (!string.IsNullOrWhiteSpace(search.Model))
            {
                query = query.Where(c => c.Model != null && c.Model.Contains(search.Model));
            }

            if (search.CategoryId.HasValue && search.CategoryId.Value > 0)
            {
                query = query.Where(c => c.CategoryId == search.CategoryId.Value);
            }

            if (search.MinPrice.HasValue)
            {
                query = query.Where(c => c.PricePerDay >= search.MinPrice.Value);
            }

            if (search.MaxPrice.HasValue)
            {
                query = query.Where(c => c.PricePerDay <= search.MaxPrice.Value);
            }

            return query.ToList();
        }

        public int GetCount()
        {
            return _context.Cars.Count();
        }

        public void Add(Car entity)
        {
            _context.Cars.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Car entity)
        {
            _context.Cars.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var car = _context.Cars
                .Include(c => c.Reservations)
                .FirstOrDefault(c => c.Id == id);

            if (car == null)
            {
                return;
            }

            if (car.Reservations != null && car.Reservations.Count > 0)
            {
                throw new InvalidOperationException(
                    "Cannot delete car that has reservations.");
            }

            _context.Cars.Remove(car);
            _context.SaveChanges();
        }
    }
}
