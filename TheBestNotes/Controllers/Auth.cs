using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TheBestNotes.Models;
using TheBestNotes.Models.DTO;
using TheBestNotes.Services;
using TheBestNotes.Services.Interfaces;

namespace TheBestNotes.Controllers;

public class Auth : Controller
{
    private readonly IUserService _userService;

    public Auth(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpPost("Login")]
    public IActionResult Login([FromBody] UserCredentialDto credentials)
    {
        var token = _userService.Login(credentials.Username, credentials.Password);
        if (token != "")
            return Ok(token);
        return Unauthorized();
    }

    [HttpPost("Signup")]
    public IActionResult Signup([FromBody] UserCredentialDto credentials)
    {
        var success = _userService.AddUser(credentials.Username, credentials.Password);
        if (success)
            return Ok();
        return BadRequest();
    }
}