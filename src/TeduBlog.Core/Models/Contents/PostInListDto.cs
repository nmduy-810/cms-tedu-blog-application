using AutoMapper;
using TeduBlog.Core.Domains.Contents;

namespace TeduBlog.Core.Models.Contents;

public class PostInListDto
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public required string Slug { get; set; }
    
    public int ViewCount { get; set; }
    
    public DateTime CreatedDate { get; set; }

    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Post, PostInListDto>();
        }
    }
}