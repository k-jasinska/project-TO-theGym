using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystem.App.Models
{
    public class Entrance
    {
        public Entrance()
        {
        }

        public Entrance(Customer customer) : this()
        {
            Customer = customer;
            Name = customer.Name;
            Surname = customer.Surname;
        }

        public int EnteranceId { get; set; }
        public Customer Customer { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public EntranceType TName { get; set; }
        public EntranceStatus Status { get; set; } = EntranceStatus.Niewazny;

        public enum EntranceStatus
        {
            Wazny,
            Niewazny
        }
    }
}
