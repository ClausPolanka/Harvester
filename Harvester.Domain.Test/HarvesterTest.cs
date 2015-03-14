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
        [TestCase(3, 4, "1 2 3 4 8 7 6 5 9 10 11 12")]
        public void Level_1_spec_examples(int rows, int cols, string expected)
        {
            var actual = Harvest(rows, cols);
            Assert.That(actual, Is.EqualTo(expected), "plot numbers");
        }

        private string Harvest(int rows, int cols)
        {
            return "";
        }
    }
}
