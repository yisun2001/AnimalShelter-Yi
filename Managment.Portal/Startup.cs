using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Managment.Core.DomainServices;
using Managment.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Managment.Portal
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
            
            services.AddDbContext<AnimalDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("Default")));

            services.AddDbContext<SecurityDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("Identity")));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<SecurityDbContext>().AddDefaultTokenProviders();

            services.AddAuthorization(options =>
            options.AddPolicy("VolunteerOnly", policy => policy.RequireClaim("Volunteer")) );

            services.AddScoped<IAnimalRepository, AnimalRepository>();
            services.AddScoped<IAnimalServices, AnimalServices>();
            services.AddScoped<INoteRepository, NoteRepository>();
            services.AddScoped<ITreatmentRepository, TreatmentRepository>();
            services.AddScoped<ITreatmentServices, TreatmentServices>();
            services.AddScoped<IResidenceRepository, ResidenceRepository>();
            services.AddScoped<IVolunteerRepository, VolunteerRepository>();
            services.AddScoped<IInterestRepository, InterestRepository>();
            services.AddScoped<IAnimalForAdoptionRepository, AnimalForAdoptionRepository>();
            services.AddControllersWithViews();
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
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "animal",
                    pattern: "{controller=Animal}/{action=Overview}/{id?}");

            });
        }
    }
}
