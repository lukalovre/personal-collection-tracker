using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Movies")]
public class Movie : IItem, ICollection
{
    [Key]
    public int ID { get; set; }
    public string Owner { get; set; }
    public string Director { get; set; }
    public string Title { get; set; }
    public int? Year { get; set; }
    public string Format { get; set; }
    public int? Region { get; set; }
    public float? Price { get; set; }
    public float? PriceInRSD { get; set; }
    public DateTime? Date { get; set; }
    public string ExternalID { get; set; }
    public string Comment { get; set; }
    public int Runtime { get; set; }
    public bool? Bookmarked { get; set; }
}
