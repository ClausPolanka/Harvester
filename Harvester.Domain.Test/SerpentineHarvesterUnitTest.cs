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
        [Test]
        public void One_row_one_col_east_width_of_1()
        {
            var sut = new SerpentineHarvester(rows: 1, cols: 1, direction: "O", width: 1);

            var actual = sut.Harvest(startRow: 1, startCol: 1);

            Assert.That(actual, Is.EqualTo("1"), "plot numbers");
        }

        [Test]
        public void One_row_two_cols_east_width_of_1()
        {
            var sut = new SerpentineHarvester(rows: 1, cols: 2, direction: "O", width: 1);

            var actual = sut.Harvest(startRow: 1, startCol: 1);

            Assert.That(actual, Is.EqualTo("1 2"), "plot numbers");
        }

        [Test]
        public void Two_rows_one_col_east_width_of_1()
        {
            var sut = new SerpentineHarvester(rows: 2, cols: 1, direction: "O", width: 1);

            var actual = sut.Harvest(startRow: 1, startCol: 1);

            Assert.That(actual, Is.EqualTo("1 2"), "plot numbers");
        }

        [Test]
        public void Three_rows_one_col_east_width_of_1()
        {
            var sut = new SerpentineHarvester(rows: 3, cols: 1, direction: "O", width: 1);

            var actual = sut.Harvest(startRow: 1, startCol: 1);

            Assert.That(actual, Is.EqualTo("1 2 3"), "plot numbers");
        }

        [Test]
        public void Two_rows_two_cols_east_width_of_1()
        {
            var sut = new SerpentineHarvester(rows: 2, cols: 2, direction: "O", width: 1);

            var actual = sut.Harvest(startRow: 1, startCol: 1);

            Assert.That(actual, Is.EqualTo("1 2 4 3"), "plot numbers");
        }

        [Test]
        public void Two_rows_two_cols_west_width_of_1()
        {
            var sut = new SerpentineHarvester(rows: 2, cols: 2, direction: "W", width: 1);

            var actual = sut.Harvest(startRow: 1, startCol: 2);

            Assert.That(actual, Is.EqualTo("2 1 3 4"), "plot numbers");
        }

        [Test]
        public void Four_rows_two_cols_east_width_of_1()
        {
            var sut = new SerpentineHarvester(rows: 4, cols: 2, direction: "O", width: 1);

            var actual = sut.Harvest(startRow: 1, startCol: 1);

            Assert.That(actual, Is.EqualTo("1 2 4 3 5 6 8 7"), "plot numbers");
        }

        [Test]
        public void Four_rows_two_cols_east_width_of_1_starting_in_last_row_first_plot()
        {
            var sut = new SerpentineHarvester(rows: 4, cols: 2, direction: "O", width: 1);

            var actual = sut.Harvest(startRow: 4, startCol: 1);

            Assert.That(actual, Is.EqualTo("7 8 6 5 3 4 2 1"), "plot numbers");
        }

        [Test]
        public void Four_rows_two_cols_west_width_of_1_starting_in_last_row_last_plot()
        {
            var sut = new SerpentineHarvester(rows: 4, cols: 2, direction: "W", width: 1);

            var actual = sut.Harvest(startRow: 4, startCol: 2);

            Assert.That(actual, Is.EqualTo("8 7 5 6 4 3 1 2"), "plot numbers");
        }

        [TestCase(1, 1, "1 3 4 2")]
        [TestCase(1, 2, "2 4 3 1")]
        public void Two_rows_two_cols_direction_south(int startRow, int startCol, string expected)
        {
            var sut = new SerpentineHarvester(rows: 2, cols: 2, direction: "S", width: 1);

            var actual = sut.Harvest(startRow, startCol);

            Assert.That(actual, Is.EqualTo(expected), "plot numbers");
        }

        [TestCase(2, 1, "3 1 2 4")]
        [TestCase(2, 2, "4 2 1 3")]
        public void Two_rows_two_cols_direction_north(int startRow, int startCol, string expected)
        {
            var sut = new SerpentineHarvester(rows: 2, cols: 2, direction: "N", width: 1);

            var actual = sut.Harvest(startRow, startCol);

            Assert.That(actual, Is.EqualTo(expected), "plot numbers");
        }

        [TestCase(1, 5, "5 10 9 4 3 8 7 2 1 6")]
        public void Two_rows_five_cols_direction_south(int startRow, int startCol, string expected)
        {
            var sut = new SerpentineHarvester(rows: 2, cols: 5, direction: "S", width: 1);

            var actual = sut.Harvest(startRow, startCol);

            Assert.That(actual, Is.EqualTo(expected), "plot numbers");
        }

        [TestCase(5, 2, "10 8 6 4 2 1 3 5 7 9")]
        public void Five_rows_two_cols_direction_south(int startRow, int startCol, string expected)
        {
            var sut = new SerpentineHarvester(rows: 5, cols: 2, direction: "N", width: 1);

            var actual = sut.Harvest(startRow, startCol);

            Assert.That(actual, Is.EqualTo(expected), "plot numbers");
        }
        
    }
}