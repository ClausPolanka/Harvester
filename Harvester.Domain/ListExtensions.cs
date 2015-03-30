using System;
using System.Collections.Generic;
using System.Linq;

namespace Harvester.Domain
{
    public static class ListExtensions
    {
        public static void ReverseEverySecondElementIn<T>(this List<List<T>> lists)
        {
            lists.ForEach(elem =>
            {
                var i = lists.IndexOf(elem);
                
                if (i%2 == 1)
                    lists[i].Reverse();
            });
        }

        public static List<List<T>> Transpose<T>(this List<List<T>> lists)
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
        
        public static List<List<int>> Make_first_and_last_row_successors(this List<List<int>> lists)
        {
            var reordered = new List<List<int>>();

            while (lists.Any())
            {
                reordered.Add(lists.First().ToList());
                lists.RemoveAt(lists.IndexOf(lists.First()));

                if (lists.Count > 1)
                {
                    reordered.Add(lists.Last().ToList());
                    lists.RemoveAt(lists.IndexOf(lists.Last()));
                }
            }

            return reordered;
        }

        public static string JoinWithBlank(this List<List<int>> plotRows)
        {
            return String.Join(" ", plotRows.SelectMany(row => row));
        }

        public static List<List<int>> MergeTwoRows(this List<List<int>> lists)
        {
            var mergedLists = new List<List<int>>();

            while (lists.Count > 1)
            {
                mergedLists.Add(Merge(lists.First(), lists[1]));
                lists.RemoveAt(0);
                lists.RemoveAt(0);
            }

            if (lists.Any()) mergedLists.Add(lists.Last());

            return mergedLists;
        }

        public static List<int> Merge(List<int> first, List<int> second)
        {
            var list = new List<int>();

            for (var i = 0; i < first.Count; i++)
            {
                list.Add(first[i]);
                list.Add(second[i]);
            }

            return list;
        }
    }
}