using System.ComponentModel.DataAnnotations;

namespace TheBestNotes.Models.DTO;

public class BaseNoteDTO
{
    [Required]
    public string Title { get; init; }
    [Required]
    public string Content { get; init; }
}