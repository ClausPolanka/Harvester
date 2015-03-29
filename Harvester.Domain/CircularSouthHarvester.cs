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
        private List<List<int>> plotRows;
        private CircularHarvesterAll all = new CircularHarvesterAll();

        public CircularSouthHarvester(int rows, int cols, int width)
        {
            this.rows = rows;
            this.cols = cols;
            this.width = width;
            this.direction = "O";
            plotRows = new PlotRowCreator().CreatePlotRows(rows, cols);
        }

        public string Harvest(int startRow, int startCol)
        {
            var transposed = ListExtensions.Transpose(plotRows);
            var newStartRow = IsLast(startCol) ? transposed.Count : startRow;
            return all.Harvest(newStartRow, transposed, direction);
        }

        private bool IsLast(int startCol)
        {
            return startCol == plotRows.First().Count;
        }
    }
}