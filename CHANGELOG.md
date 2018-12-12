## [1.0.0] - 2018-11-12
### Added
- views:
* MainPage.xaml
* ClientList.xaml
* AddEntranceToCustomer.xaml
* ViewDetails.xaml
* EntranceList.xaml
* AppStyles.xaml

## [1.0.1] - 2018-12-4
### Added
- models:
* Customer.cs
* Entrance.cs
* EntranceType.cs
* IAdmin.cs
* Admin.cs

-viewmodel:
* BindableBase.cs

## [1.0.2] - 2018-12-12
### Added
* ClassDiagram2.cd

-viewmodel:
* Converters.cs
* CustomerViewModel.cs
* MainViewModel.cs

* Database using Entity Framework Core (Code First)
* function of removing and adding a user 

### Removed
 * AddClientViewModel.cs
 
### Moved
* Admin.cs,  IAdmin.cs- to repository (Facade)
