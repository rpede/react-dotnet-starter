using Microsoft.AspNetCore.Mvc;
using Service.Models;
using Service.Services;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PostController : ControllerBase
{
    private readonly ILogger<PostController> _logger;
    private readonly PostService _service;

    public PostController(ILogger<PostController> logger, PostService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet("")]
    public IEnumerable<PostDto> List()
    {
        return _service.GetAll();
    }

    [HttpGet(":id")]
    public PostDto? Get(long id)
    {
        return _service.GetById(id);
    }

    [HttpPost]
    public IResult New([FromBody] CreatePostDto post)
    {
        _service.Create(post);
        return Results.Created();
    }
}
