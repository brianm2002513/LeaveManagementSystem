# Enterprise Leave Management System 🏢

A production-ready Enterprise Leave Management System built using **ASP.NET Core 9**, following an **N-Tier (Clean Architecture)** design pattern and fully covered by an **xUnit / InMemory EF Core Test Suite**.

## 🧠 Engineering Highlights
This project was specifically constructed to demonstrate high-level backend engineering principles:

1.  **N-Tier Architecture**: The application is cleanly separated into `Application`, `Data`, `Common`, and `Web` layers. This separation of concerns ensures that business logic is strictly decoupled from presentation logic.
2.  **Dependency Injection**: Extensively uses ASP.NET Core's built-in DI container to inject Services, DbContexts, and Mappers.
3.  **AutoMapper**: Utilizes AutoMapper to elegantly convert database entities (`LeaveType`) into safe Data Transfer Objects / ViewModels (`LeaveTypeReadOnlyViewModel`).
4.  **Test-Driven Architecture (xUnit & Moq)**: The core business logic (`LeaveTypesService`) is validated by a robust `xUnit` test suite.
5.  **EF Core In-Memory Testing**: Instead of mocking raw `DbSets` (which is prone to false positives), the test suite spins up a real `Microsoft.EntityFrameworkCore.InMemory` database for each test, ensuring that LINQ queries and `DbContext` operations work flawlessly without needing a live SQL Server.

## 🛠 Tech Stack
*   **Framework**: ASP.NET Core 9 MVC
*   **ORM**: Entity Framework Core
*   **Database**: SQL Server (LocalDB) / EF InMemory (Testing)
*   **Testing**: xUnit, Moq, Shouldly
*   **Mapping**: AutoMapper

## 🚀 How to Run

### Run the Application
Open the solution in Visual Studio or run via CLI:
```bash
dotnet run --project LeaveManagementSystem.Web
```

### Run the Unit Tests
To execute the business logic tests and verify the EF Core queries:
```bash
dotnet test
```

## 🧪 Test Suite Features
The `LeaveManagementSystem.Application.Tests` project demonstrates:
*   Mocking built-in ASP.NET dependencies (`ILogger<T>`) using `Moq`.
*   Configuring custom `AutoMapper` profiles inside the test constructor.
*   Asserting business rules (e.g., `DaysExceedMaximum` bounds checking) using the `Shouldly` fluent assertion library.

---
*Created by Brian Munashe Mbawa as a demonstration of C# / .NET Enterprise Architecture.*
