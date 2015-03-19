using System;
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

            if (plotRows.Count > 1)
            {
                plotRows.ForEach(elem =>
                {
                    var i = plotRows.IndexOf(elem);
                    if (i%2 == 1)
                        plotRows[i].Reverse();
                });
            }

            return string.Join(" ", plotRows.SelectMany(row => row));
        }
    }
}