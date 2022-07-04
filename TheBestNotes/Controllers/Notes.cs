using Microsoft.AspNetCore.Mvc;
using TheBestNotes.Models;

namespace TheBestNotes.Controllers;

public class Notes : Controller
{
    private readonly SqliteDb _dbContext;

    public Notes(SqliteDb dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost("Create")]
    public void CreateNote([FromBody] string title, [FromBody] string content)
    {
        
    }
}