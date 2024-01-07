using HotelProject.BL.Model.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.BL.Interfaces
{
    public interface ICustomerRepository
    {
        int AddCustomer(Customer customer);
        void DeleteCustomer(int id);
        List<Customer> GetCustomers(string filter);
        void UpdateCustomer(Customer customer);
    }
}
