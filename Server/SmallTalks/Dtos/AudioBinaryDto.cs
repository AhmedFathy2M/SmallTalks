using System.ComponentModel.DataAnnotations;

namespace SmallTalks.Dtos;

public class AudioBinaryDto
{
    [Required]
    public string From { get; set; }

    [Required]
    public byte[] Content { get; set; }
    [Required]
    public DateTime Time { get; set; }

    public string? To { get; set; }
}