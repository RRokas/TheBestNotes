using TheBestNotes.Models;
using Microsoft.EntityFrameworkCore;

namespace TheBestNotes;

public class SqliteDb : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Note> Notes { get; set; }
    public DbSet<NoteCategory> NoteCategories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=TheBestNotes.db;");
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var notes = modelBuilder.Entity<Note>();
        var categories = modelBuilder.Entity<NoteCategory>();

        notes
            .HasOne(i => i.Owner)
            .WithMany(i => i.OwnedNotes);
    }
}