using System.ComponentModel.DataAnnotations;

namespace TheBestNotes.Models.DTO;

public class BaseNoteDto
{
    [Required]
    public string Title { get; init; }
    [Required]
    public string Content { get; init; }
    [Required]
    public string Category { get; init; }
}