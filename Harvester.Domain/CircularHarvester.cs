using System.Collections.Generic;
using System.Linq;

namespace Harvester.Domain
{
    public class CircularHarvester : PlotHarvester
    {
        private readonly int rows;
        private readonly int cols;
        private readonly string direction;
        private readonly int width;

        public CircularHarvester(int rows, int cols, string direction, int width)
        {
            this.rows = rows;
            this.cols = cols;
            this.direction = direction;
            this.width = width;
        }

        public string Harvest(int startRow, int startCol)
        {
            var plotRows = new PlotRowCreator().CreatePlotRows(rows, cols);

            var reordered = new List<List<int>>();

            while (plotRows.Any())
            {
                reordered.Add(plotRows.First().ToList());
                reordered.Add(plotRows.Last().ToList());
                plotRows.RemoveAt(plotRows.IndexOf(plotRows.First()));
                plotRows.RemoveAt(plotRows.IndexOf(plotRows.Last()));
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