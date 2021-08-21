# Greeting MVC CSharp

## (SQL SErver) Database Setup
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

### Installing SQL Server 2019 on Mac OS
#### Docker
- The first step is to install Docker. Docker is a platform that enables software to run in its own isolated environment. Therefore, SQL Server 2019 can be run on Docker in its own isolated container.
#### Launch Docker
- Launch Docker the same way you’d launch any other application
    > When you open Docker, you might be prompted for your password so that Docker can install its networking components and links to the Docker apps. Go ahead and provide your password, as Docker needs this to run.
#### Increase the Memory (optional)
- By default, Docker will have 2GB of memory allocated to it. I’d suggest increasing it to 4GB if you can.
#### SQL Server
> Now that Docker has been installed and configured, we can download and install SQL Server 2019.
- 
    ##### Download SQL Server 2019
    - Open a Terminal window and run the following command.
    > sudo docker pull mcr.microsoft.com/mssql/server:2019-latest
    - This downloads the latest SQL Server for Linux Docker image to your computer.
    ##### Launch the Docker Image
    - Run the following command to launch an instance of the Docker image you just downloaded:
    > sudo docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=reallyStrongPwd#123" -p 1433:1433 --name Codex -d mcr.microsoft.com/mssql/server:2019-latest
    - Just change Codex to a name of your choosing, and reallyStrongPwd#123 to a password of your choosing.
    - If you get a “port already allocated” error, see below.
        - Here’s an explanation of the parameters:
            > -e 'ACCEPT_EULA=Y'
            - The Y shows that you agree with the EULA (End User Licence Agreement). This is required.
            > -e 'SA_PASSWORD=reallyStrongPwd#123'
            - Required parameter that sets the sa database password.
            > -p 1433:1433
            - This maps the local port 1433 to port 1433 on the container. The first value is the TCP port on the host environment. The second value is the TCP port in the container.
            > --name Codex
            - Another optional parameter. This parameter allows you to name the container. This can be handy when stopping and starting your container from the Terminal. You might prefer to give it a more descriptive name like sql_server_2019 or similar.
            > -d
            - This optional parameter launches the Docker container in daemon mode. This means that it runs in the background and doesn’t need its own Terminal window open. You can omit this parameter to have the container run in its own Terminal window.
            > mcr.microsoft.com/mssql/server:2019-latest
            - This tells Docker which image to use.
    ##### Password Strength
    - You need to use a strong password. Microsoft says this about the password:
    > The password should follow the SQL Server default password policy, otherwise the container can not setup SQL server and will stop working. By default, the password must be at least 8 characters long and contain characters from three of the following four sets: Uppercase letters, Lowercase letters, Base 10 digits, and Symbols.
    ##### Error – “Port already allocated”?
    - If you get an error that says something about “port is already allocated”, then perhaps you already have SQL Server installed on another container that uses that port. In this case, you’ll need to map to a different port on the host.
    - Therefore, you could change the above command to something like this:
    > sudo docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=reallyStrongPwd#123" -p 1400:1433 --name Codex -d mcr.microsoft.com/mssql/server:2019-latest
    - In this case I simply changed -p 1433:1433 to -p 1400:1433. Everything else remains the same.
    - You may now get an error saying that you need to remove the existing container first. To do that, run the following (but swap Codex with the name of your own container):
        > sudo docker rm Bart
    - Once removed, you can try running the previous command again.
#### Check Everything
- 
    ##### Check the Docker container (optional)
    - You can type the following command to check that the Docker container is running.
    > udo docker ps -a
    - This tells me that I have two docker containers up and running
    ##### Connect to SQL Server
    - Here we use the SQL Server command line tool called “sqlcmd” inside the container to connect to SQL Server.
    > sudo docker exec -it CodeX "bash"
    - Enter your password if prompted.
    > /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "reallyStrongPwd#123"
    - This should bring you to the sqlcmd prompt 1>.
    ##### Run a Quick Test
    - Run a quick test to check that SQL Server is up and running. For example, check the SQL Server version by entering this:
        > SELECT @@version
    - This will bring you to a command prompt 2> on the next line. To execute the query, enter:
        > GO
    - Result:
        > +--------------------+
          | (No column name)   |
          |--------------------|
          | Microsoft SQL Server 2019 (RTM-CU3) (KB4538853) - 15.0.4023.6 (X64) Mar  4 2020 00:59:26 
            Copyright (C) 2019 Microsoft Corporation
            Developer Edition (64-bit) on Linux (Ubuntu 18.04.4 LTS)                     |
           +--------------------+
           (1 row affected)
        - If you see a message like this, congratulations — SQL Server is now up and running on your Mac!

### Defining ConnectionString
- Once that you have installing docker and SQL Server is running:
    - Go to your project root and open the
        > appsettings.json
    - Add  a ConnectionString inside the json it should look like this 👇🏾
        ```JSON
            {
                "ConnectionStrings": {
                    "DefaultConnection": "Server=myServerAddress;Database=myDataBase;User Id=myUsername; Password=myPassword;Initial Catalog = myDataBase;"
                    }
            }
        ```
    - The next step is to use this connection string to connect to the database
        ##### Creating the database
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
        ##### Configure DbContext in startup file
        - Indide the start up file you have services that'w where you need to configure
            - Add a service for DbContext
                ```C#
                    services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(
                        Configuration.GetConnectionString("DefaultConnection")));
                ```
                > ApplicationDbContext - is the DBContext class you created for databse
                > UseSqlServer - just need the connection string
                > 'Configuration.GetConnectionString("DefaultConnection")' This is where you get the connection string you created in appsettings.json
    ##### Now we need to do migrations
    - Migration will push our models to the database
    - Open the terminal and add migration and give it a meaningful name
        > dotnet ef migrations AddingModelEntityToDatabase
        - if you get an error you need to install
            > EntityFrameworkCore.tools - it's needed for database migration and update
        - Then run the command one more time
            > dotnet ef migrations add AddingModelEntityToDatabase
        - Update the database  using this command
            > dotnet ef database update
        > It should create the database without any errors, It you encount any errors let me know                    
- You are good to go!!!
