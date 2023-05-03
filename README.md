# dotNet5782_2845_5562

# Customer and Drone Management System Demo
This demo project is a simple application written in C# and XAML, designed to showcase a customer and drone management system. The application allows the user to create, edit and delete parcels and also lets the manager manage the drones and clients.

## Features
Add new customers to the system
Edit existing customer information
Delete customers from the system
Add new drones to the system
Edit existing drone information
Delete drones from the system
View a list of all customers in the system
View a list of all drones in the system
View a list of all drones assigned to a particular customer
## Installation
To run the application, follow these steps:

* Download or clone the project from GitHub.
* Open the solution file in Visual Studio.
* Build the project.
* Run the application.
If you want to log into the system administrator, type username 00

## Usage
When you run the application, you will see a simple user interface with several buttons and fields for inputting customer and drone information.

To enter as a new customer, click the "Register" button and enter the customer's information in the fields provided.

As an administrator you can add drones, package clients and drone charging stations
To add a new drone, click the "Drones" button and enter the drone's information in the fields provided. Click "Add drone" and enter the appropriate details

To edit an existing customer or drone, select the customer or drone from the appropriate list and click the "Edit" button. Edit the information in the fields provided and click "Save" to update the customer or drone in the system.

To delete a customer or drone, select the customer or drone from the appropriate list and click the "Delete" button. Confirm that you want to delete the customer or drone.
* note: you can only delete Free drone 

To view a list of all customers or drones in the system, click the "Customers" or "Drones" button, respectively. To view a list of all drones assigned to a particular customer, select the customer from the "Customers" list and click the "Assigned Drones" button.

## Project Architecture
The project is written in C# using the Windows Presentation Foundation (WPF) framework and XAML for the user interface. It follows the Model-View-ViewModel (MVVM) design pattern, which separates the application into three layers:

* Model: This layer represents the data and business logic of the application. It includes classes to represent customers and drones, as well as a data service to manage the data. Called BL - logistic layer

* View: This layer represents the user interface of the application. It includes XAML files to define the layout and appearance of the application.

* ViewModel: This layer acts as a mediator between the Model and View layers. It includes classes to manage the data and business logic, as well as commands to handle user input.

The project also includes more design templates like Singleton and Simple Factory and more

## Customer and Drone Models
The Customer and Drone models are simple classes that represent a customer or drone in the system. They include properties for the customer or drone's name, email address, and ID.

The Customer model also includes a list of drones assigned to the customer.

## Data Service
The data service is responsible for managing the data in the application. It includes methods to add, edit, and delete customers and drones, as well as methods to retrieve lists of all customers or drones in the system.

The data service is implemented using a simple in-memory database. Data is stored in a list of Customer and Drone objects, which are manipulated using LINQ queries.

## User Interface
The user interface includes several screens to manage customers and drones:

1. Customer List: This screen displays a list of all customers in the system. Clicking on a customer opens the Customer Details screen.

2. Customer Details: This screen displays the details of a selected customer, including their name, email address, and ID. It also includes a list of all drones assigned to the customer. From this screen, you can edit the customer's information or assign a new drone to the customer.

3. Drone List: This screen displays a list of all drones in the system. Clicking on a drone opens the Drone Details screen.

4. Drone Details: This screen displays the details of a selected drone, including its name, email address, and ID. From this screen, you can edit the drone's information or delete the drone.

5. Add Customer: This screen allows you to add a new customer to the system. It includes fields to enter the customer's name and email address.

6. Add Drone: This screen allows you to add a new drone to the system. It includes fields to enter the drone's name and email address, as well as a dropdown to select the customer the drone is assigned to.

# Conclusion
This demo project provides a simple example of how to manage customers and drones in C# and XAML using the MVVM design pattern and move. It includes a basic data service, unit tests, and a user interface to manage customers and drones. You can use this project as a starting point for building more complex applications that require data management functionality.

Credits
This demo project was created by Ariel Moreshet and Yair Babaov. If you have any questions or feedback, please contact me at arialaamm@gmail.com - Ariel, yair.babayov@gmail.com - Yair.
