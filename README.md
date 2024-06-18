# Project Name
 ### BasicWebApi
 <br>

## Introduction
This project provides a set of simple Web APIs for managing company information, country data, and contact details. You can interact with these APIs to perform CRUD (Create, Read, Update, Delete) operations related to companies, countries, and contacts.

## Technologies Used
- .NET Core
- MySQL Database
- Entity Framework Core (Code First)
- Swagger (for API documentation)
- ILogger
- Dependency Injection (DI)
- Fluent Validations
- Unit testing with xUnit


### 1. MySQL Database
 The application utilizes a MySQL database to store and manage data. MySQL is a popular open-source relational database management system (RDBMS) known for its performance, scalability, and reliability.

 
 Configure the connection string in the application settings to point to your MySQL database.<br>

### 2. Code-First and Automatic Migrations
The project follows a code-first approach, where the database schema is generated based on the application’s domain models. Automatic migrations ensure that the database structure is synchronized with the code.


As you develop your models, the database schema will be automatically updated when you run migrations.<br>

### 3. Onion Architecture
The application architecture follows the Onion Architecture pattern. This design organizes the codebase into concentric layers (Core, Domain, Persistance, Web) to promote separation of concerns and maintainability.<br>

### 4. Swagger for API UI
Swagger (OpenAPI) is integrated into the project to provide an interactive API documentation interface. It allows developers to explore and test API endpoints directly from the browser.


Usage: Access the Swagger UI by navigating to the appropriate URL (usually /swagger) after running the application.<br>

### 5. Logging and Error Handling
The application includes logging mechanisms to capture relevant information during runtime. Error handling ensures graceful handling of exceptions and provides meaningful feedback to users

### 6. Dependency Injection (DI) Pattern
Dependency injection is used to manage component dependencies and promote loose coupling. It allows you to inject services and dependencies into classes rather than hardcoding them.


Usage: Register services in the DI container (e.g., in Program.cs) and inject them into controllers, services, and other components.

### 7. Fluent Validations
Fluent Validation is employed to validate input data and ensure data integrity. It provides a fluent API for defining validation rules.


Usage: Create validation rules for your models and apply them during request processing.

### 8. Lambda Expressions
Lambda expressions (anonymous functions) are used for concise and expressive code. They allow you to write inline functions without explicitly defining a method.

### 9. xUnit Tests
The project includes xUnit tests to verify the correctness of the code. xUnit is a popular testing framework for .NET.
