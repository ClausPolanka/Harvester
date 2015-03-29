using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harvester.Domain
{
    public class CircularHarvester : GeneralPlotHarvester
    {
        public string Harvest(int startRow, List<List<int>> plotRows, string direction)
        {
            GeneralHarvester.Make_first_row_always_the_start_row(startRow, plotRows);
            
            var newPlotRows = ListExtensions.Make_first_and_last_row_successors(plotRows);
         
            GeneralHarvester.Reverse_necessary_plot_rows(newPlotRows, direction);
            return ListExtensions.JoinWithBlank(newPlotRows);
        }
    }
}
