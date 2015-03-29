using System.Collections.Generic;
using System.Linq;

namespace Harvester.Domain
{
    public class CircularHarvester : PlotHarvester
    {
        private readonly int rows;
        private readonly int cols;
        private string direction;
        private readonly int width;
        private List<List<int>> plotRows;

        public CircularHarvester(int rows, int cols, string direction, int width)
        {
            this.rows = rows;
            this.cols = cols;
            this.direction = direction;
            this.width = width;
            plotRows = new PlotRowCreator().CreatePlotRows(rows, cols);
        }

        public string Harvest(int startRow, int startCol)
        {
            if (startRow == plotRows.Count)
                plotRows.Reverse();

            var reordered = new List<List<int>>();

            while (plotRows.Any())
            {
                reordered.Add(plotRows.First().ToList());
                plotRows.RemoveAt(plotRows.IndexOf(plotRows.First()));

                if (plotRows.Count > 1)
                {
                    reordered.Add(plotRows.Last().ToList());
                    plotRows.RemoveAt(plotRows.IndexOf(plotRows.Last()));
                }
            }

            ReverseNecessaryRows(reordered);
            return ListExtensions.JoinWithBlank(reordered);
        }

        private void ReverseNecessaryRows(List<List<int>> list)
        {
            if (direction == "W")
                list.Insert(0, Enumerable.Empty<int>().ToList());

            ListExtensions.ReverseEverySecondElementIn(list);
        }
    }
}