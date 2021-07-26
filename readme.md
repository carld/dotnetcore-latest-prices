# 

https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-5.0&tabs=visual-studio


https://docs.microsoft.com/en-us/aspnet/core/web-api/?view=aspnetcore-5.0

The objective is to build a web api that solves
the delayed prices problem I have previously encountered.

## Method



    dotnet add package Microsoft.EntityFrameworkCore.Sqlite
    dotnet add package Microsoft.EntityFrameworkCore.SqlServer
    dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
    dotnet add package Microsoft.EntityFrameworkCore.Design

    dotnet tool install -g dotnet-aspnet-codegenerator

Create a controller using the code generator, note that no -m is provided because of the keyless model.

    dotnet aspnet-codegenerator controller -name MarketController -async -api -dc MarketContext -outDir Controllers


    dotnet tool install --global dotnet-ef
    dotnet add package Microsoft.EntityFrameworkCore.Design

To update packages:

    dotnet tool update --global dotnet-ef 

To create migrations:

    dotnet ef migrations add InitialCreate
    dotnet ef database update


To remove all migrations:

    dotnet ef database update 0
    dotnet ef migrations remove



### Queries


    SELECT p1.id, 
       p1.ticker, 
       p1.published_at ,
	   p1.price_in_cents
       FROM prices p1
    left join
        prices p2
            on p1.ticker = p2.ticker
            and p1.published_at < p2.published_at
    where 
        p2.id is null;

