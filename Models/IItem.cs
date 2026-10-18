public interface IItem
{
    public int ID { get; set; }
    public string Title { get; set; }
    public string ExternalID { get; set; }
    public bool? Bookmarked { get; set; }
}