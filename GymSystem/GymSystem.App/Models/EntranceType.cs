using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystem.App.Models
{
    public class EntranceType
    {
        public double Price { get; set; }
        public enum Type
        {
            Normal,
            Premium,
            None
        }
        public int Duration { get; set; }
    }
}
