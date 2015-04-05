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
            var outer = new List<List<T>>(longest);

            for (var i = 0; i < longest; i++)
                outer.Add(new List<T>(lists.Count));

            for (var j = 0; j < lists.Count; j++)
            {
                for (var i = 0; i < longest; i++)
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

            while (lists.Count > 2)
            {
                mergedLists.Add(Merge(lists.First(), lists[1], lists[2]));
                lists.RemoveAt(0);
                lists.RemoveAt(0);
                lists.RemoveAt(0);
            }

            if (lists.Any())
            {
                var zeros = lists.Last().Select(element => 0).ToList();
                mergedLists.Add(Merge(lists.SecondToLast(), lists.Last(), zeros));
            }

            return mergedLists;
        }

        public static List<List<int>> Merge_two_rows_starting_top_left_outside_in(this List<List<int>> lists)
        {
            var capacity = (int) Math.Ceiling(lists.Count/2.0);
            var mergedLists = new List<int>[capacity];
            int startIndex = 0, endIndex = capacity - 1;

            while (lists.Count > 1)
            {
                mergedLists[startIndex++] = Merge(lists.First(), lists[1]);
                lists.RemoveAt(0);
                lists.RemoveAt(0);
                mergedLists[endIndex--] = Merge(lists.SecondToLast(), lists.Last());
                lists.RemoveLast();
                lists.RemoveLast();
            }

            if (lists.Any())
            {
                var middle = (int) Math.Ceiling(capacity/2.0) - 1;
                mergedLists[middle] = lists.Last();
            }

            return new List<List<int>>(mergedLists);
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
            var capacity = (int) Math.Ceiling(lists.Count/3.0);
            var mergedLists = new List<int>[capacity];
            int startIndex = 0, endIndex = capacity - 1;

            while (lists.Count > 2)
            {
                mergedLists[startIndex++] = ReversedMerge(lists.First(), lists[1], lists[2]);
                lists.RemoveAt(0);
                lists.RemoveAt(0);
                lists.RemoveAt(0);

                if (lists.Count <= 2)
                    break;

                mergedLists[endIndex--] = ReversedMerge(lists[lists.IndexOf(lists.SecondToLast()) - 1],
                    lists.SecondToLast(), lists.Last());
                lists.RemoveLast();
                lists.RemoveLast();
                lists.RemoveLast();
            }

            if (lists.Any())
            {
                var middle = (int) Math.Ceiling(capacity/2.0) - 1;
                var zeros = lists.Last().Select(s => 0).ToList();
                mergedLists[middle] = ReversedMerge(zeros, lists.Last());
            }

            return new List<List<int>>(mergedLists);
        }

        public static List<List<int>> Merge_two_rows_starting_bottom_left(this List<List<int>> lists)
        {
            var mergedLists = new List<List<int>>();

            while (lists.Count > 2)
            {
                mergedLists.Add(Merge(lists[lists.IndexOf(lists.SecondToLast()) - 1], lists.SecondToLast(), lists.Last()));
                lists.RemoveLast();
                lists.RemoveLast();
                lists.RemoveLast();
            }

            if (lists.Any())
            {
                var zeros = lists.First().Select(s => 0).ToList();
                mergedLists.Add(Merge(zeros, zeros, lists.First()));
            }

            return mergedLists;
        }

        public static List<List<int>> Merge_two_rows_starting_bottom_left_outside_in(this List<List<int>> lists)
        {
            int width = 3; // TODO pass as arg.

            var mergedLists = new List<List<int>>();

            while (lists.Count >= width)
            {
                var listsToMerge = AddListsToMergeStartingAtTheEnd(lists, width);
                listsToMerge.Reverse();
                
                mergedLists.Add(Merge(listsToMerge));

                for (var i = 0; i < width; i++)
                    lists.RemoveLast();

                if (lists.Count < width) break;

                mergedLists.Add(Merge(ListsToMergeStartingAtTheBeginning(lists, width)));
                
                for (var i = 0; i < width; i++)
                    lists.RemoveAt(0);
            }

            if (lists.Any())
            {
                var middle = (int) Math.Ceiling(mergedLists.Count/2.0);
                var zeros = lists.Last().Select(s => 0).ToList();
                mergedLists.Insert(middle, Merge(zeros, lists.SecondToLast(), lists.Last())); // TODO use for loop
            }

            return mergedLists;
        }

        public static List<List<int>> Merge_two_rows_starting_bottom_left_reversed(
            this List<List<int>> lists,
            int width)
        {
            var mergedLists = new List<List<int>>();

            while (lists.Count >= width)
            {
                var listsToMerge = AddListsToMergeStartingAtTheEnd(lists, width);

                mergedLists.Add(ReversedMerge(listsToMerge));

                for (var i = 0; i < width; i++)
                    lists.RemoveLast();
            }

            if (lists.Any())
            {
                var zeros = lists.First().Select(s => 0).ToList();
                mergedLists.Add(ReversedMerge(new List<List<int>> { zeros, lists.First() }));
            }

            return mergedLists;
        }

        private static List<List<int>> ListsToMergeStartingAtTheBeginning(List<List<int>> lists, int width)
        {
            var toMerge = new List<List<int>>();

            for (var i = 0; i <= width; i++)
                toMerge.Add(lists[i]);

            return toMerge;
        }

        private static List<List<int>> AddListsToMergeStartingAtTheEnd(List<List<int>> lists, int width)
        {
            var toMerge = new List<List<int>>();

            for (var i = 1; i <= width; i++)
                toMerge.Add(lists[lists.Count - i]);

            return toMerge;
        }

        public static List<List<int>> Merge_two_rows_starting_bottom_left_reversed_outside_in(
            this List<List<int>> lists)
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
                var middle = (int) Math.Ceiling(mergedLists.Count/2.0);
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

        public static List<int> Merge(List<int> first, List<int> second, List<int> third)
        {
            var list = new List<int>();

            for (var i = 0; i < first.Count; i++)
            {
                list.Add(first[i]);
                list.Add(second[i]);
                list.Add(third[i]);
            }

            return list;
        }

        public static List<int> Merge(List<List<int>> lists)
        {
            var list = new List<int>();

            for (var i = 0; i < lists.First().Count; i++)
            {
                foreach (var l in lists)
                    list.Add(l[i]);
            }

            return list;
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

        public static List<int> ReversedMerge(List<int> first, List<int> second, List<int> third)
        {
            var list = new List<int>();

            for (var i = 0; i < first.Count; i++)
            {
                list.Add(third[i]);
                list.Add(second[i]);
                list.Add(first[i]);
            }

            return list;
        }

        public static List<int> ReversedMerge(List<List<int>> toMerge)
        {
            var list = new List<int>();

            for (var i = 0; i < toMerge.First().Count; i++)
            {
                foreach (var m in toMerge)
                    list.Add(m[i]);
            }

            return list;
        }
    }
}