using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private Entrance _SelectedEntrance;

        public Entrance SelectedEntrance
        {
            get { return _SelectedEntrance; }
            set { _SelectedEntrance = value; OnPropertyChanged(); OnPropertyChanged("SelectedEntranceLog"); }
        }
        public List<EntranceLog> SelectedEntranceLog { get => SelectedEntrance?.EntranceLog?.ToList(); }

        private ObservableCollection<Entrance> _entrance;
        public ObservableCollection<Entrance> EntranceList
        {
            get
            {
                if (_entrance == null)
                {
                    _entrance = new ObservableCollection<Entrance>(Model?.Entrance);
                }
                return _entrance;
            }
            set => Set(ref _entrance, value);
        }
        private ObservableCollection<EntranceType> _EntranceTypes;

        public ObservableCollection<EntranceType> EntranceTypes
        {
            get
            {
                if (_EntranceTypes == null)
                {
                    _EntranceTypes = new ObservableCollection<EntranceType>(App.Repository.GetAllEntranceTypes());
                }
                return _EntranceTypes;
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
                    if (value.All(char.IsDigit) || (value.ToCharArray()[0] == "+".ToCharArray()[0] && value.Substring(1).All(char.IsDigit)))
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
        //TODO: Walidacja pól nie może być oparta o ich długość, jeśli nie opuścimy kursora to LostFocus się nie wykona i po kliknięciu zapisz dane pole w którym jest kursor 
        // się nie zapisze
        // trzeba zmienić w każdym z pól edytowalnych tryb na OnPropertyChanged, i nie sprawdzać ilości znaków,
           // bo będą się wtedy dodawać pojedyńczo, lub validować dopiero przy próbie zapisu
        public string City
        {
            get => Model.Adress.City;
            set
            {
                if (value != Model.Adress.City && value.Length > 2)
                {
                    if (!value.Any(char.IsDigit))
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
                if (value != Model.Adress.Code && value.Length > 0)
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
            IsInEdit = true;
        }

        public void CancelEdit()
        {
            IsInEdit = false;
            IsModified = false;
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