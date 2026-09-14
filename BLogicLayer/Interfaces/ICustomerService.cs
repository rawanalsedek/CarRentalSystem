using BLogicLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLogicLayer.Interfaces
{
    public interface ICustomerService
    {
        List<CustomerViewModel> GetAll();

        int GetCount();
    }
}
