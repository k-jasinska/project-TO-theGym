using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystem.App.Models
{
    public class Admin : IAdmin
    {
        public Task AddCustomer(Customer p)
        {
            throw new NotImplementedException();
        }

        public Task AddEnterance(Entrance p)
        {
            throw new NotImplementedException();
        }

        public Task AddEnteranceLog(Entrance p, DateTime date)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCustomer(Customer p)
        {
            throw new NotImplementedException();
        }

        public Task<Entrance> GetCustomerEntrances(Customer p)
        {
            throw new NotImplementedException();
        }

        public Task GetEnterance(int enteranceid)
        {
            throw new NotImplementedException();
        }

        public Task<DateTime> GetEnteranceLog(Entrance p)
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

        public Task ModifyCustomer(Customer p)
        {
            throw new NotImplementedException();
        }

        public Task<Customer> SearchCustomerByEmail(string email = "")
        {
            throw new NotImplementedException();
        }
    }
}
