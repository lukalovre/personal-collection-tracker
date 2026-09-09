using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Comics")]
public class Comic : IItem, ICollection
{
    [Key]
    public int ID { get; set; }
    public string Owner { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public int? Year { get; set; }
    public int Chapter { get; set; }
    public string Writer { get; set; } = string.Empty;
    public string Illustrator { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;
    public string ExternalID { get; set; } = string.Empty;
    public DateTime? Date { get; set; }
    public float? Price { get; set; }
    public float? PriceInRSD { get; set; }
    public bool? Bookmarked { get; set; }
}
