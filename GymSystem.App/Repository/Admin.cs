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
                var ret = m.PersonSet.Add(p); //This function adds 'p' without Address
                m.SaveChanges();
                return ret.Entity;
            }
        }
        public void ModifyCustomer(Person p)
        {
            using (var m = new Model())
            {
                var ret = m.PersonSet.Update(p);
                m.SaveChanges();
            }
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
        public async Task<Person> GetCustomer(int id)
        {
            using (var m = new Model())
            {
               return m.PersonSet.FirstOrDefault(x => x.Id == id);
              
            }
        }
        public void AddEntrance(Entrance en)
        {
            using (var m = new Model())
            {
                var ret = m.EntranceSet.Add(en);
                m.SaveChanges();
            }
        }
        public Task ModifyEntrance(Entrance en)
        {
            throw new NotImplementedException();
        }
        public Task AddEnteranceLog(Entrance p, DateTime date)
        {
            throw new NotImplementedException();
        }

        public Task<Entrance> GetCustomerEntrances(Person p)
        {
            throw new NotImplementedException();

        }

        public Entrance GetEntrance(int entranceid)
        {
            throw new NotImplementedException();
        }

        public Task<DateTime> GetEnteranceLog(Entrance p)
        {
            throw new NotImplementedException();
        }

        public List<Entrance> GetAllEntrances()
        {
            using (var m = new Model())
            {
                return m.EntranceSet.ToList();
            }
        }
        
        public Task IsValidEnterance(int enteranceid)
        {
            throw new NotImplementedException();
        }

        

        public Task<Person> SearchCustomerByEmail(string email = "")
        {
            throw new NotImplementedException();
        }
        public async Task<List<Person>> GetCustomers()
        {
            using (var m = new Model())
            {
                return m.PersonSet.ToList();
            }
        }
        public void AddEntranceType(EntranceType et)
        {
            var ret = m.EntranceTypeSet.Add(et);
            m.SaveChanges();
        }
        public void ModifyEntranceType(EntranceType et)
        {
            m.EntranceTypeSet.Update(et);
            m.SaveChanges();
        }
        public async Task DeleteEntranceType(EntranceType et)
        {
            await Task.Run(() =>
            {
                using (var m = new Model())
                {
                    var ret = m.EntranceTypeSet.Remove(et);
                    m.SaveChanges();
                }
            });
        }
        public List<EntranceType> GetAllEntranceTypes()
        {
            using (var m = new Model())
            {
                return m.EntranceTypeSet.ToList();
            }
        }
    }
}
