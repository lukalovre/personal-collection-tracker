using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Standup")]
public class Standup : IItem, ICollection
{
    [Key]
    public int ID { get; set; }
    public string Performer { get; set; }
    public string Title { get; set; }
    public int Year { get; set; }
    public int Runtime { get; set; }
    public string Link { get; set; }
    public string Writer { get; set; }
    public string Director { get; set; }
    public string Country { get; set; }
    public string Plot { get; set; }
    public string ExternalID { get; set; }
    public DateTime? Date { get; set; }
    public bool? Bookmarked { get; set; }
    public float? Price { get; set; }
    public float? PriceInRSD { get; set; }
}
