using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Books")]
public class Book : IItem, ICollection
{
    [Key]
    public int ID { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public int? Year { get; set; }
    public int? Pages { get; set; }
    public string Type { get; set; } = string.Empty;
    public bool? _1001 { get; set; }
    public int? EminaRating { get; set; }
    public string ExternalID { get; set; } = string.Empty;
    public float? Price { get; set; }
    public float? PriceInRSD { get; set; }
    public DateTime? Date { get; set; }
    public bool? Bookmarked { get; set; }
}
