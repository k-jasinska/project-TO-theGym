using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystem.App.ViewModels
{
    public class AddClientViewModel
    {

        public List<string> EnteranceTypes { get; set; } = new List<string>() { "Normal", "Premium" };

        private string _EnteranceTypeSelected;

        public string EnteranceTypeSelected
        {
            get { return _EnteranceTypeSelected; }
            set { _EnteranceTypeSelected = value; }
        }

    }
}
