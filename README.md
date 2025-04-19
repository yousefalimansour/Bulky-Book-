# 📚 Bulky Book

Bulky Book is an ASP.NET Core MVC web application for managing a bookstore. It includes features like product management, category classification, order processing, and user authentication.

## 🛠️ Technologies Used

- ASP.NET Core MVC
- Entity Framework Core
- SQL Server
- Razor Pages
- Bootstrap
- jQuery
- ASP.NET Identity

## 🚀 Getting Started

### Prerequisites

- Visual Studio 2022 or later
- .NET 6 SDK or later
- SQL Server

### Installation

1. **Clone the repository:**

   ```bash
   git clone https://github.com/yousefalimansour/Bulky-Book-.git
   ```

2. **Navigate to the project directory:**

   ```bash
   cd Bulky-Book-
   ```

3. **Configure the database:**

   - Open `appsettings.json`
   - Update the `DefaultConnection` string with your SQL Server credentials

4. **Apply migrations and update the database:**

   Open the Package Manager Console and run:

   ```bash
   Update-Database
   ```

5. **Run the application:**

   ```bash
   dotnet run
   ```

   Then open `https://localhost:5001` in your browser.

## 📁 Project Structure

- `BulkyBook.DataAccess` - Database context and EF Core migrations
- `BulkyBook.Models` - Entity models
- `BulkyBook.Utility` - Helper classes and constants
- `BulkyBookWeb` - MVC web application

## 🧑‍💻 Features

- Admin Panel to manage products, categories, and orders
- User Registration and Role-based Login
- Shopping Cart functionality
- Order and Payment Processing

## 📸 Screenshots

*Add screenshots here if needed*

## 🤝 Contributing

Feel free to fork the repo and submit a pull request for any improvements or bug fixes.

## 📄 License

This project is licensed under the [MIT License](LICENSE).
