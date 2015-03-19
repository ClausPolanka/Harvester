using System;
using System.Linq;

namespace Harvester.Domain
{
    public class SerpentineHarvester : PlotHarvester
    {
        private readonly int rows;
        private readonly int cols;
        private readonly string direction;
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
    }
}