using TheBestNotes.Controllers;

namespace TheBestNotes.Models;

public class NoteCategory
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public User Owner { get; set; }
    public List<Note> NotesInCategory { get; set; }
    
    public NoteCategory()
    {
        Id = Guid.NewGuid();
        NotesInCategory = new List<Note>();
    }
}