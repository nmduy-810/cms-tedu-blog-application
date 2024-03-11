using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TeduBlog.Core.Domains.Contents;
using TeduBlog.Core.Models.Common;
using TeduBlog.Core.Models.Contents;
using TeduBlog.Core.Repositories;
using TeduBlog.Data.SeedWorks;

namespace TeduBlog.Data.Repositories;

public class PostRepository(TeduBlogDbContext context, IMapper mapper) : RepositoryBase<Post, Guid>(context), IPostRepository
{
    private readonly IMapper _mapper = mapper;
    
    public async Task<List<Post>> GetPopularPostsAsync(int count)
    {
        return await Context.Posts.OrderByDescending(x => x.ViewCount).Take(count).ToListAsync();
    }

    public async Task<PagedResult<PostInListDto>> GetPostsPagingsAsync(string? keyword, Guid? categoryId, int pageIndex = 1, int pageSize = 10)
    {
        var query = Context.Posts.AsQueryable();
        if (!string.IsNullOrEmpty(keyword))
        {
            query = query.Where(x => x.Name.Contains(keyword));
        }

        if (categoryId.HasValue)
        {
            query = query.Where(x => x.CategoryId == categoryId.Value);
        }

        var count = await query.CountAsync();

        query = query.OrderByDescending(x => x.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);

        return new PagedResult<PostInListDto>()
        {
            Results = await _mapper.ProjectTo<PostInListDto>(query).ToListAsync(),
            CurrentPage = pageIndex,
            RowCount = count,
            PageSize = pageSize
        };
    }
}