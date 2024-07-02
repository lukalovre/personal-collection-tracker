using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Songs")]
public class Song : IItem, ICollection
{
    [Key]
    public int ID { get; set; }
    public string Artist { get; set; }
    public string Title { get; set; }
    public int Year { get; set; }
    public int Runtime { get; set; }
    public string Link { get; set; }
    public string ExternalID { get; set; }
    public DateTime? Date { get; set; }
    public bool? Bookmarked { get; set; }
    public float? Price { get; set; }
    public float? PriceInRSD { get; set; }
}
