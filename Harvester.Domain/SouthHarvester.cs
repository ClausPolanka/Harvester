using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harvester.Domain
{
    public class SouthHarvester : PlotHarvester
    {
        private readonly int rows;
        private readonly int cols;
        private readonly int width;
        private GeneralPlotHarvester generalHarvester;

        public SouthHarvester(int rows, int cols, int width, GeneralPlotHarvester generalHarvester)
        {
            this.rows = rows;
            this.cols = cols;
            this.width = width;
            this.generalHarvester = generalHarvester;
        }

        public string Harvest(int startRow, int startCol)
        {
            var plotRows = new PlotRowCreator().CreateTransposedPlotRows(rows, cols);
            var newStartRow = startCol == cols ? cols : startRow;
            return generalHarvester.Harvest(newStartRow, plotRows, direction: "O");
        }
    }
}