# Project
Simple EFCore + MySQL project to practice code-first approach and linq with C# and .NET 9.

# Approach
We are using a code-first approach, with a simple services class instead of using the repository pattern to keep it simple.

# Steps
- [x] - Design logical entity model (`draw.io`)
    - Relationships:
        - Categories - Books (One-To-Many)
        - Books - Authors (Many-To-Many)
- [x] - Create entity models
- [x] - Create a database in mysql and a user with permissions
- [x] - Install required packages
    - `MySql.EntityFrameworkCore` (MySql library for EFCore)
    - `Microsoft.EntityFrameworkCore.Design --version 9.*` (Migrations) (Major version must match our .NET 9 version)
- [x] - Setup model classes
- [x] - Setup `DbContext` derived class with `DbSet` propertjies.
    - [x] - Configure connection string
    - [x] - Configure model constraints
    - [x] - Configure relationships
    - [ ] - Configure seeds
- [ ] - Setup migrations
    - [ ] - Update database through migrations
- [ ] - Setup services
    - [ ] - Setup basic CRUD operations
- [ ] - Setup some fancy LINQ methods

# ERD
![erd image](./ERD/Diagram.drawio.png)

# About
[Yosmel Chiang](https://github.com/yosang)
