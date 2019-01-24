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
        void ModifyCustomer(Person p);
        Task DeleteCustomer(Person p);
        Person GetCustomer(int id);
        Task<List<Person>> GetCustomers();
        Task<Entrance> GetCustomerEntrances(Person p);
        Task<Person> SearchCustomerByEmail(string email = "");

        void AddEntrance(Entrance en);
        void ModifyEntrance(Entrance en);
        Task DeleteEntrance(Entrance en);
        Entrance GetEntrance(int entranceid);
        List<Entrance> GetAllEntrances();
        Task AddEntranceLog(Entrance en, DateTimeOffset date);
        Task<DateTime> GetEntranceLog(Entrance p);
        bool IsValidEntrance(int entranceid);

        void AddEntranceType(EntranceType et);
        void ModifyEntranceType(EntranceType et);
        Task DeleteEntranceType(EntranceType et);
        List<EntranceType> GetAllEntranceTypes();
    }
}
