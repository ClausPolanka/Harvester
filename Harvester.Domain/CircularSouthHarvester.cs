using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harvester.Domain
{
    public class CircularSouthHarvester : PlotHarvester
    {
        private readonly int rows;
        private readonly int cols;
        private string direction;
        private readonly int width;
        private CircularHarvesterAll all = new CircularHarvesterAll();

        public CircularSouthHarvester(int rows, int cols, int width)
        {
            this.rows = rows;
            this.cols = cols;
            this.width = width;
            this.direction = "O";
        }

        public string Harvest(int startRow, int startCol)
        {
            var plotRows = new PlotRowCreator().CreatePlotRows(rows, cols);
            var transposed = ListExtensions.Transpose(plotRows);
            var newStartRow = startCol == plotRows.First().Count ? transposed.Count : startRow;
            return all.Harvest(newStartRow, transposed, direction);
        }
    }
}