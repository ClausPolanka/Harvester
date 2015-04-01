using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Harvester.Domain.UnitTest
{
    [TestFixture]
    public class CircularHarvesterUnitTest
    {
        // → 1*  2  3
        //   4   5  6
        //   7   8  9
        //  10  11 12
        [TestCase(4, 3, 1, 1, "O", "1 2 3 12 11 10 4 5 6 9 8 7")]
        //   1  2  3* ←
        //   4  5  6
        //   7  8  9
        //  10 11 12
        [TestCase(4, 3, 1, 3, "W", "3 2 1 10 11 12 6 5 4 7 8 9")]
        //    1  2  3
        //    4  5  6
        //    7  8  9
        // → 10* 11 12
        [TestCase(4, 3, 4, 1, "O", "10 11 12 3 2 1 7 8 9 6 5 4")]
        //   1  2  3
        //   4  5  6
        //   7  8  9
        //  10 11 12* ←
        [TestCase(4, 3, 4, 3, "W", "12 11 10 1 2 3 9 8 7 4 5 6")]
        public void Even_number_of_rows_east_and_west(
            int nrOfRows,
            int nrOfCols,
            int startRow,
            int startCol,
            string direction,
            string expected)
        {
            var sut = new PlotHarvesterFactory(nrOfRows, nrOfCols, direction).CreateCircular();

            var actual = sut.Harvest(startRow, startCol);

            Assert.That(actual, Is.EqualTo(expected), "plot numbers");
        }

        // → 1*  2  3
        //   4   5  6
        //   7   8  9
        //  10  11 12
        //  13  14 15
        [TestCase(5, 3, 1, 1, "O", "1 2 3 15 14 13 4 5 6 12 11 10 7 8 9")]
        //   1  2  3* ←
        //   4  5  6
        //   7  8  9
        //  10 11 12
        //  13  14 15
        [TestCase(5, 3, 1, 3, "W", "3 2 1 13 14 15 6 5 4 10 11 12 9 8 7")]
        //    1  2  3
        //    4  5  6
        //    7  8  9
        //   10  11 12
        // → 13* 14 15
        [TestCase(5, 3, 5, 1, "O", "13 14 15 3 2 1 10 11 12 6 5 4 7 8 9")]
        //   1  2  3
        //   4  5  6
        //   7  8  9
        //  10 11 12
        //  13 14 15* ←
        [TestCase(5, 3, 5, 3, "W", "15 14 13 1 2 3 12 11 10 4 5 6 9 8 7")]
        public void Odd_number_of_rows_east_and_west(
            int nrOfRows,
            int nrOfCols,
            int startRow,
            int startCol,
            string direction,
            string expected)
        {
            var sut = new PlotHarvesterFactory(nrOfRows, nrOfCols, direction).CreateCircular();

            var actual = sut.Harvest(startRow, startCol);

            Assert.That(actual, Is.EqualTo(expected), "plot numbers");
        }

        //  ↓
        //  1*  2  3
        //  4   5  6
        //  7   8  9
        // 10  11 12
        // 13  14 15
        [TestCase(5, 3, 1, 1, "S", "1 4 7 10 13 15 12 9 6 3 2 5 8 11 14")]
        //        ↓
        //  1  2  3*
        //  4  5  6
        //  7  8  9
        // 10 11 12
        // 13 14 15
        [TestCase(5, 3, 1, 3, "S", "3 6 9 12 15 13 10 7 4 1 2 5 8 11 14")]
        //  1   2  3
        //  4   5  6
        //  7   8  9
        // 10  11 12
        // 13* 14 15
        //  ↑
        [TestCase(5, 3, 5, 1, "N", "13 10 7 4 1 3 6 9 12 15 14 11 8 5 2")]
        //  1  2  3
        //  4  5  6
        //  7  8  9
        // 10 11 12
        // 13 14 15*
        //        ↑
        [TestCase(5, 3, 5, 5, "N", "15 12 9 6 3 1 4 7 10 13 14 11 8 5 2")]
        public void Odd_number_of_rows_north_and_south(
            int nrOfRows,
            int nrOfCols,
            int startRow,
            int startCol,
            string direction,
            string expected)
        {
            var sut = new PlotHarvesterFactory(nrOfRows, nrOfCols, direction).CreateCircular();

            var actual = sut.Harvest(startRow, startCol);

            Assert.That(actual, Is.EqualTo(expected), "plot numbers");
        }

        

        //  → 1*  2  3
        // →  4   5  6                   
        //    7   8  9
        //   10  11 12
        //   13  14 15
        [TestCase(5, 3, 1, 1, "O", "1 4 2 5 3 6 15 12 14 11 13 10 7 8 9")]
        //   1  2  3* ←
        //   4  5  6 ←
        //   7  8  9
        //  10 11 12
        //  13 14 15
        [TestCase(5, 3, 1, 3, "W", "6 3 5 2 4 1 10 13 11 14 12 15 9 8 7")]
        //     1  2  3
        //     4  5  6
        //     7  8  9
        //  → 10  11 12
        // →  13* 14 15
        [TestCase(5, 3, 5, 1, "O", "10 13 11 14 12 15 6 3 5 2 4 1 7 8 9")]
        //   1  2  3  
        //   4  5  6  
        //   7  8  9  
        //  10 11 12   ←
        //  13 14 15* ←
        [TestCase(5, 3, 5, 3, "W", "15 12 14 11 13 10 1 4 2 5 3 6 9 8 7")]
        public void Odd_number_of_rows_east_and_west_and_width_of_two(
            int nrOfRows,
            int nrOfCols,
            int startRow,
            int startCol,
            string direction,
            string expected)
        {
            var sut = new PlotHarvesterFactory(nrOfRows, nrOfCols, direction, width: 2).CreateCircular();

            var actual = sut.Harvest(startRow, startCol);

            Assert.That(actual, Is.EqualTo(expected), "plot numbers");
        }

        // ↓  ↓
        // 1* 2 3 4 5
        // 6  7 8 9 10
        [TestCase(5, 3, 1, 1, "S", "2 1 7 6 9 10 4 5 3 8")]
        
        public void Odd_number_of_cols_north_and_south_and_a_width_of_two(
            int nrOfRows,
            int nrOfCols,
            int startRow,
            int startCol,
            string direction,
            string expected)
        {
            var sut = new PlotHarvesterFactory(nrOfRows, nrOfCols, direction, width: 2).CreateCircular();

            var actual = sut.Harvest(startRow, startCol);

            Assert.That(actual, Is.EqualTo(expected), "plot numbers");
        }
    }

}