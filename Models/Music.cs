using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Music")]
public class Music : IItem, ICollection
{
    [Key]
    public int ID { get; set; }
    public string Owner { get; set; }
    public string Artist { get; set; }
    public string Title { get; set; }
    public int? Year { get; set; }
    public string Format { get; set; }
    public float? Price { get; set; }
    public float? PriceInRSD { get; set; }
    public DateTime? Date { get; set; }
    public string ExternalID { get; set; }
}
