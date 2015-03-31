using System.Collections.Generic;
using System.Linq;

namespace Harvester.Domain
{
    public class WestHarvester : HarvesterDirection
    {
        private readonly int rows;
        private readonly int cols;
        private readonly int width;
        private HarvesterMode harvesterMode;

        public WestHarvester(int rows, int cols, int width, HarvesterMode harvesterMode)
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
                plotRows = MergePlotRows(startRow, plotRows);

            return harvesterMode.Harvest(startRow, plotRows, "W");
        }

        private static List<List<int>> MergePlotRows(int startRow, List<List<int>> plotRows)
        {
            if (startRow == 1)
                plotRows = plotRows.Merge_two_rows_starting_top_left();
            else
                plotRows = plotRows.Merge_two_rows_starting_bottom_left();

            return plotRows;
        }
    }
}