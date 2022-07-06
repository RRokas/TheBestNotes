using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using TheBestNotes.Attributes;
using TheBestNotes.Models;
using TheBestNotes.Models.DTO;
using TheBestNotes.Services;
using TheBestNotes.Services.Interfaces;

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
        _noteService.AddNewNote(ownerGuid, noteData.Title, noteData.Category, noteData.Content);
    }

    [Authorize]
    [ServiceFilter(typeof(MustBeOwnerOfNoteAttribute))]
    [HttpGet("GetNote")]
    public IActionResult GetNote([FromQuery] Guid noteId)
    {
        var requester = GetRequesterGuid();
        var hasAccessToNote = _noteService.HasAccessToNote(requester, noteId);

        if (hasAccessToNote)
            return Ok(_noteService.GetNote(requester, noteId));
        return Unauthorized();
    }

    [Authorize]
    [ServiceFilter(typeof(MustBeOwnerOfNoteAttribute))]
    [HttpPut("UpdateNote")]
    public IActionResult UpdateNote([FromQuery] Guid noteId, [FromBody] BaseNoteDTO noteDto)
    {
        var title = noteDto.Title;
        var content = noteDto.Content;
        _noteService.UpdateNote(GetRequesterGuid(), noteId, title, content);
        return Ok();
    }

    [Authorize]
    [ServiceFilter(typeof(MustBeOwnerOfNoteAttribute))]
    [HttpDelete("DeleteNote")]
    public IActionResult DeleteNote([FromQuery] Guid noteId)
    {
        _noteService.DeleteNote(GetRequesterGuid(), noteId);
        return Ok();
    }

    [Authorize]
    [HttpGet("GetAllNotes")]
    public IActionResult GetAllNotes()
    {
        var allNotes = _noteService.GetAllNotes(GetRequesterGuid());
        return Ok(allNotes);
    }

    private Guid GetRequesterGuid()
    {
        return new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value);
    }
}