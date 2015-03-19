using System;
using System.Collections.Generic;
using System.Linq;

namespace Harvester.Domain
{
    public class SerpentineHarvester : PlotHarvester
    {
        private readonly int rows;
        private readonly int cols;
        private string direction;
        private readonly int width;

        public SerpentineHarvester(int rows, int cols, string direction, int width)
        {
            this.rows = rows;
            this.cols = cols;
            this.direction = direction;
            this.width = width;
        }

        public string Harvest(int startRow, int startCol)
        {
            var plotRows = new PlotRowCreator().CreatePlotRows(rows, cols);

            if (direction == "S")
            {
                direction = startRow == 1 ? "O" : "W";
                var firstToLast = startCol == plotRows.First().Count;
                plotRows = Transpose(plotRows);
                startRow = firstToLast ? plotRows.Count : startRow;

            }

            if (direction == "N")
            {
                direction = startRow == plotRows.Count ? "W" : "O";
                plotRows = Transpose(plotRows);
                startRow = startCol == 1 ? 1 : plotRows.Count;
            }

            if (startRow == plotRows.Count)
                plotRows.Reverse();

            if (direction == "W")
                plotRows.Insert(0, Enumerable.Empty<int>().ToList());

            plotRows.ForEach(elem =>
            {
                var i = plotRows.IndexOf(elem);
                if (i%2 == 1)
                    plotRows[i].Reverse();
            });

            return string.Join(" ", plotRows.SelectMany(row => row));
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
    }
}