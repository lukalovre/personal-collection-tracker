using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AvaloniaApplication1.Models;

[Table("Games")]
public class GameItem : IItem, IExternalItem
{
    [Key]
    public int ID { get; set; }

    public string Title { get; set; }
    public int Year { get; set; }
    public string Platform { get; set; }
    public string ExternalID { get; set; }
    public DateTime? Date { get; set; }
}
