# Greeting MVC CSharp

## (PostgreSQL) Database Setup
-   
    ### Packages to be installed
    - Make sure that they all much the version of .net core you are running 
        ``` Microsoft.EntityFrameworkCore ```
        ```  Microsoft.EntityFrameworkCore.Tools```
        ``` Microsoft.EntityFrameworkCore.Design ```
        ``` Npgsql.EntityFrameworkCore.PostgreSQL ```

    ### Creating a model
    - Create a model class with all the fileds/properties you need (N:B the fields/properties will be your columns)
        > if you have a field to make a primary key upi can use the keywaord [key] on top of that field
        - Example of the model
            ```C#
                namespace Greetings_CSharp.Models {
                    public class Greetings
                    {
                        public int Id { get; set; }
                        public string Name { get; set; }
                        public int Counts { get; set; }
                    }
                }
            ```

    ### Create the instance of Entity Frame Work
    - In your root project create a folder that has a name related to database (GreetingDatabase, GreatingData, ...)        
        - Add a new class file call it a name related to the cintext (GreetingContext, DataContext, ...)
            - Inorder for you to make the class a DbConext it has to inherint from the DbContext
                ```C#
                    namespace Greetings.GreetingsDatabase
                    {
                        public class ApplicationDbContext : DbContext
                        {
                        }
                    }
                ```
                - Packages you will need are :
                    - You will need it for DbContext
                        > EntityFrameworkCore
                    - You will need it for Sql Server and EntityFrameworkCore need a database to work with
                        > EntityFrameworkCore.SqlServer
            - Once the packages are installed go back to your database creating
                ```C#
                    using Microsoft.EntityFrameworkCore;

                    namespace Greetings.GreetingsDatabase<...>
                ```
        ##### Database configuration
        - So with that we have added > EntityFrameworkCore, now we need to do database configuration
            - Create a constructor
                - Since we are imlimenting dbcontext we need to pass the db context in that constructor and the base class
                ```C#
                    namespace Greetings.Database
                    {
                        public class GreetingsDatabase : DbContext
                        {
                            public ApplicationDbContext(GreetingsDatabase<GreetingsDatabase> options) : base(options)
                            {
                            }
                        }
                    }
                ```
        ##### Create table (Database)
        - Now we have the DbContext class, so we have to create the table for our model
            - Create a get and set property for the model and call dbset and call the entity you want to create
                ```C#
                    namespace Greetings.Database
                    {
                        public class GreetingsDatabase : DbContext
                        {
                            public ApplicationDbContext(GreetingsDatabase<GreetingsDatabase> options) : base(options)
                            {
                            }
                            public DbSet<Model> Greetings { get; set; }
                        }
                    }
                ```
        
    ### Defining ConnectionString
    - 
        - Go to your project root and open the
            > appsettings.json
        - Add  a ConnectionString inside the json it should look like this 👇🏾
            ```JSON
                {
                    "ConnectionStrings": {
                        "DefaultConnection": "Server=127.0.0.1;Database=myDataBase;Port:5432;User Id=myUsername; Password=myPassword;;"
                    }
                }
            ```
        - The next step is to use this connection string to connect to the database
        ##### Configure DbContext in startup file
        - Indide the start up file you have services that'w where you need to configure
            - Add a service for DbContext
                ```C#
                    services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseNpgsql(
                        Configuration.GetConnectionString("DefaultConnection")));
                ```
                > `ApplicationDbContext` - is the DBContext class you created for databse
                > `UseSqlServer` - just need the connection string
                > '`Configuration`.GetConnectionString("DefaultConnection")' This is where you get the connection string you created in `appsettings`.json
        
        ##### Now we need to do migrations
        - Migration will push our models to the database but before we need to install <b>`EF Migration Tool`</b>
            - Open the terminal and type
                ``` dotnet tool install --global dotnet-ef ```

            - Open the terminal and <b>Make sure thzt you are in the root folder of the project</b>
                - Run the migration the database
                    ``` dotnet ef migrations AddingModelEntityToDatabase ```
                    - It should create a folder in your root folder called <b>Migrations</b>
                - Update the database  using this command
                ``` dotnet ef database update ```

        > <b>It should create the database without any errors, Always remember CODE FIRST</b>   

## Deploying to heroku using build packs (No Dockerfile)
- The Buildpack supports C# and F# projects. It searchs through the repository's folders to locate a `Startup.*` or `Program.*` file. If found, the `.csproj` or `.fsproj` in the containing folder will be used in the `dotnet publish <project>` command.
    - `Initialize the repo with Heroku and commit everything on your root folder`
    - ``` heroku buildpacks:set jincod/dotnetcore ```
    ### Entity Framework Core Migrations
    - You cannot run migrations with the `dotnet ef` commands using **.NET Local Tools** once the app is built.
        ### Enabling Automatic Migrations
        - `Still yet to come I couldn't figure it out...`
        
        ### Manually Running Migration Scripts on the Database
        -  Manually run SQL scripts generated by Entity Framework Core in your app's database. Heroku Postgres service.
            - Open your terminal and type (root project dolder)
                `heroku config:get DATABASE_URL --app {Name Of Your Project}`
            - Replace your connection string with that URL but make sure that you separate the `Host`, `User Id`, `Database`, `Password`
        - Run your Migration comands
    - You are goo to deploy
