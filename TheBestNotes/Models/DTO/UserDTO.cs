namespace TheBestNotes.Models.DTO;

public class UserDTO
{
    public Guid Id { get; init; }
    public string Username { get; init; }
    public List<Note> OwnedNotes { get; init; }
    public List<Note> SharedWithUser { get; init; }
}