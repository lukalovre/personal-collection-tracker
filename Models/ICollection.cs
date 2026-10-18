using System;

public interface ICollection
{
    public float? Price { get; set; }
    public float? PriceInRSD { get; set; }
    public string Comment { get; set; }
    public DateTime? Date { get; set; }
}
