using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harvester.Domain
{
    public class CircularHarvesterAll
    {
        public string Harvest(int newStartRow, List<List<int>> transposed, string direction)
        {
            ReversePlotRowsIfNecessary(newStartRow, transposed);
            var reordered = ReorderToFitGeneral(transposed);
            ReverseNecessaryRows(reordered, direction);
            return ListExtensions.JoinWithBlank(reordered);
        }

        public void ReversePlotRowsIfNecessary(int startRow, List<List<int>> list)
        {
            if (startRow == list.Count)
                list.Reverse();
        }

        public List<List<int>> ReorderToFitGeneral(List<List<int>> list)
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

        public void ReverseNecessaryRows(List<List<int>> list, string direction)
        {
            if (direction == "W")
                list.Insert(0, Enumerable.Empty<int>().ToList());

            ListExtensions.ReverseEverySecondElementIn(list);
        }
    }
}
