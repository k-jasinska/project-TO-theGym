using System;
using System.ComponentModel;
using System.Linq;
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
                    if (DataTester.IsValidSurname(value))
                    {
                        Model.Name = value.ToCharArray()[0].ToString().ToUpper() + value.Substring(1); //Capital letter
                        IsModified = true;
                        OnPropertyChanged();
                    }
                    else
                    {
                        ClientList.InvalidDataDialog("Incorrect name", value + " is not a name.");
                    }
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
                    if (DataTester.IsValidSurname(value))
                    {
                        Model.Surname = value.ToCharArray()[0].ToString().ToUpper() + value.Substring(1); //Capital letter
                        IsModified = true;
                        OnPropertyChanged();
                    }
                    else
                    {
                        ClientList.InvalidDataDialog("Incorrect surname", value + " is not a surname.");
                    }
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
                    //Phone number can contain only digits or digits preceded by the "+"
                    if (value.Length > 3 && (value.Substring(1).Any(char.IsDigit) || (value.ToCharArray()[0] == "+".ToCharArray()[0] && value.Substring(0, 1).Any(char.IsDigit))))
                    {
                        Model.Phone = value;
                        IsModified = true;
                        OnPropertyChanged();
                    }
                    else
                    {
                        ClientList.InvalidDataDialog("Incorrect phone number.", value + " is not a phone number.");
                    }
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
                    if (DataTester.IsValidEmail(value)) //Check value using Regex
                    {
                        Model.Mail = value;
                        IsModified = true;
                        OnPropertyChanged();
                    }
                    else
                    {
                        ClientList.InvalidDataDialog("Incorrect email address.", value + " is not an email address.");
                    }
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
                    if (value.Length > 2 || !value.Any(char.IsDigit))
                    {
                        Model.Adress.City = value.ToCharArray()[0].ToString().ToUpper() + value.Substring(1); //Capital letter
                        IsModified = true;
                        OnPropertyChanged();
                    }
                    else
                    {
                        ClientList.InvalidDataDialog("Incorrect city", "City name can not contain any numbers.");
                    }
                }
            }
        }

        public string Code
        {
            get => Model.Adress.Code;
            set
            {
                if (value != Model.Adress.Code && value.Length>0)
                {
                    if (value.ToCharArray()[0] >= '0' && value.ToCharArray()[0] <= '9')
                    {
                        Model.Adress.Code = value;
                        IsModified = true;
                        OnPropertyChanged();
                    }
                    else
                    {
                        ClientList.InvalidDataDialog("Incorrect postal code", value + " is not a postal code.");
                    }
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
                    if (value.Split(" ").Length > 1) //This variable must consist of street name and building number
                    {
                        Model.Adress.Street = value;
                        IsModified = true;
                        OnPropertyChanged();
                    }
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
            Model = App.Repository.GetCustomer(Model.Id);
        }
    }
}