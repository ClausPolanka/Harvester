using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

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

        public static List<List<int>> Merge_two_rows_starting_top_left(this List<List<int>> lists, int width)
        {
            var mergedLists = new List<List<int>>();

            while (lists.Count >= width)
            {
                var toMerge = new List<List<int>>();

                for (var i = 0; i < width; i++)
                    toMerge.Add(lists[i]);

                mergedLists.Add(Merge(toMerge));

                for (var i = 0; i < width; i++)
                    lists.RemoveAt(0);
            }

            if (lists.Any())
                mergedLists.Add(Merge(lists));

            return mergedLists;
        }

        public static List<List<int>> Merge_two_rows_starting_top_left_with_zeros(this List<List<int>> lists, int width)
        {
            var mergedLists = new List<List<int>>();

            while (lists.Count >= width)
            {
                var toMerge = new List<List<int>>();

                for (var i = 0; i < width; i++)
                    toMerge.Add(lists[i]);

                mergedLists.Add(Merge(toMerge));

                for (var i = 0; i < width; i++)
                    lists.RemoveAt(0);
            }

            if (lists.Any())
            {
                var zeros = lists.Last().Select(element => 0).ToList();
                var toMerge = new List<List<int>>();

                toMerge.AddRange(lists);

                for (var i = 0; i < (width - lists.Count); i++)
                    toMerge.Add(zeros);

                mergedLists.Add(Merge(toMerge));
            }

            return mergedLists;
        }

        public static List<List<int>> Merge_two_rows_starting_top_left_outside_in_with_zeros(
            this List<List<int>> lists,
            int width)
        {
            var capacity = (int) Math.Ceiling(lists.Count/(double) width);
            var mergedLists = new List<int>[capacity];
            int startIndex = 0, endIndex = capacity - 1;

            while (lists.Count >= width)
            {
                var toMerge = new List<List<int>>();

                for (var i = 0; i < width; i++)
                    toMerge.Add(lists[i]);

                mergedLists[startIndex++] = Merge(toMerge);

                for (var i = 0; i < width; i++)
                    lists.RemoveAt(0);

                if (lists.Count <= width)
                    break;

                toMerge = new List<List<int>>();

                for (var i = 0; i < width; i++)
                    toMerge.Add(lists[(lists.Count - 1) - i]);

                toMerge.Reverse();

                mergedLists[endIndex--] = Merge(toMerge);

                for (var i = 0; i < width; i++)
                    lists.RemoveLast();
            }

            if (lists.Any())
            {
                var middle = (int) Math.Ceiling(capacity/2.0) - 1;
                var zeros = lists.First().Select(s => 0).ToList();
                var toMerge = new List<List<int>>();

                toMerge.AddRange(lists);

                for (var i = 0; i < (width - lists.Count); i++)
                    toMerge.Add(zeros);

                mergedLists[middle] = Merge(toMerge);
            }

            return new List<List<int>>(mergedLists);
        }

        public static List<List<int>> Merge_two_rows_starting_top_left_outside_in(this List<List<int>> lists, int width)
        {
            var capacity = (int) Math.Ceiling(lists.Count/(double) width);
            var mergedLists = new List<int>[capacity];
            int startIndex = 0, endIndex = capacity - 1;

            while (lists.Count >= width)
            {
                var toMerge = new List<List<int>>();

                for (var i = 0; i < width; i++)
                    toMerge.Add(lists[i]);

                mergedLists[startIndex++] = Merge(toMerge);

                for (var i = 0; i < width; i++)
                    lists.RemoveAt(0);

                toMerge = new List<List<int>>();

                for (var i = 0; i < width; i++)
                    toMerge.Add(lists[(lists.Count - 1) - i]);

                toMerge.Reverse();

                mergedLists[endIndex--] = Merge(toMerge);

                for (var i = 0; i < width; i++)
                    lists.RemoveLast();
            }

            if (lists.Any())
            {
                var middle = (int) Math.Ceiling(capacity/2.0) - 1;
                mergedLists[middle] = Merge(lists);
            }

            return new List<List<int>>(mergedLists);
        }

        public static List<List<int>> Merge_two_rows_starting_top_left_reversed(this List<List<int>> lists, int width)
        {
            var mergedLists = new List<List<int>>();

            while (lists.Count >= width)
            {
                var toMerge = new List<List<int>>();

                for (var i = 0; i < width; i++)
                    toMerge.Add(lists[i]);

                toMerge.Reverse();

                mergedLists.Add(Merge(toMerge));

                for (var i = 0; i < width; i++)
                    lists.RemoveAt(0);
            }

            if (lists.Any())
            {
                lists.Reverse();
                mergedLists.Add(Merge(lists));
            }

            return mergedLists;
        }

        public static List<List<int>> Merge_two_rows_starting_top_left_reversed_outside_in_with_zeros(
            this List<List<int>> lists,
            int width)
        {
            var capacity = (int) Math.Ceiling(lists.Count/(double) width);
            var mergedLists = new List<int>[capacity];
            int startIndex = 0, endIndex = capacity - 1;

            while (lists.Count >= width)
            {
                var toMerge = new List<List<int>>();

                for (var i = 0; i < width; i++)
                    toMerge.Add(lists[i]);

                toMerge.Reverse();

                mergedLists[startIndex++] = Merge(toMerge);

                for (var i = 0; i < width; i++)
                    lists.RemoveAt(0);

                if (lists.Count <= (width - 1))
                    break;

                toMerge = new List<List<int>>();

                for (var i = 0; i < width; i++)
                    toMerge.Add(lists[(lists.Count - 1) - i]);

                mergedLists[endIndex--] = Merge(toMerge);

                for (var i = 0; i < width; i++)
                    lists.RemoveLast();
            }

            if (lists.Any())
            {
                var middle = (int) Math.Ceiling(capacity/2.0) - 1;
                var zeros = lists.First().Select(s => 0).ToList();
                var toMerge = new List<List<int>>();

                toMerge.AddRange(lists);

                for (var i = 0; i < (width - lists.Count); i++)
                    toMerge.Add(zeros);

                mergedLists[middle] = Merge(toMerge);
            }

            return new List<List<int>>(mergedLists);
        }

        public static List<List<int>> Merge_two_rows_starting_top_left_reversed_outside_in(
            this List<List<int>> lists,
            int width)
        {
            var capacity = (int) Math.Ceiling(lists.Count/(double) width);
            var mergedLists = new List<int>[capacity];
            int startIndex = 0, endIndex = capacity - 1;

            while (lists.Count >= width)
            {
                var toMerge = new List<List<int>>();

                for (var i = 0; i < width; i++)
                    toMerge.Add(lists[i]);

                toMerge.Reverse();

                mergedLists[startIndex++] = Merge(toMerge);

                for (var i = 0; i < width; i++)
                    lists.RemoveAt(0);

                if (lists.Count <= (width - 1))
                    break;

                toMerge = new List<List<int>>();

                for (var i = 0; i < width; i++)
                    toMerge.Add(lists[(lists.Count - 1) - i]);

                mergedLists[endIndex--] = Merge(toMerge);

                for (var i = 0; i < width; i++)
                    lists.RemoveLast();
            }

            if (lists.Any())
            {
                var middle = (int) Math.Ceiling(capacity/2.0) - 1;
                mergedLists[middle] = Merge(lists);
            }

            return new List<List<int>>(mergedLists);
        }

        public static List<List<int>> Merge_two_rows_starting_bottom_left_with_zeros(
            this List<List<int>> lists,
            int width)
        {
            var mergedLists = new List<List<int>>();

            while (lists.Count >= width)
            {
                var toMerge = new List<List<int>>();

                for (var i = 0; i < width; i++)
                    toMerge.Add(lists[(lists.Count - 1) - i]);

                toMerge.Reverse();

                mergedLists.Add(Merge(toMerge));

                for (var i = 0; i < width; i++)
                    lists.RemoveLast();
            }

            if (lists.Any())
            {
                var zeros = lists.First().Select(s => 0).ToList();
                var toMerge = new List<List<int>>();

                for (var i = 0; i < (width - lists.Count); i++)
                    toMerge.Add(zeros);

                toMerge.AddRange(lists);

                mergedLists.Add(Merge(toMerge));
            }

            return mergedLists;
        }

        public static List<List<int>> Merge_two_rows_starting_bottom_left(this List<List<int>> lists, int width)
        {
            var mergedLists = new List<List<int>>();

            while (lists.Count >= width)
            {
                var toMerge = new List<List<int>>();

                for (var i = 0; i < width; i++)
                    toMerge.Add(lists[(lists.Count - 1) - i]);

                toMerge.Reverse();

                mergedLists.Add(Merge(toMerge));

                for (var i = 0; i < width; i++)
                    lists.RemoveLast();
            }

            if (lists.Any())
                mergedLists.Add(Merge(lists));

            return mergedLists;
        }

        public static List<List<int>> Merge_two_rows_starting_bottom_left_outside_in(
            this List<List<int>> lists,
            int width)
        {
            var mergedLists = new List<List<int>>();

            while (lists.Count >= width)
            {
                var toMerge = new List<List<int>>();

                for (var i = 0; i < width; i++)
                    toMerge.Add(lists[(lists.Count - 1) - i]);

                toMerge.Reverse();

                mergedLists.Add(Merge(toMerge));

                for (var i = 0; i < width; i++)
                    lists.RemoveLast();

                if (lists.Count <= (width - 1)) break;

                toMerge = new List<List<int>>();

                for (var i = 0; i < width; i++)
                    toMerge.Add(lists[i]);

                mergedLists.Add(Merge(toMerge));

                for (var i = 0; i < width; i++)
                    lists.RemoveAt(0);
            }

            if (lists.Any())
            {
                var middle = (int) Math.Ceiling(mergedLists.Count/2.0);
                mergedLists.Insert(middle, Merge(lists));
            }

            return mergedLists;
        }

        public static List<List<int>> Merge_two_rows_starting_bottom_left_outside_in_with_zeros(
            this List<List<int>> lists,
            int width)
        {
            var mergedLists = new List<List<int>>();

            while (lists.Count >= width)
            {
                var toMerge = new List<List<int>>();

                for (var i = 0; i < width; i++)
                    toMerge.Add(lists[(lists.Count - 1) - i]);

                toMerge.Reverse();

                mergedLists.Add(Merge(toMerge));

                for (var i = 0; i < width; i++)
                    lists.RemoveLast();

                if (lists.Count <= (width - 1)) break;

                toMerge = new List<List<int>>();

                for (var i = 0; i < width; i++)
                    toMerge.Add(lists[i]);

                mergedLists.Add(Merge(toMerge));

                for (var i = 0; i < width; i++)
                    lists.RemoveAt(0);
            }

            if (lists.Any())
            {
                var middle = (int) Math.Ceiling(mergedLists.Count/2.0);
                var zeros = lists.First().Select(s => 0).ToList();
                var toMerge = new List<List<int>>();

                for (var i = 0; i < (width - lists.Count); i++)
                    toMerge.Add(zeros);

                toMerge.AddRange(lists);

                mergedLists.Insert(middle, Merge(toMerge));
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
                var toMerge = new List<List<int>>();

                for (var i = 0; i < width; i++)
                    toMerge.Add(lists[(lists.Count - 1) - i]);

                mergedLists.Add(Merge(toMerge));

                for (var i = 0; i < width; i++)
                    lists.RemoveLast();
            }

            if (lists.Any())
                mergedLists.Add(Merge(lists));

            return mergedLists;
        }

        public static List<List<int>> Merge_two_rows_starting_bottom_left_reversed_outside_in(
            this List<List<int>> lists,
            int width)
        {
            var mergedLists = new List<List<int>>();

            while (lists.Count >= width)
            {
                var toMerge = new List<List<int>>();

                for (var i = 0; i < width; i++)
                    toMerge.Add(lists[(lists.Count - 1) - i]);

                mergedLists.Add(Merge(toMerge));

                for (var i = 0; i < width; i++)
                    lists.RemoveLast();

                toMerge = new List<List<int>>();

                for (var i = 0; i < width; i++)
                    toMerge.Add(lists[i]);

                toMerge.Reverse();

                mergedLists.Add(Merge(toMerge));

                for (var i = 0; i < width; i++)
                    lists.RemoveAt(0);
            }

            if (lists.Any())
            {
                var middle = (int) Math.Ceiling(mergedLists.Count/2.0);
                mergedLists.Insert(middle, Merge(lists));
            }

            return mergedLists;
        }

        public static List<List<int>> Merge_two_rows_starting_bottom_left_reversed_outside_in_with_zeros(
            this List<List<int>> lists,
            int width)
        {
            var mergedLists = new List<List<int>>();

            while (lists.Count >= width)
            {
                var toMerge = new List<List<int>>();

                for (var i = 0; i < width; i++)
                    toMerge.Add(lists[(lists.Count - 1) - i]);

                mergedLists.Add(Merge(toMerge));

                for (var i = 0; i < width; i++)
                    lists.RemoveLast();

                toMerge = new List<List<int>>();

                for (var i = 0; i < width; i++)
                    toMerge.Add(lists[i]);

                toMerge.Reverse();

                mergedLists.Add(Merge(toMerge));

                for (var i = 0; i < width; i++)
                    lists.RemoveAt(0);
            }

            if (lists.Any())
            {
                var middle = (int) Math.Ceiling(mergedLists.Count/2.0);
                var zeros = lists.First().Select(s => 0).ToList();
                var toMerge = new List<List<int>>();

                toMerge.AddRange(lists);

                for (var i = 0; i < (width - lists.Count); i++)
                    toMerge.Add(zeros);

                mergedLists.Insert(middle, Merge(toMerge));
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
                list.AddRange(lists.Select(l => l[i]));

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
    }
}