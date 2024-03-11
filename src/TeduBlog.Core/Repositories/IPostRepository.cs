using TeduBlog.Core.Domains.Contents;
using TeduBlog.Core.Models.Common;
using TeduBlog.Core.Models.Contents;
using TeduBlog.Core.SeedWorks;

namespace TeduBlog.Core.Repositories;

public interface IPostRepository : IRepositoryBase<Post, Guid>
{
    Task<List<Post>> GetPopularPostsAsync(int count);

    Task<PagedResult<PostInListDto>> GetPostsPagingsAsync(string? keyword, Guid? categoryId, int pageIndex = 1, int pageSize = 10);

}