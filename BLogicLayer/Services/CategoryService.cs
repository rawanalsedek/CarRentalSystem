using BLogicLayer.Interfaces;
using DataAccessLayer.Data;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLogicLayer.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly CarRentalDbContext _context;

        public CategoryService(CarRentalDbContext context)
        {
            _context = context;
        }

        public List<Category> GetAll()
        {
            return _context.Categories
                .Include(c => c.Cars)
                .ToList();
        }

        public Category? GetById(int id)
        {
            return _context.Categories
                .FirstOrDefault(c => c.Id == id);
        }

        public void Add(Category entity)
        {
            _context.Categories.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Category entity)
        {
            _context.Categories.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var category = _context.Categories
                .Include(c => c.Cars)
                .FirstOrDefault(c => c.Id == id);

            if (category == null)
            {
                return;
            }

            if (category.Cars != null && category.Cars.Count > 0)
            {
                throw new InvalidOperationException(
                    "Cannot delete category that has cars.");
            }

            _context.Categories.Remove(category);
            _context.SaveChanges();
        }
    }
}
