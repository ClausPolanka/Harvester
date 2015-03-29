using System.Collections.Generic;
using System.Linq;

namespace Harvester.Domain
{
    public class CircularEastAndWestHarvester : PlotHarvester
    {
        private readonly int rows;
        private readonly int cols;
        private string direction;
        private readonly int width;

        public CircularEastAndWestHarvester(int rows, int cols, string direction, int width)
        {
            this.rows = rows;
            this.cols = cols;
            this.direction = direction;
            this.width = width;
        }

        public string Harvest(int startRow, int startCol)
        {
            var plotRows = new PlotRowCreator().CreatePlotRows(rows, cols);
            return new CircularHarvester().Harvest(startRow, plotRows, direction);
        }
    }
}