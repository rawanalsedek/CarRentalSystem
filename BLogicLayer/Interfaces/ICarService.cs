using BLogicLayer.ViewModels;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLogicLayer.Interfaces
{
    public interface ICarService
    {
        List<Car> GetAll();

        Car? GetById(int id);

        List<Car> Search(CarSearchViewModel search);

        int GetCount();

        void Add(Car entity);

        void Update(Car entity);

        void Delete(int id);
    }
}
