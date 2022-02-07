using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShoppingCore.DataAccess;
using ShoppingCore.DataAccess.Repositories.Implementations;
using ShoppingCore.DataAccess.Repositories.Interfaces;
using ShoppingCore.Model;
using ShoppingCore.Utilities;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCore
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
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddIdentity<IdentityUser,IdentityRole>().AddDefaultTokenProviders()
                .AddEntityFrameworkStores<AppDbContext>();
            services.AddSingleton<IEmailSender, EmailSender>();
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
                Configuration.GetConnectionString("DefaultConnection")));
            services.Configure<StripeSettings>(Configuration.GetSection("Stripe"));
            services.AddHttpContextAccessor();
            services.AddControllersWithViews();
            services.AddRazorPages().AddRazorRuntimeCompilation();
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/Identity/Account/Login";
                options.LogoutPath = $"/Identity/Account/Login";
                options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
            });
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(100);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
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
            StripeConfiguration.ApiKey = Configuration.GetSection("Stripe:SecretKey").Get<string>();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.UseEndpoints(
                endpoints =>
                {
                    endpoints.MapRazorPages();
                });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
