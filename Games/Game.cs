using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Games")]
public class Game : IItem, ICollection
{
    [Key]
    public int ID { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Developer { get; set; } = string.Empty;
    public string Edition { get; set; } = string.Empty;
    public bool Expansion { get; set; }
    public int Year { get; set; }
    public string Platform { get; set; } = string.Empty;
    public string Client { get; set; } = string.Empty;
    public bool New { get; set; }
    public bool GotFree { get; set; }
    public DateTime? Date { get; set; }
    public float? Price { get; set; }
    public float? PriceInRSD { get; set; }
    public bool PhysicalCopy { get; set; }
    public int HLTB { get; set; }
    public string Owner { get; set; } = string.Empty;
    public string ExternalID { get; set; } = string.Empty;
    public bool? Bookmarked { get; set; }
}
