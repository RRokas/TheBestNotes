using TheBestNotes.Controllers;

namespace TheBestNotes.Models;

public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public byte[] Password { get; set; }
    public byte[] Salt { get; set; }
    public List<Note> OwnedNotes { get; set; }
    public List<Note> SharedWithUser { get; set; }

    public User()
    {
        Id = Guid.NewGuid();
    }
}