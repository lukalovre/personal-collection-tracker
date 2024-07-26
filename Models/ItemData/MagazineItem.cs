using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Magazines")]
public class MagazineItem : IExternalItem
{
    [Key]
    public int ID { get; set; }
    public string ExternalID { get; set; }
}
