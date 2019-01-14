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

        private void CreateClient_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ViewDetails), null, new DrillInNavigationTransitionInfo());
        }

        private void ViewDetails_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.SelectedType != null)
            {
                Frame.Navigate(typeof(ViewDetails), ViewModel.SelectedType.Model.Id,
                    new DrillInNavigationTransitionInfo());
            }
        }

        private void AddEntrance_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddEntranceToCustomer), null, new DrillInNavigationTransitionInfo());
        }


        private async void DeleteEntrance_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var deleteOrder = ViewModel.SelectedType;
                await ViewModel.DeleteEnType(deleteOrder);
            }
            catch (Exception ex)
            {
                var dialog = new ContentDialog()
                {
                    Title = "Unable to delete entrance type",
                    Content = $"There was an error when we tried to delete \n{ex.Message}",
                    PrimaryButtonText = "OK"
                };
                await dialog.ShowAsync();
            }
        }

    }
}