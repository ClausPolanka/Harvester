using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harvester.Domain
{
    public class NorthHarvester : HarvesterDirection
    {
        private readonly int rows;
        private readonly int cols;
        private HarvesterMode generalHarvester;
        private PlotRowMergerMode plotRowMerger;

        public NorthHarvester(int rows, int cols, HarvesterMode generalHarvester, PlotRowMergerMode plotRowMerger)
        {
            this.rows = rows;
            this.cols = cols;
            this.generalHarvester = generalHarvester;
            this.plotRowMerger = plotRowMerger;
        }

        public string Harvest(int startRow, int startCol)
        {
            var plotRows = new PlotRowCreator().CreateTransposedPlotRows(rows, cols);
            var newStartRow = startCol == 1 ? 1 : cols;
            plotRows = plotRowMerger.Merge(startCol, plotRows);
            return generalHarvester.Harvest(newStartRow, plotRows, direction: "W");
        }

    }
}