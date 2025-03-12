using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using WordFlowServer;

namespace WordFlowTest
{
    public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var dbContextDescriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<DataContext>));

                if (dbContextDescriptor != null)
                {
                    services.Remove(dbContextDescriptor);
                }

                var dbConnectionDescriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbConnection));

                var uniqueDbName = $"wordFlowTest_{Guid.NewGuid()}";
                services.AddDbContext<DataContext>(options =>
                {
                    options.UseSqlServer($"server = (localdb)\\MSSQLLocalDB; database = {uniqueDbName}; trusted_connection = true");
                });
            });

            builder.UseEnvironment("Development");
        }
    }
}
