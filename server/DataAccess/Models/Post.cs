using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models;

[Index("AuthorId", Name = "IX_Posts_AuthorId")]
public partial class Post
{
    [Key]
    public long Id { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public string? AuthorId { get; set; } = null;
    public IdentityUser? Author { get; set; }
}
