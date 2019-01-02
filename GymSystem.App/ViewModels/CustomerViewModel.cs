using System;
using System.ComponentModel;
using System.Threading.Tasks;
using GymSystem.App.Models;
using GymSystem.App.ViewModels;
using GymSystem.Db;
namespace GymSystem.App
{
    public class CustomerViewModel : BindableBase, IEditableObject
    {
        private Person _model;
        private string _name;
        public Person Model
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

        private bool _isNewCustomer;

        public bool IsNewCustomer
        {
            get => _isNewCustomer;
            set => Set(ref _isNewCustomer, value);
        }

        private bool _isInEdit = false;

        public bool IsInEdit
        {
            get => _isInEdit;
            set => Set(ref _isInEdit, value);
        }

        public Action<object, EventArgs> AddNewCustomerCanceled { get; internal set; }

        public CustomerViewModel(Person c = null) //constructor
        {
            if (c == null)
                c = new Person(); 
            this._model = c;
        }

        public string Name
        {
            get => Model.Name;
            set
            {
                if (value != Model.Name)
                {
                    Model.Name = value;
                    IsModified = true;
                    OnPropertyChanged();
                }
            }
        }
        public string Surname
        {
            get => Model.Surname;
            set
            {
                if (value != Model.Surname)
                {
                    Model.Surname = value;
                    IsModified = true;
                    OnPropertyChanged();
 
                }
            }
        }
        public string Phone
        {
            get => Model.Phone;
            set
            {
                if (value != Model.Phone)
                {
                    Model.Phone = value;
                    IsModified = true;
                    OnPropertyChanged();
                }
            }
        }
        public string Email
        {
            get => Model.Mail;
            set
            {
                if (value != Model.Mail)
                {
                    Model.Mail = value;
                    IsModified = true;
                    OnPropertyChanged();
                }
            }
        }
        public string City
        {
            get => Model.Adress.City;
            set
            {
                if (value != Model.Adress.City)
                {
                    Model.Adress.City = value;
                    IsModified = true;
                    OnPropertyChanged();
                }
            }
        }

        public string Code
        {
            get => Model.Adress.Code;
            set
            {
                if (value != Model.Adress.Code)
                {
                    Model.Adress.Code = value;
                    IsModified = true;
                    OnPropertyChanged();
                }
            }
        }
        public string Street
        {
            get => Model.Adress.Street;
            set
            {
                if (value != Model.Adress.Street)
                {
                    Model.Adress.Street = value;
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
            if (IsNewCustomer)
            {
                IsNewCustomer = false;
                await App.Repository.AddCustomer(Model);
            }
            else
            {
                App.Repository.ModifyCustomer(Model);
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
            if (IsNewCustomer)
            {
                AddNewCustomerCanceled?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                await RevertChangesAsync();
            }
        }
        public async Task RevertChangesAsync()
        {
            IsInEdit = false;
            if (IsModified)
            {
                await RefreshCustomerAsync();
                IsModified = false;
            }
        }
        public async Task RefreshCustomerAsync()
        {
            Model = await App.Repository.GetCustomer(Model.Id);
        }
    }
}