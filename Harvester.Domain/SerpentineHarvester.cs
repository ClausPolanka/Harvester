using System.Collections.Generic;

namespace Harvester.Domain
{
    public class SerpentineHarvester : HarvesterMode
    {
        private HarvesterLogic logic = new HarvesterLogic();

        public string Harvest(int startRow, List<List<int>> plotRows, string direction)
        {
            logic.Make_first_row_always_the_start_row(startRow, plotRows);
            logic.Reverse_necessary_plot_rows(plotRows, direction);
            return plotRows.JoinWithBlank();
        }
    }
}