namespace Harvester.Domain
{
    public class PlotHarvesterFactory
    {
        public const string SERPENTINE = "S";
        public const string CIRCULAR = "Z";

        private readonly int rows;
        private readonly int cols;
        private readonly string direction;

        public PlotHarvesterFactory(int rows, int cols, string direction = "O")
        {
            this.rows = rows;
            this.cols = cols;
            this.direction = direction;
        }

        public PlotHarvester Create(string mode = SERPENTINE)
        {
            PlotHarvester sut;
            if (mode == CIRCULAR)
                sut = new CircularHarvester(rows, cols, direction, width: 1);
            else
                sut = new SerpentineHarvester(rows, cols, direction, width: 1);
            return sut;
        }
    }
}