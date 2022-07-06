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
        _db.SaveChanges();
    }

    public Note GetNote(Guid requestingUserId, Guid noteId)
    {
        var hasAccess = HasAccessToNote(requestingUserId, noteId);
        if (hasAccess)
            return _db.Notes.Single(n => n.Id == noteId);
        return new Note();
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

    public bool HasAccessToNote(Guid userId, Guid noteToAccessId)
    {
        var isOwner = _db.Notes.Any(n => n.Owner.Id == userId && n.Id == noteToAccessId);
        
        return isOwner;
    }
}