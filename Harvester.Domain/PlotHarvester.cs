namespace Harvester.Domain
{
    public interface PlotHarvester
    {
        string Harvest(int startRow, int startCol);
    }
}