using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeduBlog.Core.Domains.Contents;

[Table("PostActivityLogs")]
public class PostActivityLog
{
    [Key]
    public Guid Id { get; set; }

    public Guid PostId { get; set; }
    
    [MaxLength(500)]
    public string? Note { get; set; }

    public PostStatus FromStatus { get; set; }

    public PostStatus ToStatus { get; set; }

    public Guid UserId { get; set; }
    
    public DateTime CreatedDate { get; set; }

}