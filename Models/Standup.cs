using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Standup")]
public class Standup : IItem
{
    [Key]
    public int ID { get; set; }
    public string Performer { get; set; }
    public string Title { get; set; }
    public int Year { get; set; }
    public int Runtime { get; set; }
    public string Link { get; set; }
    public string Writer { get; set; }
    public string Director { get; set; }
    public string Country { get; set; }
    public string Plot { get; set; }
    public string ExternalID { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public DateTime? Date { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
}
