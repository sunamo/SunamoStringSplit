using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunamoStringSplit;
internal static class ListExtensions
{
    internal static List<T> AddOrSet<T>(this IList<T> list, int dx, T item)
    {
        if (list.Count > dx)
        {
            list[dx] = item;
        }
        else
        {
            list.Add(item);
        }
        return list.ToList();
    }
}
