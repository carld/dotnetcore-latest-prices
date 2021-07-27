# Groupwise Maximum Web API in DotNet Core

| Tool | Version |
|------|---------|
| Visual Studio Code | 1.58.1 |
| dotnet cli | .NET SDK (5.0.202) |
| macOS Catalina | 10.15.7 |
| SQLite | 3.28.0 |

https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-5.0&tabs=visual-studio

https://docs.microsoft.com/en-us/aspnet/core/web-api/?view=aspnetcore-5.0

https://dev.mysql.com/doc/refman/8.0/en/example-maximum-column-group-row.html

The objective is to build a web api that provides market price data, using a groupwise maximum query to determine the latest price for each instrument.

## Method

A new webapi project was created on the command line interface (CLI) with:

    dotnet new webapi -o latest-prices



    dotnet add package Microsoft.EntityFrameworkCore.Sqlite
    dotnet add package Microsoft.EntityFrameworkCore.SqlServer
    dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
    dotnet add package Microsoft.EntityFrameworkCore.Design

    dotnet tool install -g dotnet-aspnet-codegenerator

Create a controller using the code generator, note that no `-m` is provided because of the keyless model.

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

To drop the database:

    dotnet ef database drop

To create the database:

    dotnet ef database update

### Queries

The SQLite database `prices` table was set up with 100,000 rows.

Timings were measured from the SQLite command line:

    sqlite> .timer on

#### Groupwise maximum using a left join 

The latest prices are retrieved from a prices table using a "groupwise maximum" query:

    SELECT p1.id, 
       p1.ticker, 
       p1.published_at ,
	   p1.price_in_cents
    FROM prices p1
    LEFT JOIN
        prices p2
            ON p1.ticker = p2.ticker
            AND p1.published_at < p2.published_at
    WHERE 
        p2.id IS NULL;

> Run Time: real 180.889 user 175.904504 sys 0.719221

Adding indexes to the columns used in the left join, `ticker`, and `published_at`, did not noticeably improve performance.

#### Groupwise maximum using an uncorelated subquery

    SELECT p1.id, 
       p1.ticker, 
       p1.published_at,
	   p1.price_in_cents
    FROM prices p1
    JOIN
    ( SELECT id, ticker, MAX(published_at) FROM prices GROUP BY ticker)
    AS p2
    ON p1.id = p2.id

> Run Time: real 0.054 user 0.052637 sys 0.000744

Note that to be able to join to the subquery a unique id is necessary. Having the `id` column in the `prices` table made this join possible. Using the `published_at` column to join on is a possibility, although there may be duplicates returned when there is more than one price for a ticker published at the same time.

