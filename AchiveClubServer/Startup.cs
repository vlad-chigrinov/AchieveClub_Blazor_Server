using System.IO;

using AchieveClubServer.Data;
using AchieveClubServer.Services;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.FileProviders;

using Blazored.LocalStorage;
using Tewr.Blazor.FileReader;
using MudBlazor.Services;

namespace AchieveClubServer
{
    public class Startup
    {
        private IConfiguration _configuration;
        private IWebHostEnvironment _env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            (_configuration, _env) = (configuration, env);
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddFileReaderService();

            services.AddBlazoredLocalStorage();
            services.AddMudServices();

            string connection = _configuration.GetConnectionString("DefaultConnection");

            services.AddSingleton<string>(_ => connection);
            services.AddSingleton<ImageLoader>();
            services.AddSingleton<HashService>();

            services.AddTransient<ISupervisorRepository, SupervisorRepository>();
            services.AddTransient<IAdminRepository, AdminRepository>();
            services.AddTransient<IAchieveRepository, AchieveRepository>();
            services.AddTransient<IClubRepository, ClubRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ICompletedAchieveRepository, CompletedAchieveRepository>();
            services.AddTransient<IMedalRepository, MedalRepository>();
            services.AddTransient<IUserMedalRepository, UsersMedalRepository>();

            services.AddTransient<AdminLoginDataProvider>();
            services.AddScoped<AdminLoginService>();

            services.AddTransient<LoginLocalStorage>();
            services.AddTransient<UserLoginQuery>();
            services.AddScoped<UserLoginService>();
            services.AddScoped<UniqEmailQuery>();
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
            services.AddTransient<ChangeUserPasswordService>();
            services.AddTransient<AvatarChanger>();
            services.AddTransient<UserMedalsService>();

            services.AddTransient<UserCounter>();
            services.AddTransient<AchieveCompleteCounter>();
            services.AddTransient<AchieveCompleteRatioCounter>();

            services.AddTransient<ClubRatingMedals>();
            services.AddTransient<TotalRatingMedals>();
            services.AddTransient<MedalService>();

            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddSingleton<UserRatingStorage>();
            services.AddSingleton<ClubRatingStorage>();

            services.AddHostedService<RatingTimerUpdater>();
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
                endpoints.MapControllers();
                
            });
        }
    }
}