using DataAccess;
using DataAccess.Models;
using Service.Models;

namespace Service.Services;

public class PostService
{
    private AppDbContext _db;

    public PostService(AppDbContext db)
    {
        this._db = db;
    }

    public PostDto? GetById(long id)
    {
        return _db
            .Posts.Select(x => new PostDto
            {
                Id = x.Id,
                Title = x.Title,
                Content = x.Content,
            })
            .SingleOrDefault(x => x.Id == id);
    }

    public IEnumerable<PostDto> GetAll()
    {
        return _db.Posts.Select(x => new PostDto
        {
            Id = x.Id,
            Title = x.Title,
            Content = x.Content,
        });
    }

    public void Create(CreatePostDto post)
    {
        _db.Posts.Add(new Post { Title = post.Title, Content = post.Content });
        _db.SaveChanges();
    }
}
