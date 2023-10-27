using System.ComponentModel.DataAnnotations;

namespace SmallTalks.Dtos;

public class AudioDto
{
    [Required]
    public string From { get; set; }

    [Required]
    public string Content { get; set; }
    [Required]
    public DateTime Time { get; set; }

    public string? To { get; set; }
}