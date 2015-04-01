namespace Harvester.Domain
{
    public class PlotHarvesterFactory
    {
        public const string SERPENTINE = "S";
        public const string CIRCULAR = "Z";
        
        private const string SOUTH = "S";
        private const string EAST = "O";
        private const string WEST = "W";
        private const string NORTH = "N";

        private readonly int rows;
        private readonly int cols;
        private readonly string direction;
        private readonly int width;

        public PlotHarvesterFactory(int rows, int cols, string direction = EAST, int width = 1)
        {
            this.rows = rows;
            this.cols = cols;
            this.direction = direction;
            this.width = width;
        }

        public HarvesterDirection Create(string mode = SERPENTINE)
        {
            if (mode == CIRCULAR)
                return CreateCircular(rows, cols, direction);
            else
                return CreateSerpentine(rows, cols, direction);

        }

        public HarvesterDirection CreateCircular()
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
                    width, 
                    harvesterMode: new CircularHarvester());
                
            }
            if (direction == NORTH)
            {
                return new NorthHarvester(
                    nrOfRows, 
                    nrOfCols, 
                    width, 
                    generalHarvester: new CircularHarvester());
                
            }
            if (direction == WEST)
            {
                return new WestHarvester(
                    nrOfRows, 
                    nrOfCols, 
                    harvesterMode: new CircularHarvester(), 
                    plotRowMerger: new EastAndWestCircularPlotRowMerger(width));
                
            }
            return new EastHarvester(
                nrOfRows, 
                nrOfCols, 
                harvesterMode: new CircularHarvester(), 
                plotRowMerger: new EastAndWestCircularPlotRowMerger(width));
        }

        public HarvesterDirection CreateSerpentine(int nrOfRows, int nrOfCols, string direction)
        {
            if (direction == SOUTH)
            {
                return new SouthHarvester(
                    nrOfRows, 
                    nrOfCols, 
                    width,
                    harvesterMode: new SerpentineHarvester());
            }
            if (direction == NORTH)
            {
                return new NorthHarvester(
                    nrOfRows, 
                    nrOfCols, 
                    width,
                    generalHarvester: new SerpentineHarvester());
            }
            if (direction == WEST)
            {
                return new WestHarvester(
                    nrOfRows, 
                    nrOfCols,
                    harvesterMode: new SerpentineHarvester(),
                    plotRowMerger: new EastAndWestSerpentinePlotRowMerger(width));
            }
            return new EastHarvester(
                nrOfRows,
                nrOfCols,
                harvesterMode: new SerpentineHarvester(), 
                plotRowMerger: new EastAndWestSerpentinePlotRowMerger(width));
            
        }
    }
}