using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLogicLayer.Interfaces
{
    public interface ICategoryService
    {
        List<Category> GetAll();

        Category? GetById(int id);

        void Add(Category entity);

        void Update(Category entity);

        void Delete(int id);
    }
}
