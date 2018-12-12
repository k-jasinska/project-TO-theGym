using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

//Szablon elementu Pusta strona jest udokumentowany na stronie https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x415

namespace GymSystem.App
{
    /// <summary>
    /// Pusta strona, która może być używana samodzielnie lub do której można nawigować wewnątrz ramki.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }
        public Frame AppFrame => frame;

        private void OnNavigatingToPage(object sender, NavigatingCancelEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Back)
            {
                if (e.SourcePageType == typeof(ClientList))
                {
                    navView.SelectedItem = CustomerListMenuItem;
                }
                else if (e.SourcePageType == typeof(EntranceList))
                {
                    navView.SelectedItem = EntranceListMenuItem;
                }
                
            }
        }

        public readonly string ListaKlientow = "Customer list";
        public readonly string ListaKarnetow = "Entrance list";
 

        private void NavView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            if (AppFrame.CanGoBack)
            {
                AppFrame.GoBack();
            }
        }
        private void NavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {

  
            var label = args.InvokedItem as string;
            var pageType =
                args.IsSettingsInvoked ? typeof(ClientList) :
                label == ListaKlientow ? typeof(ClientList) :
                label == ListaKarnetow ? typeof(EntranceList) : null;
            if (pageType != null && pageType != frame.CurrentSourcePageType)
            {
                AppFrame.Navigate(pageType);
            }
        }
    }
}
