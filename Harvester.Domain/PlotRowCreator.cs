using System.Collections.Generic;
using System.Linq;

namespace Harvester.Domain
{
    public class PlotRowCreator
    {
        public List<List<int>> CreatePlotRows(int rows, int cols)
        {
            var plots = new List<int>();

            for (var i = 0; i < rows*cols; i++)
                plots.Add(i+1);

            var plotRows = new List<List<int>>();

            while (plots.Any())
            {
                plotRows.Add(plots.Take(cols).ToList());
                plots = plots.Skip(cols).ToList();
            }

            return plotRows;
        }
    }
}