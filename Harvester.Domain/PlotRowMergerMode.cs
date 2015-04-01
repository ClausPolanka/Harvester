using System.Collections.Generic;

namespace Harvester.Domain
{
    public interface PlotRowMergerMode
    {
        List<List<int>> Merge(int startCol, List<List<int>> plotRows);
    }
}