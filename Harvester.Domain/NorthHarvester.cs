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
        private readonly int width;
        private HarvesterMode generalHarvester;

        public NorthHarvester(int rows, int cols, int width, HarvesterMode generalHarvester)
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

            if (width == 2)
            {
                if (startCol == 1)
                    plotRows = plotRows.Merge_two_rows_starting_top_left_reversed();
                else
                    plotRows = plotRows.Merge_two_rows_starting_bottom_left_reversed();
            }

            return generalHarvester.Harvest(newStartRow, plotRows, direction: "W");
        }

    }
}