using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TeduBlog.Core.Domains.Contents;

[Table("PostTags")]
[PrimaryKey(nameof(PostId), nameof(TagId))]
public class PostTag
{
    public Guid PostId { get; set; }

    public Guid TagId { get; set; }
}