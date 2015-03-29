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
            var newPlotRows = Make_first_and_last_row_successors(plotRows);
            Reverse_necessary_plot_rows(newPlotRows, direction);
            return ListExtensions.JoinWithBlank(newPlotRows);
        }

        private void Make_first_row_always_the_start_row(int startRow, List<List<int>> list)
        {
            if (startRow == list.Count)
                list.Reverse();
        }

        private List<List<int>> Make_first_and_last_row_successors(List<List<int>> list)
        {
            var reordered = new List<List<int>>();

            while (list.Any())
            {
                reordered.Add(list.First().ToList());
                list.RemoveAt(list.IndexOf(list.First()));

                if (list.Count > 1)
                {
                    reordered.Add(list.Last().ToList());
                    list.RemoveAt(list.IndexOf(list.Last()));
                }
            }

            return reordered;
        }

        private void Reverse_necessary_plot_rows(List<List<int>> list, string direction)
        {
            if (direction == "W")
                list.Insert(0, Enumerable.Empty<int>().ToList());

            ListExtensions.ReverseEverySecondElementIn(list);
        }
    }
}
