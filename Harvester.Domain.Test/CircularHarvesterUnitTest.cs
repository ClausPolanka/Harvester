﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Harvester.Domain.Test
{
    [TestFixture]
    public class CircularHarvesterUnitTest
    {
        [TestCase]
        public void One_row_one_col_east_width_of_1()
        {
            var sut = new CircularHarvester(rows: 1, cols: 1, direction: "O", width: 1);

            var actual = sut.Harvest(startRow: 1, startCol: 1);

            Assert.That(actual, Is.EqualTo("0"), "plot numbers");
        }

        [TestCase]
        public void One_row_two_cols_east_width_of_1()
        {
            var sut = new CircularHarvester(rows: 1, cols: 2, direction: "O", width: 1);

            var actual = sut.Harvest(startRow: 1, startCol: 1);

            Assert.That(actual, Is.EqualTo("0 1"), "plot numbers");
        }

        [TestCase]
        public void Two_rows_one_col_east_width_of_1()
        {
            var sut = new CircularHarvester(rows: 2, cols: 1, direction: "O", width: 1);

            var actual = sut.Harvest(startRow: 1, startCol: 1);

            Assert.That(actual, Is.EqualTo("0 1"), "plot numbers");
        }

        [TestCase]
        public void Three_rows_one_col_east_width_of_1()
        {
            var sut = new CircularHarvester(rows: 3, cols: 1, direction: "O", width: 1);

            var actual = sut.Harvest(startRow: 1, startCol: 1);

            Assert.That(actual, Is.EqualTo("0 1 2"), "plot numbers");
        }

        [TestCase]
        public void Two_rows_two_cols_east_width_of_1()
        {
            var sut = new CircularHarvester(rows: 2, cols: 2, direction: "O", width: 1);

            var actual = sut.Harvest(startRow: 1, startCol: 1);

            Assert.That(actual, Is.EqualTo("0 1 3 2"), "plot numbers");
        }

    }
}