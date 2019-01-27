using GymSystem.App.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
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
    public sealed partial class ViewDetails : Page
    {
        public CustomerViewModel ViewModel { get; set; } = new CustomerViewModel();
        public ViewDetails()
        {
            this.InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Disabled;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter == null)
            {
                ViewModel = new CustomerViewModel
                {
                    IsNewCustomer = true,
                    IsInEdit = false,
                };
            }
            else
            {
                int id = (int)e.Parameter;

                App.Repository.GetCustomer(id);

                for (int i = 0; i < App.ViewModel.Customers.Count; i++)
                {
                    if (id == App.ViewModel.Customers[i].Model.Id)
                    {
                        ViewModel = new CustomerViewModel()
                        {
                            IsNewCustomer = false,
                            IsInEdit = false,
                            Model = App.Repository.GetCustomer(id),
                        };

                    }

                }
            }

            ViewModel.AddNewCustomerCanceled += AddNewCustomerCanceled;
            base.OnNavigatedTo(e);
        }
        private void AddNewCustomerCanceled(object sender, EventArgs e) => Frame.GoBack();
    }

}
