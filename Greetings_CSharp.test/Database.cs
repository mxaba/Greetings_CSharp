using System;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using DatabaseFeatures;
using Greetings_CSharp.Database;

namespace Greetings_CSharp.test
{
    public class DatabaseFixture : IDisposable
    {
        public DatabaseFixture()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            TestDatabase = new TestDatabaseBuilder().WithConfiguration(configuration).WithTemplateDatabase("uuid_extension_enabled").Build();
            TestDatabase.Create();

            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseNpgsql(TestDatabase.ConnectionString);
            DbContext = new ApplicationDbContext(builder.Options);
            DbContext.Database.EnsureCreated();
        }


        public ITestDatabase TestDatabase { get; }

        public ApplicationDbContext DbContext { get; }

        public void Dispose()
        {
            TestDatabase.Drop();
        }
    }

    [CollectionDefinition("Database")]
    public class DatabaseCollectionFixture : ICollectionFixture<DatabaseFixture>
    {
    }
}