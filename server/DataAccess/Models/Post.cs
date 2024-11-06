using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models;

[Index("AuthorId", Name = "IX_Posts_AuthorId")]
public partial class Post
{
    [Key]
    public long Id { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public string? AuthorId { get; set; }

    [ForeignKey("AuthorId")]
    [InverseProperty("Posts")]
    public virtual AspNetUser? Author { get; set; }
}
