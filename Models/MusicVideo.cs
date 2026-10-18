using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollectionTracker.Models;

[Table("MusicVideos")]
public record MusicVideo : IItem, ICollection
{
    [Key]
    public int ID { get; set; }
    public string ExternalID { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public int Year { get; set; }
    public int Runtime { get; set; }
    public DateTime? Date { get; set; }
    public bool? Bookmarked { get; set; }
    public string Comment { get; set; } = string.Empty;
    public float? Price { get; set; }
    public float? PriceInRSD { get; set; }
}
