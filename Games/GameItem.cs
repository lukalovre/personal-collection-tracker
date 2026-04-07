using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Games")]
public class GameItem : IExternalItem
{
    [Key]
    public int ID { get; set; }
    public string ExternalID { get; set; }
}
