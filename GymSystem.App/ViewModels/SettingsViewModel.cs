using Microsoft.Toolkit.Uwp.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymSystem.App.ViewModels;
using GymSystem.Db;

namespace GymSystem.App.ViewModels
{


    /// <summary>
    /// Provides data and commands accessible to the entire app.  
    /// </summary>
    public class SettingsViewModel : BindableBase
    {
        public class EntranceTypeViewModel
        {
            private EntranceType _model;
            private string _name;
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
                }
            }
            public int Duration
            {
                get => Model.Duration;
                set
                {
                    Model.Duration = value;
                }
            }
            public decimal Price
            {
                get => Model.Price;
                set
                {
                    Model.Price = value;
                }
            }
            public string Name
            {
                get => Model.Name;
                set
                {
                    Model.Name = value;
                }
            }
        }
        /// <summary>
        /// Creates a new MainViewModel.
        /// </summary>
        public SettingsViewModel() => Task.Run(GetEnTypesListAsync);

        /// <summary>
        /// The collection of customers in the list. 
        /// </summary>
        public ObservableCollection<EntranceTypeViewModel> EntranceTypes { get; }
            = new ObservableCollection<EntranceTypeViewModel>();

        private EntranceTypeViewModel _selectedEnType;

        /// <summary>
        /// Gets or sets the selected customer, or null if no customer is selected. 
        /// </summary>
        public EntranceTypeViewModel SelectedType
        {
            get => _selectedEnType;
            set => Set(ref _selectedEnType, value);
        }

        private bool _isLoading = false;

        /// <summary>
        /// Gets or sets a value indicating whether the Customers list is currently being updated. 
        /// </summary>
        public bool IsLoading
        {
            get => _isLoading;
            set => Set(ref _isLoading, value);
        }

        /// <summary>
        /// Gets the complete list of customers from the database.
        /// </summary>
        public async Task GetEnTypesListAsync()
        {
            await DispatcherHelper.ExecuteOnUIThreadAsync(() => IsLoading = true);

            var types = App.Repository.GetAllEntranceTypes();
            if (types == null)
            {
                return;
            }

            await DispatcherHelper.ExecuteOnUIThreadAsync(() =>
            {
                EntranceTypes.Clear();
                foreach (var c in types)
                {
                    EntranceTypes.Add(new EntranceTypeViewModel(c));
                }
                IsLoading = false;
            });
        }

        internal async Task DeleteEnType(EntranceTypeViewModel deleteOrder)
        {
            await App.Repository.DeleteEntranceType(deleteOrder.Model);
        }
    }
}



