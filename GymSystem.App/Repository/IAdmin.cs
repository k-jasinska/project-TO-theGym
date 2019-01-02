using GymSystem.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystem.App.Models
{
    public interface IAdmin
    {
        Task<Person> AddCustomer(Person p);
        Task DeleteCustomer(Person p);
        void ModifyCustomer(Person p);
        Task<Entrance> GetCustomerEntrances(Person p);
        Task AddEnterance(Entrance p);
        Task<Person> SearchCustomerByEmail(string email = "");
        Task AddEnteranceLog(Entrance p, DateTime date);
        Task<DateTime> GetEnteranceLog(Entrance p);
        Task IsValidEnterance(int enteranceid);
        Entrance GetEntrance(int entranceid);
        Task<Entrance> GetEnterances();
        Task<List<Person>> GetCustomers();
        Task<Person> GetCustomer(int id);
    }
}
