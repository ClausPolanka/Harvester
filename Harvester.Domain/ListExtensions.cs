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

        public static List<List<int>> Merge_two_rows_starting_top_left(this List<List<int>> lists)
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
        
        public static List<List<int>> Merge_two_rows_starting_top_left_outside_in(this List<List<int>> lists)
        {
            var mergedLists = new List<List<int>>();

            while (lists.Count > 1)
            {
                mergedLists.Add(Merge(lists.First(), lists[1]));
                lists.RemoveAt(0);
                lists.RemoveAt(0);
                mergedLists.Add(Merge(lists.SecondToLast(), lists.Last()));
                lists.RemoveLast();
                lists.RemoveLast();
            }

            if (lists.Any())
            {
                var middle = (int) Math.Ceiling(mergedLists.Count / 2.0);
                mergedLists.Insert(middle, lists.Last());
            }

            return mergedLists;
        }

        public static List<List<int>> Merge_two_rows_starting_top_left_reversed(this List<List<int>> lists)
        {
            var mergedLists = new List<List<int>>();

            while (lists.Count > 1)
            {
                mergedLists.Add(ReversedMerge(lists.First(), lists[1]));
                lists.RemoveAt(0);
                lists.RemoveAt(0);
            }

            if (lists.Any()) mergedLists.Add(lists.Last());

            return mergedLists;
        }

        public static List<List<int>> Merge_two_rows_starting_top_left_reversed_outside_in(this List<List<int>> lists)
        {
            var mergedLists = new List<List<int>>();

            while (lists.Count > 1)
            {
                mergedLists.Add(ReversedMerge(lists.First(), lists[1]));
                lists.RemoveAt(0);
                lists.RemoveAt(0);
                mergedLists.Add(ReversedMerge(lists.SecondToLast(), lists.Last()));
                lists.RemoveLast();
                lists.RemoveLast();
            }

            if (lists.Any())
            {
                var middle = (int) Math.Ceiling(mergedLists.Count / 2.0);
                mergedLists.Insert(middle, lists.Last());
            }

            return mergedLists;
        }

        public static List<List<int>> Merge_two_rows_starting_bottom_left(this List<List<int>> lists)
        {
            var mergedLists = new List<List<int>>();

            while (lists.Count > 1)
            {
                mergedLists.Add(Merge(lists.SecondToLast(), lists.Last()));
                lists.RemoveLast();
                lists.RemoveLast();
            }

            if (lists.Any()) mergedLists.Add(lists.First());

            return mergedLists;
        }

        public static List<List<int>> Merge_two_rows_starting_bottom_left_outside_in(this List<List<int>> lists)
        {
            var mergedLists = new List<List<int>>();

            while (lists.Count > 1)
            {
                mergedLists.Add(Merge(lists.SecondToLast(), lists.Last()));
                lists.RemoveLast();
                lists.RemoveLast();
                mergedLists.Add(Merge(lists.First(), lists[1]));
                lists.RemoveAt(0);
                lists.RemoveAt(0);
            }

            if (lists.Any())
            {
                var middle = (int) Math.Ceiling(mergedLists.Count / 2.0);
                mergedLists.Insert(middle, lists.Last());
            }

            return mergedLists;
        }

        public static List<List<int>> Merge_two_rows_starting_bottom_left_reversed(this List<List<int>> lists)
        {
            var mergedLists = new List<List<int>>();

            while (lists.Count > 1)
            {
                mergedLists.Add(ReversedMerge(lists.SecondToLast(), lists.Last()));
                lists.RemoveLast();
                lists.RemoveLast();
                mergedLists.Add(ReversedMerge(lists.First(), lists[1]));
                lists.RemoveAt(0);
                lists.RemoveAt(0);
            }

            if (lists.Any())
            {
                var middle = (int) Math.Ceiling(mergedLists.Count / 2.0);
                mergedLists.Insert(middle, lists.Last());
            }

            return mergedLists;
        }

        public static List<List<int>> Merge_two_rows_starting_bottom_left_reversed_outside_in(this List<List<int>> lists)
        {
            var mergedLists = new List<List<int>>();

            while (lists.Count > 1)
            {
                mergedLists.Add(ReversedMerge(lists.SecondToLast(), lists.Last()));
                lists.RemoveLast();
                lists.RemoveLast();
                mergedLists.Add(ReversedMerge(lists.First(), lists[1]));
                lists.RemoveAt(0);
                lists.RemoveAt(0);
            }

            if (lists.Any())
            {
                var middle = (int) Math.Ceiling(mergedLists.Count / 2.0);
                mergedLists.Insert(middle, lists.Last());
            }

            return mergedLists;
        }

        public static void RemoveLast(this List<List<int>> lists)
        {
            lists.RemoveAt(lists.Count - 1);
        }

        public static List<int> SecondToLast(this List<List<int>> lists)
        {
            return lists[(lists.IndexOf(lists.Last()) - 1)];
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

        public static List<int> ReversedMerge(List<int> first, List<int> second)
        {
            var list = new List<int>();

            for (var i = 0; i < first.Count; i++)
            {
                list.Add(second[i]);
                list.Add(first[i]);
            }

            return list;
        }
    }
}