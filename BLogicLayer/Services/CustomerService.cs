using BLogicLayer.Interfaces;
using BLogicLayer.ViewModels;
using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLogicLayer.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly CarRentalDbContext _context;

        public CustomerService(CarRentalDbContext context)
        {
            _context = context;
        }

        public List<CustomerViewModel> GetAll()
        {
            var userRole = _context.Roles
                .FirstOrDefault(r => r.Name == "User");

            if (userRole == null)
            {
                return new List<CustomerViewModel>();
            }

            var userIds = _context.UserRoles
                .Where(ur => ur.RoleId == userRole.Id)
                .Select(ur => ur.UserId)
                .ToList();

            return _context.Users
                .Where(u => userIds.Contains(u.Id))
                .Select(u => new CustomerViewModel
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email,
                    ReservationsCount = u.Reservations != null ? u.Reservations.Count : 0
                })
                .ToList();
        }

        public int GetCount()
        {
            return GetAll().Count;
        }
    }
}
