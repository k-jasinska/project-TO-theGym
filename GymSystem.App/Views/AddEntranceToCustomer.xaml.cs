using GymSystem.App.Models;
using GymSystem.App.ViewModels;
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
using Windows.UI.Xaml.Navigation;

//Szablon elementu Pusta strona jest udokumentowany na stronie https://go.microsoft.com/fwlink/?LinkId=234238

namespace GymSystem.App
{
    /// <summary>
    /// Pusta strona, która może być używana samodzielnie lub do której można nawigować wewnątrz ramki.
    /// </summary>
    public sealed partial class AddEntranceToCustomer : Page
    {
       public EntranceViewModel ViewModel { get; set; } = new EntranceViewModel();

      
        public AddEntranceToCustomer()
        {
            this.InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Disabled;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter == null)
            {
                ViewModel = new EntranceViewModel
                {
                    IsNewEntrance = true,
                    IsInEdit = true
                };
            }
            else
            {
               
            }

            ViewModel.AddEntranceCanceled += AddEntranceCanceled;
            base.OnNavigatedTo(e);
        }
        public void AddEntranceCanceled(object sender, EventArgs e) => Frame.GoBack();
    }
}
