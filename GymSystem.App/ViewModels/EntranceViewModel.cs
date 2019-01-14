using System;
using System.ComponentModel;
using System.Threading.Tasks;
using GymSystem.App.Models;
using GymSystem.App.ViewModels;
using GymSystem.Db;
namespace GymSystem.App
{
    public class EntranceViewModel : BindableBase, IEditableObject
    {
        private Entrance _model;
        private string _name;
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

        public Action<object, EventArgs> AddEntranceCanceled { get; internal set; }

        public EntranceViewModel(Entrance en = null) //constructor
        {
            if (en == null)
                en = new Entrance();
            this._model = en;
        }

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
        public System.Collections.Generic.ICollection<EntranceLog> EntranceLog
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

        public bool IsModified { get; private set; }
        public async Task SaveAsync()
        {
            IsInEdit = false;
            IsModified = false;
            if (IsNewEntrance)
            {
                IsNewEntrance = false;
                App.Repository.AddEntrance(Model);
            }
            else
            {
                await App.Repository.ModifyEntrance(Model);
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
    }
}