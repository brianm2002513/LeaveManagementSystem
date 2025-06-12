# Employee Leave Management System

## Overview
This project is the main application developed as part of Trevor Williams' **Complete ASP.NET Core and Entity Framework Development** course. It is an Employee Leave Management System built using ASP.NET Core MVC and Entity Framework Core, featuring user authentication, role-based access control, and a SQL Server database backend.

## ✨ Features
- User registration and login with ASP.NET Core Identity
- Role-based authorization (Admin, Employee)
- CRUD operations for leave requests and leave types
- Leave approval workflow for managers
- Responsive UI with Bootstrap
- Data validation and error handling
- Deployment-ready for Azure or local IIS

## 🛠️ Technologies Used
- ASP.NET Core MVC
- Entity Framework Core (Code First)
- SQL Server
- ASP.NET Core Identity
- Bootstrap 5
- AutoMapper
- Dependency Injection

## Getting Started

### Prerequisites
- [.NET 7 SDK](https://dotnet.microsoft.com/en-us/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server)
- IDE such as [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)

### 🛠️ Installation Instructions

1. Clone the repository:
```bash
git clone https://github.com/brianm2002513/LeaveManagementSystem.git
cd LeaveManagementSystem
```

2. Update the connection string in `appsettings.json` to point to your SQL Server instance:
```bash
"ConnectionStrings": {
"DefaultConnection": "Server=YOUR_SERVER_NAME;Database=LeaveManagementSystemDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

3. Apply migrations and seed the database:
```bash
dotnet ef database update
```

4. Run the application:
```bash
dotnet run
```
5. Open your browser and navigate to `https://localhost:7179` (or the URL shown in the console).

## Usage
- Register a new user or use seeded admin credentials.
- Default Admin Credentials:
    - Username: admin@localhost.com
    - Password: Admin@123
- Create, view, and manage leave requests.
- Admin users can approve or reject leave requests.

## 📄 License
This project is licensed under the MIT License.

## 🙌 Contact
For questions or feedback, please contact me at munashebrian19@gmail.com.

---



