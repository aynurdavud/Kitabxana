using KitabxanaS.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KitabxanaS
{
    public class Startup {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

   


    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllersWithViews();
            //services.AddIdentity<AppUser, IdentityRole>(IdentityOption =>
            //{
            //    IdentityOption.Password.RequireUppercase = true;
            //    IdentityOption.Password.RequireLowercase = true;
            //    IdentityOption.Password.RequireNonAlphanumeric = false;
            //    IdentityOption.User.RequireUniqueEmail = true;
            //    IdentityOption.Lockout.AllowedForNewUsers = true;
            //    IdentityOption.Lockout.MaxFailedAccessAttempts = 5;
            //    IdentityOption.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
            //    IdentityOption.SignIn.RequireConfirmedEmail = false;
            //    IdentityOption.SignIn.RequireConfirmedPhoneNumber = false;

            //}).AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();
            services.AddDbContext<AppDbContext>(option =>
            {
                option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));

            });

        }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();


        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
              );
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

        });
    }
}
}
