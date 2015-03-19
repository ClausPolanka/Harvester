using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Harvester.Domain.Test
{
    [TestFixture]
    public class HarvesterTest
    {
        private const string BLANK = " ";
        private const string SERPENTINE = "S";
        private const string CIRCULAR = "Z";
        private const string NORTH = "N";
        private const string OST = "O";
        private const string SOUTH = "S";
        private const string WEST = "W";

        [TestCase(5, 4, 1, 1, OST, SERPENTINE, 2, "1 5 2 6 3 7 4 8 16 12 15 11 14 10 13 9 17 18 19 20")]
        [TestCase(5, 4, 4, 1, OST, CIRCULAR, 2, "13 17 14 18 15 19 16 20 8 4 7 3 6 2 5 1 9 10 11 12")]
        public void Level_5_spec_examples(
            int rows,
            int cols,
            int startRow,
            int startCol,
            string direction,
            string mode,
            int width,
            string expected)
        {
            var actual = Harvest(rows, cols, startRow, startCol, direction, mode, width);
            Assert.That(actual, Is.EqualTo(expected), "plot numbers");
        }

        private string Harvest(int rows, int cols, int startRow, int startCol, string direction, string mode, int width)
        {
            throw new NotImplementedException();
        }
    }
}