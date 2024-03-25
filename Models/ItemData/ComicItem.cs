using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Comics")]
public class ComicItem : IItem, IExternalItem
{
    [Key]
    public int ID { get; set; }
    public string ExternalID { get; set; }
    public string Title { get; set; }
    public string Writer { get; set; }
    public string Illustrator { get; set; }
    public int Year { get; set; }
    public bool _1001 { get; set; }
    public DateTime? Date { get; set; }
}
