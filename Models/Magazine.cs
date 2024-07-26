using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Magazines")]
public class Magazine : IItem, ICollection
{
    [Key]
    public int ID { get; set; }
    public string Title { get; set; } = string.Empty;
    public int Year { get; set; }
    public int Issue { get; set; }
    public string ExternalID { get; set; }
    public DateTime? Date { get; set; }
    public bool? Bookmarked { get; set; }
    public float? Price { get; set; }
    public float? PriceInRSD { get; set; }
}
