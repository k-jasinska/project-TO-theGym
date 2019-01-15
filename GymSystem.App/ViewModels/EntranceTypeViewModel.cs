using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymSystem.Db;

namespace GymSystem.App.ViewModels
{
    public class EntranceTypeViewModel : BindableBase
    {
        private EntranceType _model;
        public EntranceTypeViewModel(EntranceType en = null) //constructor
        {
            if (en == null)
                en = new EntranceType();
            this._model = en;
        }
        public EntranceType Model
        {
            get => _model;
            set
            {
                if (_model != value)
                {
                    _model = value;
                }
            }
        }
        public int Id
        {
            get => Model.Id;
            set
            {
                Model.Id = value;
                EndEdit();
            }
        }
        public int Duration
        {
            get => Model.Duration;
            set
            {
                Model.Duration = value;
                EndEdit();
            }
        }
        public float Price
        {
            get => (float)Model.Price;
            set
            {
                Model.Price = (decimal)value;
                EndEdit();
            }
        }
        public string Name
        {
            get => Model.Name;
            set
            {
                Model.Name = value;
                EndEdit();
            }
        }
        public void EndEdit() => SaveTypes();
        public void SaveTypes()
        {
            App.Repository.ModifyEntranceType(Model);
        }
    }
}
