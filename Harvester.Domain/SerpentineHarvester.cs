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
                plotRows = ListExtensions.Transpose(plotRows);
                startRow = firstToLast ? plotRows.Count : startRow;

            }

            if (direction == "N")
            {
                direction = startRow == plotRows.Count ? "W" : "O";
                plotRows = ListExtensions.Transpose(plotRows);
                startRow = startCol == 1 ? 1 : plotRows.Count;
            }

            ReverseNecessaryRows(startRow, plotRows);
            return ListExtensions.JoinWithBlank(plotRows);
        }

        private void ReverseNecessaryRows(int startRow, List<List<int>> list)
        {
            if (startRow == list.Count)
                list.Reverse();

            if (direction == "W")
                list.Insert(0, Enumerable.Empty<int>().ToList());

            ListExtensions.ReverseEverySecondElementIn(list);
        }
    }
}