using ContactsManager.UI.StartupExtensions;
using CRUDLearning.Middleware;

namespace CRUDLearning
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Logging
            builder.Host.ConfigureLogging(loggingProvider =>
            {
                loggingProvider.ClearProviders();
                //loggingProvider.AddEventLog();
                loggingProvider.AddConsole();
            });

            builder.Services.ConfigureService(builder.Configuration);

            //app building
            WebApplication app = builder.Build();


            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandlingMiddleware();
            }
            app.UseHttpLogging();
            app.UseStaticFiles(); // serve public content
            app.UseRouting(); // Identifying Action method based on Route
            app.UseAuthentication(); // Reading Identity Cookie
            app.UseAuthorization(); // Validate access permission of the user

            app.MapControllers(); // Execute the filter pipeline (action + filters)
            //app.MapControllerRoute(name:"default",pattern:);
            //
            app.Logger.LogDebug("debug log");
            app.Logger.LogInformation("information log");
            app.Logger.LogWarning("warning log");
            app.Logger.LogError("error log");
            app.Logger.LogCritical("critical log");
            app.Run();

        }
    }
}