namespace TeduBlog.Core.Models.Common;

public class PagedResult<T> : PagedResultBase where T : class
{
    public List<T> Results { get; set; } = [];
}