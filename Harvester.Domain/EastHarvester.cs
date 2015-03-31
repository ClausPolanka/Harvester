using System.Collections.Generic;
using System.Linq;

namespace Harvester.Domain
{
    public class EastHarvester : HarvesterDirection
    {
        private readonly int rows;
        private readonly int cols;
        private readonly int width;
        private HarvesterMode harvesterMode;

        public EastHarvester(int rows, int cols, int width, HarvesterMode harvesterMode)
        {
            this.rows = rows;
            this.cols = cols;
            this.width = width;
            this.harvesterMode = harvesterMode;
        }

        public string Harvest(int startRow, int startCol)
        {
            var plotRows = new PlotRowCreator().CreatePlotRows(rows, cols);

            if (width == 2)
            {
                if (startRow == 1)
                {
                    plotRows = plotRows.Merge_two_rows_starting_top_left();
                }
                else
                {
                    plotRows = plotRows.Merge_two_rows_starting_bottom_left();
                }
            }

            return harvesterMode.Harvest(startRow, plotRows, "O");
        }
    }
}