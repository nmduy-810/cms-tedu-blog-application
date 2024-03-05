using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TeduBlog.Core.Domains.Contents;

[Table("PostCategories")]
[Index(nameof(Slug), IsUnique = true)]
public class PostCategory
{
    [Key]
    public Guid Id { get; set; }

    [MaxLength(250)]
    public required string Name { get; set; }

    [Column(TypeName = "varchar(250)")]
    public required string Slug { get; set; }
    
    [MaxLength(150)]
    public string? SeoDescription { get; set; }

    public Guid ParentId { get; set; }

    public int SortOrder { get; set; }
    
    public bool IsActive { get; set; }
    
    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }
}