using System.Collections.Generic;

namespace Harvester.Domain
{
    public interface EastAndWestPlotRowMerger
    {
        List<List<int>> Merge(int startRow, List<List<int>> plotRows);
    }
}