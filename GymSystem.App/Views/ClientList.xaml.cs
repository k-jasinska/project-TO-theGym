using GymSystem.App.ViewModels;
using Microsoft.Toolkit.Uwp.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

//Szablon elementu Pusta strona jest udokumentowany na stronie https://go.microsoft.com/fwlink/?LinkId=234238

namespace GymSystem.App
{
    /// <summary>
    /// Pusta strona, która może być używana samodzielnie lub do której można nawigować wewnątrz ramki.
    /// </summary>
    public sealed partial class ClientList : Page
    {
        public ClientList()
        {
            this.InitializeComponent();
        }
        public MainViewModel ViewModel => App.ViewModel;

        private void CreateClient_Click(object sender, RoutedEventArgs e)
        {
           Frame.Navigate(typeof(ViewDetails), null, new DrillInNavigationTransitionInfo());
        }

        private void ViewDetails_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.SelectedCustomer != null)
            {
                Frame.Navigate(typeof(ViewDetails), ViewModel.SelectedCustomer.Model.Id,
                    new DrillInNavigationTransitionInfo());
            }
        }

        private void AddEntrance_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddEntranceToCustomer), ViewModel.SelectedCustomer.Model, new DrillInNavigationTransitionInfo());
        }


        private async void DeletePerson_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var deleteOrder = ViewModel.SelectedCustomer;
                await ViewModel.DeletePerson(deleteOrder);
            }
            catch (Exception ex)
            {
                var dialog = new ContentDialog()
                {
                    Title = "Unable to delete person",
                    Content = $"There was an error when we tried to delete \n{ex.Message}",
                    PrimaryButtonText = "OK"
                };
                await dialog.ShowAsync();
            }
        }

        private void CustomerSearchBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (CustomerSearchBox != null)
            {
                CustomerSearchBox.AutoSuggestBox.QuerySubmitted += CustomerSearchBox_QuerySubmitted;
                CustomerSearchBox.AutoSuggestBox.TextChanged += CustomerSearchBox_TextChanged;
                CustomerSearchBox.AutoSuggestBox.PlaceholderText = "Search customers...";
            }
        }
        private async void CustomerSearchBox_TextChanged(AutoSuggestBox sender,
         AutoSuggestBoxTextChangedEventArgs args)
        {
            // We only want to get results when it was a user typing,
            // otherwise we assume the value got filled in by TextMemberPath
            // or the handler for SuggestionChosen.
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                // If no search query is entered, refresh the complete list.
                if (String.IsNullOrEmpty(sender.Text))
                {
                    //await DispatcherHelper.ExecuteOnUIThreadAsync(async () =>
                    //    await ViewModel.GetCustomerListAsync());
                    //sender.ItemsSource = null;
                }
                else
                {
                    string[] parameters = sender.Text.Split(new char[] { ' ' },
                        StringSplitOptions.RemoveEmptyEntries);
                    //sender.ItemsSource = ViewModel.Customers
                    //    .Where(customer => parameters.Any(parameter =>
                    //        customer.Address.StartsWith(parameter, StringComparison.OrdinalIgnoreCase) ||
                    //        customer.FirstName.StartsWith(parameter, StringComparison.OrdinalIgnoreCase) ||
                    //        customer.LastName.StartsWith(parameter, StringComparison.OrdinalIgnoreCase) ||
                    //        customer.Company.StartsWith(parameter, StringComparison.OrdinalIgnoreCase)))
                    //    .OrderByDescending(customer => parameters.Count(parameter =>
                    //        customer.Address.StartsWith(parameter, StringComparison.OrdinalIgnoreCase) ||
                    //        customer.FirstName.StartsWith(parameter, StringComparison.OrdinalIgnoreCase) ||
                    //        customer.LastName.StartsWith(parameter, StringComparison.OrdinalIgnoreCase) ||
                    //        customer.Company.StartsWith(parameter, StringComparison.OrdinalIgnoreCase)))
                    //    .Select(customer => $"{customer.FirstName} {customer.LastName}");
                }
            }
        }
        private async void CustomerSearchBox_QuerySubmitted(AutoSuggestBox sender,
         AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (String.IsNullOrEmpty(args.QueryText))
            {
                //await DispatcherHelper.ExecuteOnUIThreadAsync(async () =>
                //    await ViewModel.GetCustomerListAsync());
            }
            else
            {
                string[] parameters = sender.Text.Split(new char[] { ' ' },
                    StringSplitOptions.RemoveEmptyEntries);

                //var matches = ViewModel.Customers.Where(customer => parameters
                //    .Any(parameter =>
                //        customer.Address.StartsWith(parameter, StringComparison.OrdinalIgnoreCase) ||
                //        customer.FirstName.StartsWith(parameter, StringComparison.OrdinalIgnoreCase) ||
                //        customer.LastName.StartsWith(parameter, StringComparison.OrdinalIgnoreCase) ||
                //        customer.Company.StartsWith(parameter, StringComparison.OrdinalIgnoreCase)))
                //    .OrderByDescending(customer => parameters.Count(parameter =>
                //        customer.Address.StartsWith(parameter, StringComparison.OrdinalIgnoreCase) ||
                //        customer.FirstName.StartsWith(parameter, StringComparison.OrdinalIgnoreCase) ||
                //        customer.LastName.StartsWith(parameter, StringComparison.OrdinalIgnoreCase) ||
                //        customer.Company.StartsWith(parameter, StringComparison.OrdinalIgnoreCase)))
                //    .ToList();

                //await DispatcherHelper.ExecuteOnUIThreadAsync(() =>
                //{
                //    ViewModel.Customers.Clear();
                //    foreach (var match in matches)
                //    {
                //        ViewModel.Customers.Add(match);
                //    }
                //});
            }
        }

      
    }
}
