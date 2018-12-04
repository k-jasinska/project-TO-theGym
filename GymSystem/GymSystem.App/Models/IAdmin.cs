using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystem.App.Models
{
    public interface IAdmin
    {
        Task AddCustomer(Customer p);
        Task DeleteCustomer(Customer p);
        Task ModifyCustomer(Customer p);
        Task<Entrance> GetCustomerEntrances(Customer p);
        Task AddEnterance(Entrance p);
        Task<Customer> SearchCustomerByEmail(string email = "");
        Task AddEnteranceLog(Entrance p, DateTime date);
        Task<DateTime> GetEnteranceLog(Entrance p);
        Task IsValidEnterance(int enteranceid);
        Task GetEnterance(int enteranceid);
        Task<Entrance> GetEnterances();
    }
}
