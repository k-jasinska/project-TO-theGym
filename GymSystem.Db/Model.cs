using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSystem.Db
{
    public partial class Model : DbContext
    {
    
    
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=GymSystem.db");
        }

        public virtual DbSet<Person> PersonSet { get; set; }
        public virtual DbSet<Entrance> EntranceSet { get; set; }
        public virtual DbSet<EntranceType> EntranceTypeSet { get; set; }
        public virtual DbSet<EntranceLog> EntranceLogSet { get; set; }
        public virtual DbSet<Address> AddressSet { get; set; }
    }
    public partial class Entrance
    {
  
        public int Id { get; set; }
        public System.DateTimeOffset BeginDate { get; set; }
        public System.DateTimeOffset EndDate { get; set; }

        public virtual Person Person { get; set; }
        public virtual EntranceType EntranceType { get; set; } //Price, duration depends on type
        public virtual ICollection<EntranceLog> EntranceLog { get; set; }
    }
    public partial class Address
    {

        public int Id { get; set; }
        public string Street { get; set; }
        public string Code { get; set; }
        public string City { get; set; }

        public virtual ICollection<Person> Person { get; set; }
    }
    public partial class EntranceLog
    {
        public int Id { get; set; }
        public string Date { get; set; }

        public virtual Entrance Entrance { get; set; }
    }
    public partial class EntranceType
    {

        public int Id { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }

        public virtual ICollection<Entrance> Entrance { get; set; }
    }
    public partial class Person
    {
        public Person()
        {
            Adress = new Address();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Pesel { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }

        public virtual ICollection<Entrance> Entrance { get; set; }
        public virtual Address Adress { get; set; }
    }
}
