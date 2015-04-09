using System;
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
        [TestCase(2, 5, 1, 1, "S", "2 1 7 6 9 10 4 5 3 8")]

        //       ↓  ↓
        // 1 2 3 4* 5
        // 6 7 8 9 10
        [TestCase(2, 5, 1, 4, "S", "5 4 10 9 6 7 1 2 3 8")]

        // 1  2 3 4  5
        // 6* 7 8 9 10
        // ↑  ↑
        [TestCase(2, 5, 1, 1, "N", "6 7 1 2 5 4 10 9 8 3")]

        // 1 2 3 4   5
        // 6 7 8 9* 10
        //       ↑   ↑
        [TestCase(2, 5, 1, 4, "N", "9 10 4 5 2 1 7 6 8 3")]
        
        //     1   2   3   4   5   6   7   8   9 
        //   *10  11  12  13  14  15  16  17  18
        //     ↑   ↑
        [TestCase(2, 9, 2, 1, "N", "10 11 1 2 9 8 18 17 12 13 3 4 7 6 16 15 14 5")]
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

        //  1   2  3  4       
        //  5   6  7  8        8  4  7  3  6  2  5  1              13 17 14 18 15 19 16 20
        //  9  10 11 12        0  9  0 10  0 11  0 12               8  4  7  3  6  2  5  1
        // 13* 14 15 16       13 17 14 18 15 19 16 20               0  9  0 10  0 11  0 12
        // 17  18 19 20
        [TestCase(5, 4, 4, 1, 2, "O", "13 17 14 18 15 19 16 20 8 4 7 3 6 2 5 1 9 0 10 0 11 0 12 0")]
        
        //  1   2  3  4 
        //  5   6  7  8       0  1  5  0  2  6  0  3  7  0  4  8
        //  9* 10 11 12       9 13 17 10 14 18 11 15 19 12 16 20
        // 13  14 15 16 
        // 17  18 19 20
        [TestCase(5, 4, 3, 1, 3, "O", "9 13 17 10 14 18 11 15 19 12 16 20 8 4 0 7 3 0 6 2 0 5 1 0")]
        public void With_Zerso(
            int nrOfRows,
            int nrOfCols,
            int startRow,
            int startCol,
            int width,
            string direction,
            string expected)
        {
            debug(nrOfRows, nrOfCols);
            var sut = new PlotHarvesterFactory(nrOfRows, nrOfCols, direction, width).CreateCircularWithZeros();

            var actual = sut.Harvest(startRow, startCol);

            Assert.That(actual, Is.EqualTo(expected), "plot numbers");
        }

        private static void debug(int rows, int cols)
        {
            for (int i = 1; i <= (rows*cols); i++)
            {
                Console.Out.Write(i + " ");

                if (i%cols == 0)
                    Console.Out.WriteLine("");
            }
        }

    }

}