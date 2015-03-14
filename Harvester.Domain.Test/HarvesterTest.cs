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

        [TestCase(3, 4, "1 2 3 4 8 7 6 5 9 10 11 12")]
        [TestCase(5, 2, "1 2 4 3 5 6 8 7 9 10")]
        [TestCase(2, 5, "1 2 3 4 5 10 9 8 7 6")]
        [TestCase(23, 12, "")]
        public void Level_1_spec_examples(int rows, int cols, string expected)
        {
            var actual = Harvest(rows, cols);
            Assert.That(actual, Is.EqualTo(expected), "plot numbers");
        }

        private static string Harvest(int rows, int cols)
        {
            var allRows = new List<List<int>>();

            var inc = 0;
            for (var i = 0; i < rows; i++)
            {
                var row = new List<int>();

                for (var plot = 1 + inc; plot <= cols + inc; plot++)
                    row.Add(plot);

                allRows.Add(row);
                inc += cols;
            }

            ReverseEverySecondRow(allRows);
            return ConvertPlotsToString(allRows);
        }

        private static void ReverseEverySecondRow(List<List<int>> allRows)
        {
            for (var rowIndex = 0; rowIndex < allRows.Count; rowIndex++)
            {
                if (isMultipleOfTwo(rowIndex))
                    allRows[rowIndex].Reverse();
            }
        }

        private static bool isMultipleOfTwo(int i)
        {
            return i%2 == 1;
        }

        private static string ConvertPlotsToString(IEnumerable<List<int>> allRows)
        {
            var allRowsAsString = allRows.Select(r => string.Join(BLANK, r)).ToList();
            return string.Join(BLANK, allRowsAsString);
        }
    }
}