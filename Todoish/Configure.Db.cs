using Todoish.ServiceModel;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System.Data;

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

                if (db.CreateTableIfNotExists<Todo>())
                {
                    db.Insert<Todo>(new Todo[]
                    {
                        new() {Id = 1, Text = "Learn"},
                        new() {Id = 2, Text = "Blazor", IsFinished = true},
                        new() {Id = 3, Text = "WASM!"},
                    });
                }
            });
    }
}
