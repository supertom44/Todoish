using Todoish.ServiceModel;
using ServiceStack.Data;
using ServiceStack.OrmLite;

[assembly: HostingStartup(typeof(Todoish.ConfigureDb))]

namespace Todoish
{
    public class ConfigureDb : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder) => builder
            .ConfigureServices((context,services) => services.AddSingleton<IDbConnectionFactory>(new OrmLiteConnectionFactory(
                context.Configuration.GetConnectionString("DefaultConnection") ?? ":memory:",
                SqliteDialect.Provider)))
            .ConfigureAppHost(appHost =>
            {
                // Create non-existing Table and add Seed Data Example
                using var db = appHost.Resolve<IDbConnectionFactory>().Open();                
                if (db.CreateTableIfNotExists<TodoTask>())
                {
                    db.Insert<TodoTask>(new TodoTask
                    {
                        Title = "My First Task",
                        DueDate = DateTime.Today
                    });
                    
                    db.Insert<TodoTask>(new TodoTask
                    {
                        Title = "Tomorrows Task",
                        DueDate = DateTime.Today + TimeSpan.FromDays(1)
                    });
                }

            });
    }
}
