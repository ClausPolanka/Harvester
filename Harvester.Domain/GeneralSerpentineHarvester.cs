using System.Collections.Generic;

namespace Harvester.Domain
{
    public class GeneralSerpentineHarvester : GeneralPlotHarvester
    {
        public string Harvest(int startRow, List<List<int>> plotRows, string direction)
        {
            GeneralHarvester.Make_first_row_always_the_start_row(startRow, plotRows);
            GeneralHarvester.Reverse_necessary_plot_rows(plotRows, direction);
            return ListExtensions.JoinWithBlank(plotRows);
        }
    }
}