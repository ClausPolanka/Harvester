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
            plotRows = new PlotRowMerger(width).Merge(startRow, plotRows);
            return harvesterMode.Harvest(startRow, plotRows, "W");
        }
    }
}