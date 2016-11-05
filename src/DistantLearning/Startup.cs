using DataAccessProvider;
using Domain;
using Domain.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var sqlConnectionString = Configuration.GetConnectionString("DistantLearningDb");

            services.AddDbContext<DomainModelContext>(options =>
                    options.UseSqlServer(
                        sqlConnectionString,
                        b => b.MigrationsAssembly("distantlearning")
                    )
            );

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
            services.AddScoped<IDataAccessProvider<Role>, DataAccessProvider<Role>>();
            services.AddScoped<IDataAccessProvider<TeacherDiscipline>, DataAccessProvider<TeacherDiscipline>>();
            services.AddScoped<IDataAccessProvider<Test>, DataAccessProvider<Test>>();
            services.AddScoped<IDataAccessProvider<TestResult>, DataAccessProvider<TestResult>>();
            services.AddScoped<IDataAccessProvider<User>, DataAccessProvider<User>>();
            services.AddScoped<IDataAccessProvider<UserMark>, DataAccessProvider<UserMark>>();
            services.AddScoped<IDataAccessProvider<UserParent>, DataAccessProvider<UserParent>>();
            services.AddScoped<IDataAccessProvider<UserRole>, DataAccessProvider<UserRole>>();
            services.AddScoped<IDataAccessProvider<UserSetting>, DataAccessProvider<UserSetting>>();
            services.AddScoped<IDataAccessProvider<UserStudent>, DataAccessProvider<UserStudent>>();
            services.AddScoped<IDataAccessProvider<UserTeacher>, DataAccessProvider<UserTeacher>>();

            services.AddMvc()
                .AddJsonOptions(
                    options => { options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}