using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using GymSystem.App.ViewModels;
namespace GymSystem.App
{
    /// <summary>
    /// Pusta strona, która może być używana samodzielnie lub do której można nawigować wewnątrz ramki.
    /// </summary>
    public sealed partial class ViewSettings : Page
    {
        public ViewSettings()
        {
            this.InitializeComponent();
        }
        public SettingsViewModel ViewModel { get; set; } = new SettingsViewModel();

        private async void AddTicketType_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.AddTicketType();
            await ViewModel.GetEnTypesListAsync();
        }

        private async void DeleteTicketType_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ViewModel.SelectedType != null)
                {
                    var deleteOrder = ViewModel.SelectedType;
                    await ViewModel.DeleteEnType(deleteOrder);
                }
                else
                {
                    var dialog = new ContentDialog()
                    {
                        Title = "No one ticket type selected",
                        Content = "Firstly select ticket type, then click delete again.",
                        PrimaryButtonText = "OK"
                    };
                    await dialog.ShowAsync();
                }
            }
            catch (Exception ex)
            {
                var dialog = new ContentDialog()
                {
                    Title = "Unable to delete ticket type",
                    Content = $"There was an error when we tried to delete \n{ex.Message}",
                    PrimaryButtonText = "OK"
                };
                await dialog.ShowAsync();
            }
            await ViewModel.GetEnTypesListAsync();
        }
    }
}