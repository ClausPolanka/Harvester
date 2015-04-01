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
        private HarvesterMode harvesterMode;
        private PlotRowMergerMode plotRowMerger;

        public SouthHarvester(int rows, int cols, HarvesterMode harvesterMode, PlotRowMergerMode plotRowMerger)
        {
            this.rows = rows;
            this.cols = cols;
            this.harvesterMode = harvesterMode;
            this.plotRowMerger = plotRowMerger;
        }

        public string Harvest(int startRow, int startCol)
        {
            var plotRows = new PlotRowCreator().CreateTransposedPlotRows(rows, cols);
            var newStartRow = startCol == cols ? cols : startRow;
            plotRows = plotRowMerger.Merge(startCol, plotRows);
            return harvesterMode.Harvest(newStartRow, plotRows, direction: "O");
        }
    }
}