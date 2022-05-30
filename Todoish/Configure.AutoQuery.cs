using Microsoft.AspNetCore.Hosting;
using ServiceStack;
using ServiceStack.Data;

[assembly: HostingStartup(typeof(Todoish.ConfigureAutoQuery))]

namespace Todoish
{
    public class ConfigureAutoQuery : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder) => builder
            .ConfigureServices(services => {
                // Enable Audit History
                services.AddSingleton<ICrudEvents>(c =>
                    new OrmLiteCrudEvents(c.Resolve<IDbConnectionFactory>()));
            })
            .ConfigureAppHost(appHost => {
                appHost.Plugins.Add(new AutoQueryFeature {
                    MaxLimit = 1000,
                    //IncludeTotal = true,
                });

                appHost.Resolve<ICrudEvents>().InitSchema();
            });
    }
}