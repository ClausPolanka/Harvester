using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harvester.Domain
{
    public class SouthHarvester : HarvesterDirection
    {
        private readonly int rows;
        private readonly int cols;
        private readonly int width;
        private HarvesterMode harvesterMode;

        public SouthHarvester(int rows, int cols, int width, HarvesterMode harvesterMode)
        {
            this.rows = rows;
            this.cols = cols;
            this.width = width;
            this.harvesterMode = harvesterMode;
        }

        public string Harvest(int startRow, int startCol)
        {
            var plotRows = new PlotRowCreator().CreateTransposedPlotRows(rows, cols);
            var newStartRow = startCol == cols ? cols : startRow;
            plotRows = new NorthAndSouthPlotRowMerger(width).Merge(startCol, plotRows);
            return harvesterMode.Harvest(newStartRow, plotRows, direction: "O");
        }
    }
}