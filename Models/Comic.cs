using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Comics")]
public class Comic : IItem, ICollection
{
    [Key]
    public int ID { get; set; }
    public string Owner { get; set; }
    public string Title { get; set; }
    public int Chapter { get; set; }
    public string Writer { get; set; }
    public string Illustrator { get; set; }
    public string Language { get; set; }
    public string ExternalID { get; set; }
    public DateTime? Date { get; set; }
    public float? Price { get; set; }
    public float? PriceInRSD { get; set; }
}
