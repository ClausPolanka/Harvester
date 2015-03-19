using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Harvester.Domain.Test
{
    [TestFixture]
    public class SerpentineHarvesterUnitTest
    {
        [TestCase]
        public void One_row_one_col_east_width_of_1()
        {
            var sut = new SerpentineHarvester(rows: 1, cols: 1, direction: "O", width: 1);

            var actual = sut.Harvest(startRow: 1, startCol: 1);

            Assert.That(actual, Is.EqualTo("0"), "plot numbers");
        }

        [TestCase]
        public void One_row_two_cols_east_width_of_1()
        {
            var sut = new SerpentineHarvester(rows: 1, cols: 2, direction: "O", width: 1);

            var actual = sut.Harvest(startRow: 1, startCol: 1);

            Assert.That(actual, Is.EqualTo("0 1"), "plot numbers");
        }

        [TestCase]
        public void Two_rows_one_col_east_width_of_1()
        {
            var sut = new SerpentineHarvester(rows: 2, cols: 1, direction: "O", width: 1);

            var actual = sut.Harvest(startRow: 1, startCol: 1);

            Assert.That(actual, Is.EqualTo("0 1"), "plot numbers");
        }

        [TestCase]
        public void Three_rows_one_col_east_width_of_1()
        {
            var sut = new SerpentineHarvester(rows: 3, cols: 1, direction: "O", width: 1);

            var actual = sut.Harvest(startRow: 1, startCol: 1);

            Assert.That(actual, Is.EqualTo("0 1 2"), "plot numbers");
        }

        [TestCase]
        public void Two_rows_two_cols_east_width_of_1()
        {
            var sut = new SerpentineHarvester(rows: 2, cols: 2, direction: "O", width: 1);

            var actual = sut.Harvest(startRow: 1, startCol: 1);

            Assert.That(actual, Is.EqualTo("0 1 3 2"), "plot numbers");
        }

        [TestCase]
        public void Two_rows_two_cols_west_width_of_1()
        {
            var sut = new SerpentineHarvester(rows: 2, cols: 2, direction: "W", width: 1);

            var actual = sut.Harvest(startRow: 1, startCol: 2);

            Assert.That(actual, Is.EqualTo("1 0 2 3"), "plot numbers");
        }

        [TestCase]
        public void Four_rows_two_cols_east_width_of_1()
        {
            var sut = new SerpentineHarvester(rows: 4, cols: 2, direction: "O", width: 1);

            var actual = sut.Harvest(startRow: 1, startCol: 1);

            Assert.That(actual, Is.EqualTo("0 1 3 2 4 5 7 6"), "plot numbers");
        }

        [TestCase]
        public void Four_rows_two_cols_east_width_of_1_starting_in_last_row_first_plot()
        {
            var sut = new SerpentineHarvester(rows: 4, cols: 2, direction: "O", width: 1);

            var actual = sut.Harvest(startRow: 4, startCol: 1);

            Assert.That(actual, Is.EqualTo("6 7 5 4 2 3 1 0"), "plot numbers");
        }

    }
}