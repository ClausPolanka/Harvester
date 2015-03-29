﻿using NUnit.Framework;

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
        [TestCase(4, 3, 1, 1, "O", "10 11 12 3 2 1 7 8 9 6 5 4")]
        //   1  2  3
        //   4  5  6
        //   7  8  9
        //  10 11 12* ←
        //[TestCase(4, 3, 1, 1, "W", "3 2 1 10 11 12 6 5 4 7 8 9")]
        public void Even_number_of_rows(
            int nrOfRows,
            int nrOfCols,
            int startRow,
            int startCol,
            string direction,
            string expected)
        {
            var sut = new CircularHarvester(nrOfRows, nrOfCols, direction, width: 1);

            var actual = sut.Harvest(startRow, startCol);

            Assert.That(actual, Is.EqualTo(expected), "plot numbers");
        }
    }
}