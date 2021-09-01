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

            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql("Host=ec2-18-214-238-28.compute-1.amazonaws.com;Database=d5etmdfu8k1n23;Port=5432;User Id=vztsnbyxsnkopv;Password=fb36d0e1d5ac82b96468cb9c2e60f47fba10efb2ed3ffda4ea23e7b85794adb2;SSL Mode=Require;Trust Server Certificate=True;"));
        }
    }
}