using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using GymSystem.App.ViewModels;
using Microsoft.Toolkit.Uwp.Helpers;
using System.Linq;

//Szablon elementu Pusta strona jest udokumentowany na stronie https://go.microsoft.com/fwlink/?LinkId=234238

namespace GymSystem.App
{
    /// <summary>
    /// Pusta strona, która może być używana samodzielnie lub do której można nawigować wewnątrz ramki.
    /// </summary>
    public sealed partial class EntranceList : Page
    {
        public EntranceList()
        {
            this.InitializeComponent();
        }
        public EntranceListViewModel ViewModel { get; set; } = new EntranceListViewModel();

        private async void DeleteEntrance_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var deleteOrder = ViewModel.SelectedEntrance;
                await ViewModel.DeleteEntrance(deleteOrder);
            }
            catch (Exception ex)
            {
                var dialog = new ContentDialog()
                {
                    Title = "Unable to delete entrance",
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
        private async void CustomerSearchBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {

            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                if (String.IsNullOrEmpty(sender.Text))
                {
                    await DispatcherHelper.ExecuteOnUIThreadAsync(async () =>
                          await ViewModel.GetEntranceListAsync());
                }
                else
                {
                    string[] parameters = sender.Text.Split(new char[] { ' ' },
                        StringSplitOptions.RemoveEmptyEntries);

                    sender.ItemsSource = ViewModel.Entrances.Where(customer => parameters
                        .Any(parameter =>
                            customer.Id.ToString().StartsWith(parameter, StringComparison.OrdinalIgnoreCase) ||
                            customer.Person.Surname.StartsWith(parameter, StringComparison.OrdinalIgnoreCase) ||
                            customer.Person.Name.StartsWith(parameter, StringComparison.OrdinalIgnoreCase))
                            )
                        .Select(customer => $"{customer.Id}: {customer.Person.Name} {customer.Person.Surname}");
                }
            }
        }
        private async void CustomerSearchBox_QuerySubmitted(AutoSuggestBox sender,
         AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            try
            {
                if (String.IsNullOrEmpty(args.QueryText))
                {
                    await DispatcherHelper.ExecuteOnUIThreadAsync(async () =>
                        await ViewModel.GetEntranceListAsync());
                }
                else
                {
                    string[] parameters = sender.Text.Split(new char[] { ' ' },
                        StringSplitOptions.RemoveEmptyEntries);

                    var matches = ViewModel.Entrances.Where(customer => parameters
                        .Any(parameter =>
                            customer.Id.ToString().StartsWith(parameter, StringComparison.OrdinalIgnoreCase) ||
                            customer.Person.Surname.StartsWith(parameter, StringComparison.OrdinalIgnoreCase) ||
                            customer.Person.Name.StartsWith(parameter, StringComparison.OrdinalIgnoreCase))
                            )

                        .ToList();

                    await DispatcherHelper.ExecuteOnUIThreadAsync(() =>
                    {
                        ViewModel.Entrances.Clear();
                        foreach (var match in matches)
                        {
                            ViewModel.Entrances.Add(match);
                        }
                    });
                }
            }
            catch (Exception ex )
            {

                var dialog = new ContentDialog()
                {
                    Title = "Error",
                    Content = $"There was an error during db connection \n{ex.Message}",
                    PrimaryButtonText = "OK"
                };
                await dialog.ShowAsync();
            }
        }

        private async void AddLog_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var entrance = ViewModel.SelectedEntrance;
                if (entrance?.IsValidEntrance??false)
                {
                    await ViewModel.AddLog(entrance);
                    var dialog = new ContentDialog()
                    {
                        Title = "OK",
                        Content = $"Entrance added",
                        PrimaryButtonText = "OK"
                    };
                    await dialog.ShowAsync();
                }
                else
                {
                    var dialog = new ContentDialog()
                    {
                        Title = "You shall no pass",
                        Content = $"Selected entrance is invalid",
                        PrimaryButtonText = "OK"
                    };
                    await dialog.ShowAsync();
                }
            }
            catch (Exception ex)
            {
                var dialog = new ContentDialog()
                {
                    Title = "Unable to add entrance",
                    Content = $"Can nod add entrance",
                    PrimaryButtonText = "OK"
                };
                await dialog.ShowAsync();
            }
        }
    }
}
