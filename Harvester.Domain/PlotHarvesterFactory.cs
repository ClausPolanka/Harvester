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
                sut = CreateSerpentine(rows, cols, direction);
            return sut;
        }

        public PlotHarvester CreateCircular(string mode = SERPENTINE)
        {
            return CreateCircular(rows, cols, direction);
        }

        public PlotHarvester CreateCircular(int nrOfRows, int nrOfCols, string direction)
        {
            if (direction == "S")
                return new SouthHarvester(nrOfRows, nrOfCols, width: 1, generalHarvester: new CircularHarvester());
            if (direction == "N")
                return new NorthHarvester(nrOfRows, nrOfCols, width: 1, generalHarvester: new CircularHarvester());

            return new CircularEastAndWestHarvester(nrOfRows, nrOfCols, direction, width: 1);
        }

        public PlotHarvester CreateSerpentine(int nrOfRows, int nrOfCols, string direction)
        {
            if (direction == "S")
            {
                return new SouthHarvester(nrOfRows, nrOfCols, width: 1,
                    generalHarvester: new GeneralSerpentineHarvester());
            }
            if (direction == "N")
            {
                return new NorthHarvester(nrOfRows, nrOfCols, width: 1,
                    generalHarvester: new GeneralSerpentineHarvester());
            }

            return new SerpentineHarvester(nrOfRows, nrOfCols, direction, width: 1);
        }
    }
}