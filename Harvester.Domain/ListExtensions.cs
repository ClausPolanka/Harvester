using System;
using System.Collections.Generic;
using System.Linq;

namespace Harvester.Domain
{
    public static class ListExtensions
    {
        public static void ReverseEverySecondElementIn<T>(List<List<T>> list)
        {
            list.ForEach(elem =>
            {
                var i = list.IndexOf(elem);
                
                if (i%2 == 1)
                    list[i].Reverse();
            });
        }

        public static List<List<T>> Transpose<T>(List<List<T>> lists)
        {
            var longest = lists.Any() ? lists.Max(l => l.Count) : 0;
            List<List<T>> outer = new List<List<T>>(longest);
            for (int i = 0; i < longest; i++)
                outer.Add(new List<T>(lists.Count));
            for (int j = 0; j < lists.Count; j++)
            {
                for (int i = 0; i < longest; i++)
                    outer[i].Add(lists[j].Count > i ? lists[j][i] : default(T));
            }
            return outer;
        }

        public static string JoinWithBlank(List<List<int>> plotRows)
        {
            return String.Join(" ", plotRows.SelectMany(row => row));
        }
    }
}