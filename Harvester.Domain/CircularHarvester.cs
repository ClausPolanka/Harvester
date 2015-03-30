using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harvester.Domain
{
    public class CircularHarvester : HarvesterMode
    {
        private HarvesterLogic logic = new HarvesterLogic();

        public string Harvest(int startRow, List<List<int>> plotRows, string direction)
        {
            logic.Make_first_row_always_the_start_row(startRow, plotRows);
            var newPlotRows = plotRows.Make_first_and_last_row_successors();
            logic.Reverse_necessary_plot_rows(newPlotRows, direction);
            return newPlotRows.JoinWithBlank();
        }
    }
}
