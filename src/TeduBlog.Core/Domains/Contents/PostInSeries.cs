using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TeduBlog.Core.Domains.Contents;

[Table("PostInSeries")]
[PrimaryKey(nameof(PostId), nameof(SeriesId))]
public class PostInSeries
{
    public Guid PostId { get; set; }

    public Guid SeriesId { get; set; }

    public int DisplayOrder { get; set; }
}