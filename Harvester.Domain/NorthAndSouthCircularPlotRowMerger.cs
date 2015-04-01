using System.Collections.Generic;

namespace Harvester.Domain
{
 
    public class NorthAndSouthCircularPlotRowMerger : PlotRowMergerMode
    {
        private readonly int width;

        public NorthAndSouthCircularPlotRowMerger(int width)
        {
            this.width = width;
        }

        public List<List<int>> Merge(int startCol, List<List<int>> plotRows)
        {
            if (width == 1)
                return new List<List<int>>(plotRows);

            if (startCol == 1)
                return new List<List<int>>(plotRows.Merge_two_rows_starting_top_left_reversed_outside_in());
            else
                return new List<List<int>>(plotRows.Merge_two_rows_starting_bottom_left_reversed_outside_in());
        }
    }
}