using TheBestNotes.Models;

namespace TheBestNotes.Services;

public interface INoteService
{
    public void AddNewNote(Guid ownerUserId, string title, string content);
    public Note GetNote(Guid requestingUserId, Guid noteId);
    public bool UpdateNote(Guid requestingUserId, Guid noteId, string title, string content);
    public bool DeleteNote(Guid requestingUserId, Guid noteId);
    public bool ShareNote(Guid requestingUserId, Guid userIdToShareWith, Guid noteId);
}