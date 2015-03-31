using System.Collections.Generic;
using System.Linq;

namespace Harvester.Domain
{
    public class EastAndWestHarvester : HarvesterDirection
    {
        private readonly int rows;
        private readonly int cols;
        private string direction;
        private readonly int width;
        private HarvesterMode harvesterMode;

        public EastAndWestHarvester(int rows, int cols, string direction, int width, HarvesterMode harvesterMode)
        {
            this.rows = rows;
            this.cols = cols;
            this.direction = direction;
            this.width = width;
            this.harvesterMode = harvesterMode;
        }

        public string Harvest(int startRow, int startCol)
        {
            var plotRows = new PlotRowCreator().CreatePlotRows(rows, cols);
            
            if (width == 2 && direction == "O")
            {
                plotRows = plotRows.Merge_two_rows_starting_left();
            }
            else if (width == 2 && direction == "W")
            {
                plotRows.ForEach(row => row.Reverse());
                plotRows = plotRows.Merge_two_rows_starting_left();
                direction = "O";
            }
            
            return harvesterMode.Harvest(startRow, plotRows, direction);
        }
    }
}