using AutoMapper;
using TeduBlog.Core.Repositories;
using TeduBlog.Core.SeedWorks;
using TeduBlog.Data.Repositories;

namespace TeduBlog.Data.SeedWorks;

public class UnitOfWork(TeduBlogDbContext context, IMapper mapper) : IUnitOfWork
{
    public IPostRepository Posts { get; } = new PostRepository(context, mapper);

    public async Task<int> CompleteAsync()
    {
        return await context.SaveChangesAsync();
    }

    public void Dispose()
    {
        context.Dispose();
    }
}