using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Work")]
public class Work : IItem, ICollection
{
    [Key]
    public int ID { get; set; }
    public string Title { get; set; }
    public string Collaborators { get; set; }
    public DateTime? Date { get; set; }
    public bool Public { get; set; }
    public string Type { get; set; }
    public string Location { get; set; }
    public string PublicLocation { get; set; }
    public bool Finished { get; set; }
    public bool? Main { get; set; }
    public string ExternalID { get; set; }
    public float? Price { get; set; }
    public float? PriceInRSD { get; set; }
}
