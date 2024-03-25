using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Books")]
public class Book : IItem, ICollection
{
    [Key]
    public int ID { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public int? Year { get; set; }
    public int? Pages { get; set; }
    public string Type { get; set; }
    public bool? _1001 { get; set; }
    public int? EminaRating { get; set; }
    public bool? EminaRead { get; set; }
    public bool? LukaRead { get; set; }
    public string ExternalID { get; set; }
    public float? Price { get; set; }
    public float? PriceInRSD { get; set; }
    public DateTime? Date { get; set; }
}
