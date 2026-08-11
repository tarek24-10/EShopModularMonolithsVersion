using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shared.Data.Seed;

namespace Shared.Data
{
    public static class Extensions
    {
        public static IApplicationBuilder UseMigration<Tcontext>(this IApplicationBuilder app) where Tcontext : DbContext
        {
            MigrateDatabaseAsync<Tcontext>(app.ApplicationServices).GetAwaiter().GetResult();
            SeedDatabaseAsync(app.ApplicationServices).GetAwaiter().GetResult();
            return app;
        }

        private static async Task MigrateDatabaseAsync<TContext>(IServiceProvider serviceProvider) where TContext : DbContext
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<TContext>();
            await context.Database.MigrateAsync();
        }

        private static async Task SeedDatabaseAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var seeders = scope.ServiceProvider.GetServices<IDataSeeder>(); // Get Services (s)
            foreach (var seeder in seeders)
            {
                await seeder.SeedAllAsync();
            }
        }
    }
}
