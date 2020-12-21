using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DomainServices;
using EF.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace UI.ASMApp
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
            services.AddDbContext<AnimalShelterDbContext>(options => options.UseSqlServer("server = animalshelter-yi.database.windows.net; database = animalshelter-yi; User ID=yisun2001;Password=P#GbY#P44E9^;Trusted_Connection=False;Encrypt=True; MultipleActiveResultSets=True"));
            services.AddDbContext<IEFDbContext>(options => options.UseSqlServer("server = animalshelter-yi.database.windows.net; database = Identity; User ID=yisun2001;Password=P#GbY#P44E9^;Trusted_Connection=False;Encrypt=True; MultipleActiveResultSets=True"));

            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<IEFDbContext>().AddDefaultTokenProviders();

            services.AddAuthorization(options => {
                options.AddPolicy("Volunteer", policy => policy.RequireClaim("VolunteerType"));
            });

            services.AddControllersWithViews();
            services.AddScoped<IAnimalRepository, AnimalRepository>();
            services.AddScoped<IApplicationRepository, ApplicationRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ITreatmentRepository, TreatmentRepository>();
            services.AddScoped<IResidenceRepository, ResidenceRepository>();
            services.AddScoped<IVolunteerRepository, VolunteerRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
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
            });
        }
    }
}
