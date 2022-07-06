using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TheBestNotes.Models;
using TheBestNotes.Models.DTO;
using TheBestNotes.Services;

namespace TheBestNotes.Controllers;

public class Auth : Controller
{
    private readonly IUserService _userService;

    public Auth(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpPost("Login")]
    public IActionResult Login([FromBody] UserCredentialDTO credentials)
    {
        var token = _userService.Login(credentials.Username, credentials.Password);
        if (token != "")
            return Ok(token);
        return Unauthorized();
    }

    [HttpPost("Signup")]
    public IActionResult Signup([FromBody] UserCredentialDTO credentials)
    {
        var success = _userService.AddUser(credentials.Username, credentials.Password);
        if (success)
            return Ok();
        return BadRequest();
    }
}