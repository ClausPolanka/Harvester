using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harvester.Domain
{
    public class CircularSouthHarvester : PlotHarvester
    {
        private readonly int rows;
        private readonly int cols;
        private readonly int width;

        public CircularSouthHarvester(int rows, int cols, int width)
        {
            this.rows = rows;
            this.cols = cols;
            this.width = width;
        }

        public string Harvest(int startRow, int startCol)
        {
            var plotRows = new PlotRowCreator().CreateTransposedPlotRows(rows, cols);
            var newStartRow = startCol == cols ? cols : startRow;
            return new GeneralCircularHarvester().Harvest(newStartRow, plotRows, direction: "O");
        }
    }
}