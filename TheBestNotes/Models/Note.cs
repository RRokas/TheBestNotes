namespace TheBestNotes.Models;

public class Note
{
    public Guid Id { get; set; }
    public User Owner { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public Note()
    {
        Id = Guid.NewGuid();
    }
}