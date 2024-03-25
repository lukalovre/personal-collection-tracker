using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Books")]
public class BookItem : IItem, IExternalItem
{
    [Key]
    public int ID { get; set; }
    public string ExternalID { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public int Year { get; set; }
    public bool is1001 { get; set; }
    public DateTime? Date { get; set; }
}
