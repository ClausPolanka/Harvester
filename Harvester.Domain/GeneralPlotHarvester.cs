using System.Collections.Generic;

namespace Harvester.Domain
{
    public interface GeneralPlotHarvester
    {
        string Harvest(int startRow, List<List<int>> plotRows, string direction);
    }
}