using System.Collections.Generic;
using AvaloniaApplication1.Models;

namespace Repositories;

public interface IDatasource
{
    void Add<T>(T item)
        where T : IItem;

    List<T> GetList<T>()
        where T : IItem;

    List<T> GetDoneList<T>()
        where T : IExternalItem;

    void MakeBackup(string path);

    void Update<T>(T item)
        where T : IItem;
}
