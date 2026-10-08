using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Consoles")]
public class ConsoleItem : IItem, ICollection
{
    [Key]
    public int ID { get; set; }
    public string Title { get; set; } = string.Empty;
    public int? Year { get; set; }
    public string Condition { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Generation { get; set; } = string.Empty;
    public string Controllers { get; set; } = string.Empty;
    public string Games { get; set; } = string.Empty;
    public DateTime? Date { get; set; }
    public float? Price { get; set; }
    public float? PriceInRSD { get; set; }
    public string Comment { get; set; } = string.Empty;
    public string ExternalID { get; set; } = string.Empty;
    public bool? Bookmarked { get; set; }
}