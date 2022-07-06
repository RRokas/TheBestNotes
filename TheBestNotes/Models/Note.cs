using TheBestNotes.Models.DTO;

namespace TheBestNotes.Models;

public class Note
{
    public Guid Id { get; set; }
    public User Owner { get; set; }
    public NoteCategory Category { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public Note()
    {
        Id = Guid.NewGuid();
    }

    public BaseNoteDto GetDto()
    {
        return new BaseNoteDto()
        {
            Title = Title,
            Category = Category.Name,
            Content = Content
        };
    }
}