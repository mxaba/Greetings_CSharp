using Greetings_CSharp.Database;
using Greetings_CSharp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Greetings_CSharp.test
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ICreateReadUpdateDelete, CreateReadUpdateDelete>();

            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql("Server=localhost;Database=greetings;Port=5432;User Id=postgresgreet;Password=postgres;"));
        }
    }
}