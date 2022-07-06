using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TheBestNotes.Services;
using TheBestNotes.Services.Interfaces;

namespace TheBestNotes.Attributes;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public class MustBeOwnerOfNoteAttribute : Attribute, IAsyncActionFilter
{
    private readonly INoteService _noteService;

    public MustBeOwnerOfNoteAttribute(INoteService noteService)
    {
        _noteService = noteService;
    }
    
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var userId = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        var queryString = context.HttpContext.Request.QueryString.ToString();
        var noteId = queryString.Split("=")[1];

        var userHasAccessToNote = _noteService.HasAccessToNote(Guid.Parse(userId), Guid.Parse(noteId));
        
            if (userHasAccessToNote)
            await next();
        
        context.Result = new UnauthorizedResult();
    }
}