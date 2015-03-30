using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harvester.Domain
{
    public class HarvesterLogic
    {
        public void Make_first_row_always_the_start_row(int startRow, List<List<int>> lists)
        {
            if (startRow == lists.Count)
                lists.Reverse();
        }

        public void Reverse_necessary_plot_rows(List<List<int>> list, string direction)
        {
            if (direction == "W")
                list.Insert(0, Enumerable.Empty<int>().ToList());

            list.ReverseEverySecondElementIn();
        }
    }
}
