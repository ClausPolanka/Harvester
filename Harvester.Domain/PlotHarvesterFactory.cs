namespace Harvester.Domain
{
    public class PlotHarvesterFactory
    {
        public const string SERPENTINE = "S";
        public const string CIRCULAR = "Z";
        
        private const string SOUTH = "S";
        private const string EAST = "O";
        private const string NORTH = "N";

        private readonly int rows;
        private readonly int cols;
        private readonly string direction;

        public PlotHarvesterFactory(int rows, int cols, string direction = EAST)
        {
            this.rows = rows;
            this.cols = cols;
            this.direction = direction;
        }

        public HarvesterDirection Create(string mode = SERPENTINE)
        {
            if (mode == CIRCULAR)
                return CreateCircular(rows, cols, direction);
            else
                return CreateSerpentine(rows, cols, direction);

        }

        public HarvesterDirection CreateCircular(string mode = SERPENTINE)
        {
            return CreateCircular(rows, cols, direction);
        }

        public HarvesterDirection CreateCircular(int nrOfRows, int nrOfCols, string direction)
        {
            if (direction == SOUTH)
            {
                return new SouthHarvester(
                    nrOfRows, 
                    nrOfCols, 
                    width: 1, 
                    harvesterMode: new CircularHarvester());
                
            }
            if (direction == NORTH)
            {
                return new NorthHarvester(
                    nrOfRows, 
                    nrOfCols, 
                    width: 1, 
                    generalHarvester: new CircularHarvester());
                
            }

            return new EastAndWestHarvester(
                nrOfRows, 
                nrOfCols, 
                direction, 
                width: 1, 
                harvesterMode: new CircularHarvester());
        }

        public HarvesterDirection CreateSerpentine(int nrOfRows, int nrOfCols, string direction)
        {
            if (direction == SOUTH)
            {
                return new SouthHarvester(
                    nrOfRows, 
                    nrOfCols, 
                    width: 1,
                    harvesterMode: new SerpentineHarvester());
            }
            if (direction == NORTH)
            {
                return new NorthHarvester(
                    nrOfRows, 
                    nrOfCols, 
                    width: 1,
                    generalHarvester: new SerpentineHarvester());
            }

            return new EastAndWestHarvester(
                nrOfRows, 
                nrOfCols, 
                direction, 
                width: 1, 
                harvesterMode: new SerpentineHarvester());
        }
    }
}