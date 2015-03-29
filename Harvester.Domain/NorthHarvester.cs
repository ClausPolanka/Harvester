using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harvester.Domain
{
    public class NorthHarvester : PlotHarvester
    {
        private readonly int rows;
        private readonly int cols;
        private readonly int width;
        private GeneralPlotHarvester generalHarvester;

        public NorthHarvester(int rows, int cols, int width, GeneralPlotHarvester generalHarvester)
        {
            this.rows = rows;
            this.cols = cols;
            this.width = width;
            this.generalHarvester = generalHarvester;
        }

        public string Harvest(int startRow, int startCol)
        {
            var plotRows = new PlotRowCreator().CreateTransposedPlotRows(rows, cols);
            var newStartRow = startCol == 1 ? 1 : cols;
            return generalHarvester.Harvest(newStartRow, plotRows, direction: "W");
        }

    }
}