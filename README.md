# DT071G - Programming in C# .NET

The basics of programming with C# .NET.

In this course, I explored programming with C#, covering everything from fundamental OOP principles to more advanced design patterns like Dependency Injection. I then applied this knowledge by developing various applications using C# and .NET (5+). I built console applications with .NET 5 in a Linux environment, a mobile application using Xamarin.Forms, and an API with ASP.NET Core. Additionally, I worked with databases, utilizing SQLite, SQL Server, and EF Core for data management.

## Development environment
- _VS Code_ with the following extensions:
  - _ms-dotnettools.csharp_
  - _jchannon.csharpextensions_
- _.NET 6.0 SDK_
- _ASP.NET Core Web API_
- _EF Core_
- _SQL Server_
- _SQL Server Management Studio (SSMS)_

## Setup
- Configure EF Core and add a database connection:
  - Create a database connection using SSMS.
  - Add the database connection string to _appsettings.json_ or in a user-secret:
  ```
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog={db_name}; User ID={db_username}; Password={db_password};"
  }
  ```
  - Install EF tools: _dotnet tool install --global dotnet-ef_
  - Run the EF Core migrations: _dotnet ef database update_
- Add a signing key to _appsettings.json_ or in a user-secret:
```
"TokenSettings": {
  "SigningKey": "{your_secure_signing_key_with_atleast_16_characters}"
}
```
- Run the application: _dotnet run_
- Test the application with Swagger: _https://localhost:7177/swagger/index.html_

## Demo and essay
- [Video demo](https://www.youtube.com/watch?v=-3N8dx2N0Cc)
- [Essay](https://github.com/albinronnkvist/Course_DT071G_ProgrammingInCSharpDotNET/blob/master/DT071G_essay_sv.pdf)
