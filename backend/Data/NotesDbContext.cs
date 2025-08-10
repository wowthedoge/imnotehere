using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data;

public class NotesDbContext : DbContext
{
    public NotesDbContext(DbContextOptions<NotesDbContext> options)
        : base(options) { }

    public DbSet<Note> Notes { get; set; }
}
