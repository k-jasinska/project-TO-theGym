using GymSystem.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystem.App.Models
{
    public class Admin : IAdmin
    {
        Model m;
        public async Task<Person> AddCustomer(Person p)
        {
            using (var m = new Model())
            {
                var ret=  m.PersonSet.Add(p);
                m.SaveChanges();
                return ret.Entity;
            }
        }
        public async Task<Person> GetCustomer(int id)
        {
            using (var m = new Model())
            {
               return m.PersonSet.FirstOrDefault(x => x.Id == id);
              
            }
        }
        public Task AddEnterance(Entrance p)
        {
            throw new NotImplementedException();
        }

        public Task AddEnteranceLog(Entrance p, DateTime date)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteCustomer(Person p)
        {
            await Task.Run(() =>
            {
                using (var m = new Model())
                {
                    var ret = m.PersonSet.Remove(p);
                    m.SaveChanges();
                }
            });
 
        }

        public Task<Entrance> GetCustomerEntrances(Person p)
        {
            throw new NotImplementedException();

        }

        public Task GetEnterance(int enteranceid)
        {
            throw new NotImplementedException();
        }

        public  Task<DateTime> GetEnteranceLog(Entrance p)
        {
            throw new NotImplementedException();
        }

        public Task<Entrance> GetEnterances()
        {
            throw new NotImplementedException();
        }

        public Task IsValidEnterance(int enteranceid)
        {
            throw new NotImplementedException();
        }

        public Task ModifyCustomer(Person p)
        {
            throw new NotImplementedException();
        }

        public Task<Person> SearchCustomerByEmail(string email = "")
        {
            throw new NotImplementedException();
        }
        public async  Task<List<Person>> GetCustomers()
        {
             using (var m = new Model())
            {
              return  m.PersonSet.ToList();
            }
        }
    }
}
