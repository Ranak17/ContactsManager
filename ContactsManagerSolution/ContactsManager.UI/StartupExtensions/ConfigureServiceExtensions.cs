using ContactsManager.Core.Domain.IdentityEntities;
using ContactsManager.Core.Domain.RepositoryContracts;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ContactsManager.Core.ServiceContracts;
using ContactsManager.Infrastructure.DBContext;
using Services;
using Repositories;
using Microsoft.AspNetCore.Authorization;

namespace ContactsManager.UI.StartupExtensions
{
    public static class ConfigureServicesExtension
    {
        public static IServiceCollection ConfigureService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddLogging();


            services.AddControllersWithViews();
            //use builder.Services.BuildServiceProvider().GetService()  for server instance
            //var logger = builder.Services.BuildServiceProvider().GetService<ILogger<>>();
            //Add Services to IOC Cotainers
            services.AddScoped<ICountriesRepository, CountriesRepository>();
            services.AddScoped<IPersonRepository, PersonsRepository>();
            services.AddScoped<ICountriesAdderService, CountriesAdderService>();
            services.AddScoped<ICountriesGetterService, CountriesGetterService>();
            services.AddScoped<IPersonsAdderService, PersonsAdderService>();
            services.AddScoped<IPersonsGetterService, PersonsGetterService>();
            //Adding DBContext as service
            services.AddDbContext<ApplicationDBContext>((options) =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            //Enable identity in this project
            services.AddIdentity<ApplicationUser, ApplicationRole>(
            //By default identity framework provide below password validations
            /*         (options) =>{
                     options.Password.RequireDigit = false;      
                     options.Password.RequiredLength = 6;        
                     options.Password.RequireUppercase = false; 
                     options.Password.RequireLowercase = false; 
                     options.Password.RequireNonAlphanumeric = false;
                 }*/)
                .AddEntityFrameworkStores<ApplicationDBContext>()
                .AddDefaultTokenProviders()
                //Configured Respositoory layer that interact with DBContext to manipulate users data 
                .AddUserStore<UserStore<ApplicationUser, ApplicationRole, ApplicationDBContext, Guid>>()
                //Configured Respositoory layer that interact with DBContext to manipulate Roles data
                .AddRoleStore<RoleStore<ApplicationRole, ApplicationDBContext, Guid>>();

            services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();//enforces authorization policy (user must be authenticated) for all the action methods
            });
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
            });



            services.AddHttpLogging(options =>
            {
                options.LoggingFields = HttpLoggingFields.RequestPropertiesAndHeaders | HttpLoggingFields.ResponsePropertiesAndHeaders;

            });


            return services;

        }
    }
}
