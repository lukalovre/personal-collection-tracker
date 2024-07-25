using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Library")]
public class Library : IItem
{
    [Key]
    public int ID { get; set; }
    public int ItemID { get; set; }
    public string Type { get; set; } = string.Empty;
    public int PersonID { get; set; }
    public DateTime LentDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public string ExternalID { get; set; } = string.Empty;
    public DateTime? Date { get; set; }
    public bool? Bookmarked { get; set; }
    public string Title { get; set; } = string.Empty;
}
