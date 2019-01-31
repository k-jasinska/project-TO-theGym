# project: The Gym

## Authors

* Karolina Jasińska
* Sebastian Jankowski

A desktop application for the gym staff allowing gym management, written in C#.

## Features:
* intuitive GUI for inexperienced gym staff
* adding, editing, deleting, searching, sorting customer's data
* adding new ticket
* extending the validity of tickets with price calculating 
* fast checking ticket validity at the entrance 
* possibility to add several passes
* counting day visitors

## Differences between MVC and MVVM:

In both patterns the Model represents a collection of classes that explains the business logic and the View represents the user interface components (XAML in this project).

The controller take the user's input and figure out what to do with it.

In MVVM the controller is partly replaced with a ViewModel. The ViewModel provides a set of interfaces, each of which represents a UI component in the View. We use a technique called “binding” to connect UI components to ViewModel interfaces. We write presentational things such as converting Date to String in the ViewModel instead of the View.

In MVC the view knows about the controller, the controller knows the model. In MVVM the view does not need to know about any other layer in the architecture.

## Database structure:
![sequence diagram](https://github.com/k-jasinska/project-TO-theGym/blob/master/docs/structure%20of%20database.JPG)

## Class diagram:
![sequence diagram](https://github.com/k-jasinska/project-TO-theGym/blob/master/docs/ClassDiagram.png)

## Sequence diagrams:
Browsing customer list
![sequence diagram](https://raw.githubusercontent.com/k-jasinska/project-TO-theGym/master/docs/browsing%20Customer%20List.jpg)

Browsing entrance list
![sequence diagram](https://raw.githubusercontent.com/k-jasinska/project-TO-theGym/master/docs/browsing%20Entrance%20List.jpg)

Edit customer details
![sequence diagram](https://raw.githubusercontent.com/k-jasinska/project-TO-theGym/master/docs/edit%20customer.jpg)

## Use cases:
![use case diagram](https://raw.githubusercontent.com/k-jasinska/project-TO-theGym/master/docs/use%20cases.jpg)

## Design patterns used in the application:

Memento- is a pattern that provides the ability to restore an object to its previous state. The memento pattern is implemented with three objects: the originator, a caretaker and a memento. Memento contains state of an object to be restored. Originator creates and stores states in Memento objects and Caretaker object is responsible to restore object state from Memento. IEditableObject (behavioral pattern)

Facade- a facade is an object that serves as a front-facing interface masking more complex underlying or structural code. This pattern provides a simpler interface to the client, improve the readability and usability. iAdmin (structural pattern)

Observer- maintains a list of its dependents, called observers, and notifies them automatically of any state changes, usually by calling one of their methods. The observer pattern is also a key part in the MVVM architectural pattern. INotifyPropertyChanged (behavioral pattern)
