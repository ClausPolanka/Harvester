using System.Collections.Generic;
using System.Linq;

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

    public class SerpentineHarvester : PlotHarvester
    {
        private readonly int rows;
        private readonly int cols;
        private string direction;
        private readonly int width;

        public SerpentineHarvester(int rows, int cols, string direction, int width)
        {
            this.rows = rows;
            this.cols = cols;
            this.direction = direction;
            this.width = width;
        }

        public string Harvest(int startRow, int startCol)
        {
            var plotRows = new PlotRowCreator().CreatePlotRows(rows, cols);
            return new GeneralSerpentineHarvester().Harvest(startRow, plotRows, direction);
        }

        private void ReverseNecessaryRows(int startRow, List<List<int>> list)
        {
            if (startRow == list.Count)
                list.Reverse();

            if (direction == "W")
                list.Insert(0, Enumerable.Empty<int>().ToList());

            ListExtensions.ReverseEverySecondElementIn(list);
        }
    }
}