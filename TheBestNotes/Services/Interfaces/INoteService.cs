using TheBestNotes.Models;

namespace TheBestNotes.Services.Interfaces;

public interface INoteService
{
    public void AddNewNote(Guid ownerUserId, string title, string category, string content);
    public Note GetNote(Guid requestingUserId, Guid noteId);
    public List<Note> GetAllNotes(Guid userId);
    public bool UpdateNote(Guid requestingUserId, Guid noteId, string title, string content);
    public void DeleteNote(Guid requestingUserId, Guid noteId);
    public bool HasAccessToNote(Guid userId, Guid noteToAccessId);
}