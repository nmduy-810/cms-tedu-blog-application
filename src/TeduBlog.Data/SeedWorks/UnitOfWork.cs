using Microsoft.EntityFrameworkCore;
using TeduBlog.Core.SeedWorks;

namespace TeduBlog.Data.SeedWorks;

public class UnitOfWork(DbContext context) : IUnitOfWork
{
    public async Task<int> CompleteAsync()
    {
        return await context.SaveChangesAsync();
    }

    public void Dispose()
    {
        context.Dispose();
    }
}