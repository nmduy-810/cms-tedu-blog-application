using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TeduBlog.Core.Domains.Contents;
using TeduBlog.Core.Models.Common;
using TeduBlog.Core.Models.Contents;
using TeduBlog.Core.SeedWorks;

namespace TeduBlog.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PostController(IUnitOfWork unitOfWork, IMapper mapper) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreatePost([FromBody] CreateOrUpdatePostRequestDto request)
    {
        var post = mapper.Map<CreateOrUpdatePostRequestDto, Post>(request);

        unitOfWork.Posts.Add(post);

        var result = await unitOfWork.CompleteAsync();
        
        return result > 0 ? Ok() : BadRequest();
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdatePost(Guid id, [FromBody] CreateOrUpdatePostRequestDto request)
    {
        var post = await unitOfWork.Posts.GetByIdAsync(id);
        if (post == null)
        {
            return NotFound();
        }
        mapper.Map(request, post);

        var result = await unitOfWork.CompleteAsync();
        return result > 0 ? Ok() : BadRequest();
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeletePosts([FromQuery] Guid[] ids)
    {
        foreach (var id in ids)
        {
            var post = await unitOfWork.Posts.GetByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            unitOfWork.Posts.Remove(post);
        }
        var result = await unitOfWork.CompleteAsync();
        return result > 0 ? Ok() : BadRequest();
    }
    
    [HttpGet]
    [Route("{id:guid}")]
    public async Task<ActionResult<PostDto>> GetPostById(Guid id)
    {
        var post = await unitOfWork.Posts.GetByIdAsync(id);
        if (post == null)
        {
            return NotFound();
        }
        return Ok(post);
    }
    
    [HttpGet]
    [Route("paging")]
    public async Task<ActionResult<PagedResult<PostInListDto>>> GetPostsPaging(string? keyword, Guid? categoryId,
        int pageIndex, int pageSize = 10)
    {
        var result = await unitOfWork.Posts.GetPostsPagingsAsync(keyword, categoryId, pageIndex, pageSize);
        return Ok(result);
    }
    
}