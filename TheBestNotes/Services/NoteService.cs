using Microsoft.EntityFrameworkCore;
using TheBestNotes.Models;
using TheBestNotes.Services.Interfaces;

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
        return _db.Notes.Single(n => n.Id == noteId);
    }

    public List<Note> GetAllNotes(Guid userId)
    {
        var notes = new List<Note>();

        var ownedNotes = _db.Notes.Where(n => n.Owner.Id == userId);
        notes.AddRange(ownedNotes);
        
        return notes;
    }

    public void UpdateNote(Guid requestingUserId, Guid noteId, string title, string content)
    {
        var noteToUpdate = _db.Notes.Single(n => n.Id == noteId);
        noteToUpdate.Title = title;
        noteToUpdate.Content = content;
        _db.SaveChanges();
    }

    public void DeleteNote(Guid requestingUserId, Guid noteId)
    {
        var noteToDelete = _db.Notes.Single(n => n.Id == noteId);
        _db.Notes.Remove(noteToDelete);
        _db.SaveChanges();
    }

    public void AddCategory(Guid userId, string name)
    {
        var owner = _db.Users.Single(u => u.Id == userId);
        var alreadyExists = _db.NoteCategories.Any(c => c.Name == name && c.Owner.Id == userId);

        if (!alreadyExists)
        {
            _db.NoteCategories.Add(new NoteCategory()
            {
                Name = name,
                Owner = owner
            });
            _db.SaveChanges();
        }
    }

    public void DeleteCategory(Guid categoryId)
    {
        var categoryToRemove = _db.NoteCategories.Single(c => c.Id == categoryId);
        _db.NoteCategories.Remove(categoryToRemove);
        _db.SaveChanges();
    }

    public void AssignCategory(Guid noteId, Guid categoryId)
    {
        var note = _db.Notes.Single(n => n.Id == noteId);
        var category = _db.NoteCategories.Single(c => c.Id == categoryId);
        note.Category = category;
        _db.SaveChanges();
    }

    public NoteCategory GetNoteCategory(Guid categoryId)
    {
        return _db.NoteCategories.Single(c => c.Id == categoryId);
    }

    public bool HasAccessToNote(Guid userId, Guid noteToAccessId)
    {
        var isOwner = _db.Notes.Any(n => n.Owner.Id == userId && n.Id == noteToAccessId);
        
        return isOwner;
    }
}