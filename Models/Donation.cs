using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Donations")]
public class Donation : IItem, ICollection
{
    [Key]
    public int ID { get; set; }
    public string Link { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public float? PriceInRSD { get; set; }
    public string Currency { get; set; } = string.Empty;
    public float? Price { get; set; }
    public string ExternalID { get; set; } = string.Empty;
    public bool? Bookmarked { get; set; }
    public DateTime? Date { get; set; }
    public string Comment { get; set; } = string.Empty;
}