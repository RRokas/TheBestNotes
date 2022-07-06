using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using TheBestNotes.Models;
using TheBestNotes.Models.DTO;
using TheBestNotes.Services;

namespace TheBestNotes.Controllers;

public class Notes : Controller
{
    private readonly INoteService _noteService;

    public Notes(INoteService noteService)
    {
        _noteService = noteService;
    }

    [Authorize]
    [HttpPost("Create")]
    public void Create([FromBody] BaseNoteDTO noteData)
    {
        var owner = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        var ownerGuid = new Guid(owner);
        _noteService.AddNewNote(ownerGuid, noteData.Title, noteData.Content);
    }

    [Authorize]
    [HttpGet("GetSingle")]
    public IActionResult GetSingle([FromQuery] Guid noteId)
    {
        var requester = GetRequesterGuid();
        var hasAccessToNote = _noteService.HasAccessToNote(requester, noteId);

        if (hasAccessToNote)
            return Ok(_noteService.GetNote(requester, noteId));
        return Unauthorized();
    }

    private Guid GetRequesterGuid()
    {
        return new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value);
    }
}