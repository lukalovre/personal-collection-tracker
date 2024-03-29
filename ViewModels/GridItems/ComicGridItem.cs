using AvaloniaApplication1.Models;

public record ComicGridItem(
    int ID,
    int Index,
    string Title,
    string Writer,
    int? Chapter,
    int Pages,
    int? Rating) : IGridItem;
