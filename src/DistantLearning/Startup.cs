using System;
using System.Net;
using System.Threading.Tasks;
using DataAccessProvider;
using DistantLearning.Services;
using Domain;
using Domain.Model;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DistantLearning
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddJsonFile("config.json", true, true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
                builder.AddUserSecrets();

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var sqlConnectionString = Configuration.GetConnectionString("DistantLearningDb");

            services.AddDbContext<DomainModelContext>(options =>
                    options.UseSqlServer(
                        sqlConnectionString,
                        b => b.MigrationsAssembly("DistantLearning")
                    )
            );

            services.AddIdentity<User, IdentityRole>(options =>
                {
                    options.Cookies.ApplicationCookie.Events = new CookieAuthenticationEvents
                    {
                        OnRedirectToLogin = ctx =>
                        {
                            if (ctx.Request.Path.StartsWithSegments("/api") &&
                                (ctx.Response.StatusCode == (int) HttpStatusCode.OK))
                                ctx.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
                            else
                                ctx.Response.Redirect(ctx.RedirectUri);
                            return Task.FromResult(0);
                        }
                    };
                })
                .AddEntityFrameworkStores<DomainModelContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IDataAccessProvider<Answer>, DataAccessProvider<Answer>>();
            services.AddScoped<IDataAccessProvider<ChildParent>, DataAccessProvider<ChildParent>>();
            services.AddScoped<IDataAccessProvider<Comment>, DataAccessProvider<Comment>>();
            services.AddScoped<IDataAccessProvider<Consultation>, DataAccessProvider<Consultation>>();
            services.AddScoped<IDataAccessProvider<Discipline>, DataAccessProvider<Discipline>>();
            services.AddScoped<IDataAccessProvider<Document>, DataAccessProvider<Document>>();
            services.AddScoped<IDataAccessProvider<Group>, DataAccessProvider<Group>>();
            services.AddScoped<IDataAccessProvider<Journal>, DataAccessProvider<Journal>>();
            services.AddScoped<IDataAccessProvider<JournalDiscipline>, DataAccessProvider<JournalDiscipline>>();
            services.AddScoped<IDataAccessProvider<Mark>, DataAccessProvider<Mark>>();
            services.AddScoped<IDataAccessProvider<Quarter>, DataAccessProvider<Quarter>>();
            services.AddScoped<IDataAccessProvider<Question>, DataAccessProvider<Question>>();
            services.AddScoped<IDataAccessProvider<TeacherDiscipline>, DataAccessProvider<TeacherDiscipline>>();
            services.AddScoped<IDataAccessProvider<Test>, DataAccessProvider<Test>>();
            services.AddScoped<IDataAccessProvider<TestResult>, DataAccessProvider<TestResult>>();
            services.AddScoped<IDataAccessProvider<User>, DataAccessProvider<User>>();
            services.AddScoped<IDataAccessProvider<UserMark>, DataAccessProvider<UserMark>>();
            services.AddScoped<IDataAccessProvider<UserParent>, DataAccessProvider<UserParent>>();
            services.AddScoped<IDataAccessProvider<UserSetting>, DataAccessProvider<UserSetting>>();
            services.AddScoped<IDataAccessProvider<UserStudent>, DataAccessProvider<UserStudent>>();
            services.AddScoped<IDataAccessProvider<UserTeacher>, DataAccessProvider<UserTeacher>>();

            services.AddMvc()
                .AddJsonOptions(
                    options => { options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; });

            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }

            app.UseStatusCodePages("text/plain", "Ошибка: {0}");

            app.UseStaticFiles();

            app.UseIdentity();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });

            DatabaseInitialize(app.ApplicationServices).Wait();
        }

        private async Task DatabaseInitialize(IServiceProvider serviceProvider)
        {
            string[] roles = {"Teacher", "Student", "Parent", "Moderator", "Admin"};

            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var adminEmail = "admin@admin.com";
            var password = "Aaa123!";

            foreach (var role in roles)
                if (await roleManager.FindByNameAsync(role) == null)
                    await roleManager.CreateAsync(new IdentityRole(role));

            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                var admin = new User
                {
                    Email = adminEmail,
                    UserName = adminEmail,
                    FirstName = "Администратор"
                };
                var result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                    await userManager.AddToRoleAsync(admin, "Admin");
            }
        }
    }
}