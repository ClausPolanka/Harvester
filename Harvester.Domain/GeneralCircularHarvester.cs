using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harvester.Domain
{
    public class GeneralCircularHarvester
    {
        public string Harvest(int startRow, List<List<int>> plotRows, string direction)
        {
            Make_first_row_always_the_start_row(startRow, plotRows);
            var newPlotRows = ListExtensions.Make_first_and_last_row_successors(plotRows);
            Reverse_necessary_plot_rows(newPlotRows, direction);
            return ListExtensions.JoinWithBlank(newPlotRows);
        }

        private void Make_first_row_always_the_start_row(int startRow, List<List<int>> lists)
        {
            if (startRow == lists.Count)
                lists.Reverse();
        }
        
        private void Reverse_necessary_plot_rows(List<List<int>> list, string direction)
        {
            if (direction == "W")
                list.Insert(0, Enumerable.Empty<int>().ToList());

            ListExtensions.ReverseEverySecondElementIn(list);
        }
    }
}
