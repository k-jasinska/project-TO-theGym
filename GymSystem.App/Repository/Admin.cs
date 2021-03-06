﻿using GymSystem.Db;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymSystem.App.Models
{
    public class Admin : IAdmin
    {
        public async Task<Person> AddCustomer(Person p)
        {
            using (var m = new Model())
            {
                var ret = m.PersonSet.Add(p);
                m.SaveChanges();
                return ret.Entity;
            }
        }
        public void ModifyCustomer(Person p)
        {
            using (var m = new Model())
            {
                m.PersonSet.Update(p);
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
        public Person GetCustomer(int id)
        {
            using (var m = new Model())
            {
                Person ret = m.PersonSet
                    .Include(p => p.Adress)
                    .Include(p => p.Entrance)
                    .ThenInclude(p => p.EntranceLog)
                    .Include(p => p.Entrance)
                    .ThenInclude(p => p.EntranceType)
                    .FirstOrDefault(x => x.Id == id);
                return ret;
            }
        }
        public async Task<List<Person>> GetCustomers()
        {
            using (var m = new Model())
            {
                return m.PersonSet.Include(p => p.Adress).ToList();
            }
        }
        public Task<Entrance> GetCustomerEntrances(Person p)
        {
            throw new NotImplementedException();
        }
        public Task<Person> SearchCustomerByEmail(string email = "")
        {
            throw new NotImplementedException();
        }

        public void AddEntrance(Entrance en)
        {
            using (var m = new Model())
            {
                var ret = m.EntranceSet.Attach(en);
                m.SaveChanges();
            }
        }
        public void ModifyEntrance(Entrance en)
        {
            using (var m = new Model())
            {
                var ret = m.EntranceSet.Update(en);
                m.SaveChanges();
            }
        }
        public async Task DeleteEntrance(Entrance en)
        {
            await Task.Run(() =>
            {
                using (var m = new Model())
                {
                    var ret = m.EntranceSet.Remove(en);
                    m.SaveChanges();
                }
            });
        }
        public Entrance GetEntrance(int entranceid)
        {
            using (var m = new Model())
            {
                return m.EntranceSet.Include(e => e.EntranceType).Include(e => e.EntranceLog).FirstOrDefault(x => x.Id == entranceid);
            }
        }
        public async Task AddEntranceLog(Entrance en, DateTimeOffset date)
        {
            using (var m = new Model())
            {
                EntranceLog el = new EntranceLog
                {
                    Date = date,
                    Entrance = m.EntranceSet.First(x => x.Id == en.Id),

                };
                await m.EntranceLogSet.AddAsync(el);
                m.SaveChanges();
            }
        }

        public Task<DateTime> GetEntranceLog(Entrance p)
        {
            throw new NotImplementedException();
        }
        public List<Entrance> GetAllEntrances()
        {
            using (var m = new Model())
            {
                return m.EntranceSet.Include(e => e.EntranceType).Include(e => e.EntranceLog).Include(e => e.Person).ToList();
            }
        }
        public bool IsValidEntrance(int entranceid)
        {
            using (var m = new Model())
            {
                var en = m.EntranceSet.FirstOrDefault(x => x.Id == entranceid);
                if (en.BeginDate <= DateTimeOffset.Now && en.EndDate >= DateTimeOffset.Now) return true;
                else return false;
            }
        }



        public void AddEntranceType(EntranceType et)
        {
            using (var m = new Model())
            {
                var ret = m.EntranceTypeSet.Add(et);
                m.SaveChanges();
            }
        }
        public void ModifyEntranceType(EntranceType et)
        {
            using (var m = new Model())
            {
                m.EntranceTypeSet.Update(et);
                m.SaveChanges();
            }
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
