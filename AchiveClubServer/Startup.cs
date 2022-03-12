using AchiveClubServer.Data;
using AchiveClubServer.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Blazored.Modal;
using Blazored.LocalStorage;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace AchiveClubServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddBlazoredLocalStorage();
            services.AddBlazoredModal();

            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddScoped<string>(_ => connection);

            services.AddSingleton<ImageLoader>();
            services.AddSingleton<HashService>();

            services.AddTransient<ISupervisorRepository, SupervisorRepository>();
            services.AddTransient<IAdminRepository, AdminRepository>();
            services.AddTransient<IAchieveRepository, AchieveRepository>();
            services.AddTransient<IClubRepository, ClubRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ICompletedAchieveRepository, CompletedAchieveRepository>();

            services.AddTransient<AdminLoginDataProvider>();
            services.AddScoped<AdminLoginService>();

            services.AddTransient<LoginLocalStorageService>();
            services.AddTransient<UserLoginDataProvider>();
            services.AddScoped<UserLoginService>();
            services.AddScoped<UniqEmailProvider>();
            services.AddTransient<RegistrationService>();

            services.AddTransient<ClubNamesService>();
            services.AddTransient<ClubPageModelBuilder>();
            services.AddTransient<ClubRatingService>();
            services.AddTransient<ClubScoreService>();
            services.AddTransient<ClubUsersService>();

            services.AddTransient<CompleteAchieveService>();
            services.AddTransient<UserAchievementsService>();
            services.AddTransient<UserPageModelBuilder>();
            services.AddTransient<UserRatingService>();
            services.AddTransient<UserScoreService>();

            services.AddRazorPages();
            services.AddServerSideBlazor();
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(env.ContentRootPath, "../StaticFiles")),
                RequestPath = "/StaticFiles"
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}