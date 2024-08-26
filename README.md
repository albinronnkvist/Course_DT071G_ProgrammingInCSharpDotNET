# DT071G - Programming in C# .NET

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

## Demo and documentation
- Video demo: https://www.youtube.com/watch?v=-3N8dx2N0Cc
- [Documentation](https://firebasestorage.googleapis.com/v0/b/myportfolio-4e23b.appspot.com/o/essays%2FDT071G_essay_sv.pdf?alt=media&token=772e0e6f-d96f-401a-b514-65a4ff338328)
