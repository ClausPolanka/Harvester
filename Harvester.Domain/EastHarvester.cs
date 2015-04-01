using System.Linq;

namespace Harvester.Domain
{
    public class EastHarvester : HarvesterDirection
    {
        private readonly int rows;
        private readonly int cols;
        private HarvesterMode harvesterMode;
        private EastAndWestPlotRowMerger plotRowMerger;

        public EastHarvester(int rows, int cols, HarvesterMode harvesterMode, EastAndWestPlotRowMerger plotRowMerger)
        {
            this.rows = rows;
            this.cols = cols;
            this.harvesterMode = harvesterMode;
            this.plotRowMerger = plotRowMerger;
        }

        public string Harvest(int startRow, int startCol)
        {
            var plotRows = new PlotRowCreator().CreatePlotRows(rows, cols);
            plotRows = plotRowMerger.Merge(startRow, plotRows);
            return harvesterMode.Harvest(startRow, plotRows, direction: "O");
        }
    }
}