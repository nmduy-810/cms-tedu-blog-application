using TeduBlog.Core.Domains.Contents;

namespace TeduBlog.Core.Models.Contents;

public class PostDto
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public required string Slug { get; set; }
    
    public string? Description { get; set; }

    public Guid CategoryId { get; set; }

    public string? Thumbnail { get; set; }

    public string? Content { get; set; }

    public Guid AuthorUserId { get; set; }

    public string? Source { get; set; }

    public string? Tags { get; set; }

    public string? SeoDescription { get; set; }
    
    public int ViewCount { get; set; }
    
    public bool IsPaid { get; set; }

    public decimal? RoyaltyAmount  { get; set; }
    
    public PostStatus Status { get; set; }
    
    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }
}