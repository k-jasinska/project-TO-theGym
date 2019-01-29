using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using GymSystem.App.Models;
using GymSystem.App.ViewModels;
using GymSystem.Db;
namespace GymSystem.App
{
    public class EntranceViewModel : BindableBase, IEditableObject
    {
        private Entrance _model;
        public EntranceViewModel(Entrance en = null) //constructor
        {
            if (en == null)
            {
                en = new Entrance();
                en.BeginDate = new DateTimeOffset(DateTime.Now);
                en.EndDate = new DateTimeOffset(DateTime.Now).AddDays(1);
                en.EntranceType = new EntranceType();
                en.EntranceLog = new List<EntranceLog>();
                IsNewEntrance = true;
            }
            this._model = en;
        }

        //this constructor tries to retrieve Entrance from given Person
        public EntranceViewModel(Person p) //constructor
        {
            Entrance en = new Entrance();
            if(p != null && p.Entrance != null && p.Entrance.Count > 0)
            {
                en = p.Entrance[0];
                IsNewEntrance = false;
            }
            else if (p != null)
            {
                en.Person = p;
                en.BeginDate = new DateTimeOffset(DateTime.Now);
                en.EndDate = new DateTimeOffset(DateTime.Now).AddDays(1);
                en.EntranceType = new EntranceType();
                en.EntranceLog = new List<EntranceLog>();
                IsNewEntrance = true;
            }
            else
            {
                en.BeginDate = new DateTimeOffset(DateTime.Now);
                en.EndDate = new DateTimeOffset(DateTime.Now).AddDays(1);
                en.EntranceType = new EntranceType();
                en.EntranceLog = new List<EntranceLog>();
                IsNewEntrance = true;
            }
            _model = en;
        }
        public Entrance Model
        {
            get => _model;
            set
            {
                if (_model != value)
                {
                    _model = value;
                    OnPropertyChanged(string.Empty);
                }
            }
        }

        private bool _isNewEntrance;

        public bool IsNewEntrance
        {
            get => _isNewEntrance;
            set => Set(ref _isNewEntrance, value);
        }

        private bool _isInEdit = false;

        public bool IsInEdit
        {
            get => _isInEdit;
            set => Set(ref _isInEdit, value);
        }


        public string SelectedType
        {
            get => Model.EntranceType.Name;
            set
            {
                List<EntranceType> list = App.Repository.GetAllEntranceTypes();
                foreach (EntranceType et in list)
                {
                    if(et.Name==value)
                    {
                        Model.EntranceType = et;
                        EntranceType = et;
                        Price = (float)et.Price;
                        EndDate = BeginDate.AddDays(et.Duration);
                    }
                }
            }
        }
        public Action<object, EventArgs> AddEntranceCanceled { get; internal set; }

        

        public int Id
        {
            get => Model.Id;
            set
            {
                if (value != Model.Id)
                {
                    Model.Id = value;
                    IsModified = true;
                    OnPropertyChanged();
                }
            }
        }
        public DateTimeOffset BeginDate
        {
            get => Model.BeginDate;
            set
            {
                if (value != Model.BeginDate)
                {
                    Model.BeginDate = value;
                    EndDate = value.AddDays(EntranceType.Duration);
                    IsModified = true;
                    OnPropertyChanged();
                }
            }
        }
        public DateTimeOffset EndDate
        {
            get => Model.EndDate;
            set
            {
                if (value != Model.EndDate)
                {
                    Model.EndDate = value;
                    IsModified = true;
                    OnPropertyChanged();
                }
            }
        }
        public Person Person
        {
            get => Model.Person;
            set
            {
                if (value != Model.Person)
                {
                    Model.Person = value;
                    IsModified = true;
                    OnPropertyChanged();
                }
            }
        }
        public EntranceType EntranceType
        {
            get => Model.EntranceType;
            set
            {
                if (value != Model.EntranceType)
                {
                    Model.EntranceType = value;
                    IsModified = true;
                    OnPropertyChanged();
                }
            }
        }
        public float Price
        {
            get => (float)Model.EntranceType.Price;
            set
            {
                OnPropertyChanged();
            }
        }
        public bool IsValidEntrance
        {
            get => Model.BeginDate <= DateTimeOffset.Now && Model.EndDate >= DateTimeOffset.Now;
        }
        public ICollection<EntranceLog> EntranceLog
        {
            get => Model.EntranceLog;
            set
            {
                if (value != Model.EntranceLog)
                {
                    Model.EntranceLog = value;
                    IsModified = true;
                    OnPropertyChanged();
                }
            }
        }
        public List<string> TicketTypes
        {
            get => GetTicketTypesNames();
        }
        public bool IsModified { get; private set; }
        public async Task SaveAsync()
        {
            IsInEdit = false;
            IsModified = false;
            try
            {
                if (IsNewEntrance)
                {
                    IsNewEntrance = false;
                    App.Repository.AddEntrance(Model);
                }
                else
                {
                    App.Repository.ModifyEntrance(Model);
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void StartEdit() => IsInEdit = true;
        public async void EndEdit() => await SaveAsync();

        public void BeginEdit()
        {
            //  throw new NotImplementedException();
        }

        public void CancelEdit()
        {
            // throw new NotImplementedException();
        }
        public async Task CancelEditsAsync()
        {
            AddEntranceCanceled?.Invoke(this, EventArgs.Empty);
        }
        public async Task RevertChangesAsync()
        {
            IsInEdit = false;
            if (IsModified)
            {
                RefreshCustomer();
                IsModified = false;
            }
        }
        public void RefreshCustomer()
        {
            Model = App.Repository.GetEntrance(Model.Id);
        }
        
        public List<string> GetTicketTypesNames()
        {
            var list = App.Repository.GetAllEntranceTypes();
            var ret = new List<string>();
            foreach(EntranceType et in list) {
                ret.Add(et.Name);
            }
            return ret;
        }
    }
}