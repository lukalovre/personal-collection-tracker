using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Movies")]
public class Movie : IItem, ICollection
{
    [Key]
    public int ID { get; set; }
    public string Owner { get; set; } = string.Empty;
    public string Director { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public int? Year { get; set; }
    public string Format { get; set; } = string.Empty;
    public int? Region { get; set; }
    public float? Price { get; set; }
    public float? PriceInRSD { get; set; }
    public DateTime? Date { get; set; }
    public string ExternalID { get; set; } = string.Empty;
    public string Comment { get; set; } = string.Empty;
    public int Runtime { get; set; }
    public bool? Bookmarked { get; set; }
}
