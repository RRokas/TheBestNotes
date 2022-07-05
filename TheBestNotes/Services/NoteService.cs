using Microsoft.EntityFrameworkCore;
using TheBestNotes.Models;

namespace TheBestNotes.Services;

public class NoteService : INoteService
{
    private readonly SqliteDb _db;

    public NoteService(SqliteDb dbContext)
    {
        _db = dbContext;
    }
    
    public void AddNewNote(Guid ownerUserId, string title, string content)
    {
        var noteToAdd = new Note()
        {
            Owner = _db.Users.Single(u => u.Id == ownerUserId),
            Title = title,
            Content = content
        };

        _db.Notes.Add(noteToAdd);
    }

    public Note GetNote(Guid requestingUserId, Guid noteId)
    {
        throw new NotImplementedException();
    }

    public bool UpdateNote(Guid requestingUserId, Guid noteId, string title, string content)
    {
        throw new NotImplementedException();
    }

    public bool DeleteNote(Guid requestingUserId, Guid noteId)
    {
        throw new NotImplementedException();
    }

    public bool ShareNote(Guid requestingUserId, Guid userIdToShareWith, Guid noteId)
    {
        throw new NotImplementedException();
    }
}