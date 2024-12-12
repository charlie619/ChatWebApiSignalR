In order to add sql lite db, please execute the following commands:

###################################################################################
Remove migration folder first.
Make sure the Microsoft.EntityFrameworkCore.Tools package is added to the project.
###################################################################################

1. dotnet ef migrations add InitialCreate
2. dotnet ef database update
