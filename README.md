# **Restaurant Management System**

The **Restaurant Management System** is a simple web application built with **Angular** for the frontend and **.NET Core** for the backend. It provides an online reservation system for restaurant tables, allowing users to book tables based on seating capacity, date, and time.

## **Implemented Features**

- **Table Management**: Displays a list of tables along with their locations and seating capacities, with filtering options.
- **Reservation Management**: Allows users to book tables for a specific number of people at a given date and time.
- **Code-First Approach**: Uses Entity Framework Core's code-first approach for creating the database and seeding initial data.
- **Unit Tests**: Includes comprehensive unit tests to validate business rules and key functionalities.

## **Setup and Configuration**
### Backend Setup

1. **Install the required dependencies**:
   - Install the **.NET SDK**, **Entity Framework Core**, and the latest **Node modules**.

2. **Clone the repository**:
   ```bash
   git clone https://github.com/ThakkarKhushbu/RestaurantManagementSystem.git
 **API Code**
- Navigate to the project directory: cd Backend\RestaurantManagementSystem
- Configure the database connection in the appsettings.json file.
- Run database migrations to create the necessary tables: dotnet ef database update, In case you would like to setup database directly via script then Navigate to folder - DbScript amd execute that script.
- Build the project: dotnet build
- Run the application: dotnet run
### Front End Setup
- Use 22.0 for Node.js version
- Do required setup for Angular 19 project 
- Navigate to the project directory: cd Frontend\restaurant-management
- Run Npm i : It will install node modules based on package.json file configuration.
- Run Npm Start

**Demo**

