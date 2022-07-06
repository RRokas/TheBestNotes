using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheBestNotes.Controllers;
using System.Text.Json.Serialization;
namespace TheBestNotes.Models;

public class User
{
    public Guid Id { get; set; }
    public string Role { get; set; }
    public string Username { get; set; }
    public byte[] Password { get; set; }
    public byte[] Salt { get; set; }
    public List<Note> OwnedNotes { get; set; }

    public User()
    {
        Id = Guid.NewGuid();
        OwnedNotes = new List<Note>();
    }
}