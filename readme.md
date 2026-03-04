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
- [ ] - Create a database in mysql and a user with permissions
- [ ] - Install required packages
    - `MySql.EntityFrameworkCore` (MySql library for EFCore)
    - `Microsoft.EntityFrameworkCore.Design --version 9.*` (Migrations) (Major version must match our .NET 9 version)
- [ ] - Setup model classes
- [ ] - Setup `DbContext` derived class with `DbSet` propertjies.
    - [ ] - Configure connection string
    - [ ] - Configure seeds, model constraints and relationships

# ERD
![erd image](./ERD/Diagram.drawio.png)

# About
[Yosmel Chiang](https://github.com/yosang)
