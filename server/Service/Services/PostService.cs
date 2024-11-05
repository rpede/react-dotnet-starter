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
            .Posts.Join(
                _db.Users,
                post => post.AuthorId,
                user => user.Id,
                (post, user) => new { post, user }
            )
            .Select(x => new PostDto
            {
                Id = x.post.Id,
                Title = x.post.Title,
                Content = x.post.Content,
                Author = x.user.UserName ?? "",
            })
            .SingleOrDefault(x => x.Id == id);
    }

    public IEnumerable<PostDto> GetAll()
    {
        return _db
            .Posts.Join(
                _db.Users,
                post => post.AuthorId,
                user => user.Id,
                (post, user) => new { post, user }
            )
            .Select(x => new PostDto
            {
                Id = x.post.Id,
                Title = x.post.Title,
                Content = x.post.Content,
                Author = x.user.UserName ?? "",
            });
    }

    public void Create(CreatePostDto post)
    {
        _db.Posts.Add(new Post { Title = post.Title, Content = post.Content });
        _db.SaveChanges();
    }
}
