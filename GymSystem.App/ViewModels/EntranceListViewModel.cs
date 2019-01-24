﻿using Microsoft.Toolkit.Uwp.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystem.App.ViewModels
{
    public class EntranceListViewModel : BindableBase
    {
        // Creates a new EntranceViewModel.
        public EntranceListViewModel() => Task.Run(GetEntranceListAsync);

        // The collection of entrances displayed on the list. 
        public ObservableCollection<EntranceViewModel> Entrances { get; }
            = new ObservableCollection<EntranceViewModel>();

        private EntranceViewModel _selectedEntrance;

        // Gets or sets the selected entrance, or null if no customer is selected. 
        public EntranceViewModel SelectedEntrance
        {
            get => _selectedEntrance;
            set => Set(ref _selectedEntrance, value);
        }

        private bool _isLoading = false;

        // Gets or sets a value indicating whether the Entrance list is currently being updated. 
        public bool IsLoading
        {
            get => _isLoading;
            set => Set(ref _isLoading, value);
        }

        // Gets the complete list of entrance from the database.
        public async Task GetEntranceListAsync()
        {
            await DispatcherHelper.ExecuteOnUIThreadAsync(() => IsLoading = true);

            var localEntrances = App.Repository.GetAllEntrances();
            if (localEntrances == null)
            {
                return;
            }

            await DispatcherHelper.ExecuteOnUIThreadAsync(() =>
            {
                Entrances.Clear();
                foreach (var envm in localEntrances)
                {
                    Entrances.Add(new EntranceViewModel(envm));
                }
                IsLoading = false;
            });
        }

        internal async Task DeleteEntrance(EntranceViewModel deleteOrder)
        {
            await App.Repository.DeleteEntrance(deleteOrder.Model);
            Sync();
        }

        /// <summary>
        /// Saves any modified customers and reloads the customer list from the database.
        /// </summary>
        public void Sync()
        {
            Task.Run(async () =>
            {
                IsLoading = true;
                foreach (var modifiedEntrance in Entrances
                    .Where(en => en.IsModified).Select(en => en.Model))
                {
                    //   await App.Repository.Customers.UpsertAsync(modifiedCustomer);
                }

                await GetEntranceListAsync();
                IsLoading = false;
            });
        }
    }
}



