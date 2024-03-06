using Microsoft.EntityFrameworkCore;
using TeduBlog.Data;

namespace TeduBlog.Api;

public static class MigrationManager
{
    public static void MigrationDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<TeduBlogDbContext>();
        context.Database.Migrate();
        new SeedData().SeedAsync(context).Wait();
    }
}