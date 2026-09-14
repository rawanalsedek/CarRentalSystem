using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLogicLayer.Interfaces
{
    public interface IReservationService
    {
        List<Reservation> GetAll();

        Reservation? GetById(int id);

        List<Reservation> GetByUserId(string userId);

        int GetCount();

        int GetPendingCount();

        bool HasOverlap(int carId, DateTime startDate, DateTime endDate, int? excludeReservationId = null);

        decimal CalculateTotalPrice(decimal pricePerDay, DateTime startDate, DateTime endDate);

        void Add(Reservation entity);

        void Update(Reservation entity);

        void ChangeStatus(int id, ReservationStatus status);

        void Delete(int id);
    }
}
