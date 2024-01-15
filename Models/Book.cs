using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AvaloniaApplication1.Models;

[Table("Books")]
public class Book : IItem
{
    [Key]
    public int ID { get; set; }
    public int GoodreadsID { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public int Year { get; set; }
    public bool is1001 { get; set; }
    public string ExternalID { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public DateTime? Date { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
}
