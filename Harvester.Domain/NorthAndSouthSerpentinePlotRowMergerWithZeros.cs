using System.Collections.Generic;

namespace Harvester.Domain
{
    public class NorthAndSouthSerpentinePlotRowMergerWithZeros : PlotRowMergerMode
    {
        private readonly int width;

        public NorthAndSouthSerpentinePlotRowMergerWithZeros(int width)
        {
            this.width = width;
        }

        public List<List<int>> Merge(int startCol, List<List<int>> plotRows)
        {
            if (width == 1)
                return new List<List<int>>(plotRows);

            if (startCol == 1)
                return new List<List<int>>(plotRows.Merge_two_rows_starting_top_left_reversed(width));
            else
                return new List<List<int>>(plotRows.Merge_two_rows_starting_bottom_left_reversed(width));
        }
    }
}