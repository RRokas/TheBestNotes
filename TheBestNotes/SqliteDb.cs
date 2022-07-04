using TheBestNotes.Models;
using Microsoft.EntityFrameworkCore;

namespace TheBestNotes;

public class SqliteDb : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Note> Notes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=TheBestNotes.db;");
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var notes = modelBuilder.Entity<Note>();

        notes
            .HasOne(i => i.Owner)
            .WithMany(i => i.OwnedNotes);

        notes
            .HasMany(i => i.SharedWith)
            .WithMany(i => i.SharedWithUser);
    }
}