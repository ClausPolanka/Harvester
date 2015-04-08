using System.Collections.Generic;

namespace Harvester.Domain
{
    public class EastAndWestCircularPlotRowMergerWithZeros : EastAndWestPlotRowMerger
    {
        private readonly int width;

        public EastAndWestCircularPlotRowMergerWithZeros(int width)
        {
            this.width = width;
        }

        public List<List<int>> Merge(int startRow, List<List<int>> plotRows)
        {
            if (width == 1)
                return new List<List<int>>(plotRows);

            if (startRow == 1)
                return new List<List<int>>(plotRows.Merge_two_rows_starting_top_left_outside_in_with_zeros(width));
            else
                return new List<List<int>>(plotRows.Merge_two_rows_starting_bottom_left_outside_in_with_zeros(width));
        }
    }
}