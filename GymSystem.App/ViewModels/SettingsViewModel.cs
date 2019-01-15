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
        // Creates a new SettingsViewModel.
        public SettingsViewModel() => Task.Run(GetEnTypesListAsync);

        // The collection of EntranceTypes in the list. 
        public ObservableCollection<EntranceTypeViewModel> EntranceTypes { get; }
            = new ObservableCollection<EntranceTypeViewModel>();

        private EntranceTypeViewModel _selectedEnType;

        // Gets or sets the selected type, or null if no customer is selected. 
        public EntranceTypeViewModel SelectedType
        {
            get => _selectedEnType;
            set => Set(ref _selectedEnType, value);
        }

        private bool _isLoading = false;

        // Gets or sets a value indicating whether the EntranceType list is currently being updated. 
        public bool IsLoading
        {
            get => _isLoading;
            set => Set(ref _isLoading, value);
        }

        // Gets the complete list of EntranceType from the database.
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

        //Adds new, empty Entrance (Ticket) type to the database
        internal void AddTicketType()
        {
            EntranceType et = new EntranceType
            {
                Duration = 0,
                Name = "",
                Price = 0
            };
            App.Repository.AddEntranceType(et);
        }

        //Removes Entrance Type from the database
        internal async Task DeleteEnType(EntranceTypeViewModel deleteOrder)
        {
            await App.Repository.DeleteEntranceType(deleteOrder.Model);
        }
        
    }
}



