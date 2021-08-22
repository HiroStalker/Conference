using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Backend.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

/*
    CodeFirst:
    dotnet tool install -g dotnet-ef --version 3.1.3
    dotnet build
    dotnet ef migrations add Initial
    dotnet ef database update

    Add-Migration Refactor
    Update-Database

*/
namespace Backend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddDbContext<ApplicationDBContext>(options =>
                {
                    // if(RuntimeInformation .IsOSPlatform(OSPlatform.Windows)){
                    //     options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                    // }else{
                    //The problem here is that we need the nuget for the Sqlite

                    //"dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 2.2.1"
                    options.UseSqlite("Data Source=conferences.db");
                    // }
                });

            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services
                .AddSwaggerGen(Option =>
                    Option
                        .SwaggerDoc("v1",
                        new Swashbuckle.AspNetCore.Swagger.Info
                        {
                            Title = "Conference API",
                            Version = "v1"
                        }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSwagger();

            app
                .UseSwaggerUI(options =>
                    options
                        .SwaggerEndpoint("/swagger/v1/swagger.json",
                        "Conference API v1"));

            app.UseHttpsRedirection();
            app.UseMvc();

            app
                .Run(Context =>
                {
                    Context.Response.Redirect("/swagger");
                    return Task.CompletedTask;
                });
        }
    }
}
