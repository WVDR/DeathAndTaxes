# DeathAndTaxes
A small full-stack solution to do tax calculations using .NET Core using MVC Razor and doing some basic CRUD operations on SQL Server (provided is a local DB Export).
README
A small full-stack solution to do tax calculations using .NET Core using MVC Razor and doing some basic CRUD operations on SQL Server (provided is a local DB Export).

Features:

• Latest .NET Core version (.net 7)

• demonstrates how progressive tax works

o Adheres to the SOLID principals

o Unit tests

o Avoids scaffolding where possible

o Clean well-formatted code

Brief:

The FE is a tax calculator for an individual.

The application will take annual income and postal code.

![image](https://github.com/WVDR/DeathAndTaxes/assets/10729245/e129434b-b57a-49d0-b98c-0137234bdfe6)


The flat value:

• 10000 per year

• Else if the individual earns less than 200000 per year the tax will be at 5%


The flat rate:

• All users pay 17.5% tax on their income

Approach Taken:

• Tax calculator written using TDD

• SOLID principals

•Testing framework is Nunit using the Core framework

• Razor frontend

• EF core with code first done.

• Design Patterns used are (TODO)

• IOC/Dependency Injection

• Allows for the entering of the Postal code and annual income on the front end and submitting the values

• The calculated value is stored in SQL Server with date/time and the two fields entered

• Security (TODO)

• Server side is Web API/REST APIs using swagger
