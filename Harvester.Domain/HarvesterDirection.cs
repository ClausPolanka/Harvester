namespace Harvester.Domain
{
    public interface HarvesterDirection
    {
        string Harvest(int startRow, int startCol);
    }
}