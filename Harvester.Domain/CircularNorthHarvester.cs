using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harvester.Domain
{
    public class CircularNorthHarvester : PlotHarvester
    {
        private readonly int rows;
        private readonly int cols;
        private readonly int width;

        public CircularNorthHarvester(int rows, int cols, int width)
        {
            this.rows = rows;
            this.cols = cols;
            this.width = width;
        }

        public string Harvest(int startRow, int startCol)
        {
            var plotRows = new PlotRowCreator().CreateTransposedPlotRows(rows, cols);
            var newStartRow = startCol == 1 ? 1 : cols;
            return new GeneralCircularHarvester().Harvest(newStartRow, plotRows, direction: "W");
        }

    }
}