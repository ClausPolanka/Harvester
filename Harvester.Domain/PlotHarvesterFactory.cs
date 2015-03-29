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
                sut = CreateCircular(rows, cols, direction);
            else
                sut = new SerpentineHarvester(rows, cols, direction, width: 1);
            return sut;
        }

        public PlotHarvester CreateCircular(string mode = SERPENTINE)
        {
            return CreateCircular(rows, cols, direction);
        }

        public PlotHarvester CreateCircular(int nrOfRows, int nrOfCols, string direction)
        {
            PlotHarvester sut;
            if (direction == "S")
                sut = new CircularSouthHarvester(nrOfRows, nrOfCols, direction, width: 1);
            else if (direction == "N")
            {
                sut = new CircularNorthHarvester(nrOfRows, nrOfCols, direction, width: 1);
            }
            else if (direction == "O")
                sut = new CircularHarvester(nrOfRows, nrOfCols, direction, width: 1);
            else
                sut = new CircularHarvester(nrOfRows, nrOfCols, direction, width: 1);
            return sut;
        }
    }
}