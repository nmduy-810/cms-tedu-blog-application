using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace TeduBlog.Data;

public class TeduBlogDbContextFactory : IDesignTimeDbContextFactory<TeduBlogDbContext>
{
    public TeduBlogDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        
        var builder = new DbContextOptionsBuilder<TeduBlogDbContext>();
        builder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        return new TeduBlogDbContext(builder.Options);
    }
}