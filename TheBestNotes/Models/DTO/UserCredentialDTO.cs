using System.ComponentModel.DataAnnotations;

namespace TheBestNotes.Models.DTO;

public class UserCredentialDto
{
    [Required]
    public string Username { get; init; }
    [Required]
    public string Password { get; init; }
}