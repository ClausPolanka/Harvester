using NUnit.Framework;

namespace Harvester.Domain.UnitTest
{
    [TestFixture]
    public class SerpentineHarvesterUnitTest
    {
        [Test]
        public void One_row_one_col_east_width_of_1()
        {
            // → 1*

            var sut = new PlotHarvesterFactory(rows: 1, cols: 1).Create();

            var actual = sut.Harvest(startRow: 1, startCol: 1);

            Assert.That(actual, Is.EqualTo("1"), "plot numbers");
        }

        [Test]
        public void One_row_two_cols_east_width_of_1()
        {
            // → 1* 2

            var sut = new PlotHarvesterFactory(rows: 1, cols: 2).Create();

            var actual = sut.Harvest(startRow: 1, startCol: 1);

            Assert.That(actual, Is.EqualTo("1 2"), "plot numbers");
        }

        [Test]
        public void Two_rows_one_col_east_width_of_1()
        {
            // → 1*
            //   2

            var sut = new PlotHarvesterFactory(rows: 2, cols: 1).Create();

            var actual = sut.Harvest(startRow: 1, startCol: 1);

            Assert.That(actual, Is.EqualTo("1 2"), "plot numbers");
        }

        [Test]
        public void Three_rows_one_col_east_width_of_1()
        {
            // → 1*
            //   2
            //   3

            var sut = new PlotHarvesterFactory(rows: 3, cols: 1).Create();

            var actual = sut.Harvest(startRow: 1, startCol: 1);

            Assert.That(actual, Is.EqualTo("1 2 3"), "plot numbers");
        }

        [Test]
        public void Two_rows_two_cols_east_width_of_1()
        {
            // → 1* 2
            //   3  4

            var sut = new PlotHarvesterFactory(rows: 2, cols: 2).Create();

            var actual = sut.Harvest(startRow: 1, startCol: 1);

            Assert.That(actual, Is.EqualTo("1 2 4 3"), "plot numbers");
        }

        [Test]
        public void Two_rows_two_cols_west_width_of_1()
        {
            // 1 2* ←
            // 3 4

            var sut = new PlotHarvesterFactory(rows: 2, cols: 2, direction: "W").Create();

            var actual = sut.Harvest(startRow: 1, startCol: 2);

            Assert.That(actual, Is.EqualTo("2 1 3 4"), "plot numbers");
        }

        [Test]
        public void Four_rows_two_cols_east_width_of_1()
        {
            // → 1* 2
            //   3  4
            //   5  6
            //   7  8

            var sut = new PlotHarvesterFactory(rows: 4, cols: 2).Create();

            var actual = sut.Harvest(startRow: 1, startCol: 1);

            Assert.That(actual, Is.EqualTo("1 2 4 3 5 6 8 7"), "plot numbers");
        }

        [Test]
        public void Four_rows_two_cols_east_width_of_1_starting_in_last_row_first_plot()
        {
            //   1  2
            //   3  4
            //   5  6
            // → 7* 8

            var sut = new PlotHarvesterFactory(rows: 4, cols: 2).Create();

            var actual = sut.Harvest(startRow: 4, startCol: 1);

            Assert.That(actual, Is.EqualTo("7 8 6 5 3 4 2 1"), "plot numbers");
        }

        [Test]
        public void Four_rows_two_cols_west_width_of_1_starting_in_last_row_last_plot()
        {
            // 1  2
            // 3  4
            // 5  6
            // 7  8* ← 

            var sut = new PlotHarvesterFactory(rows: 4, cols: 2, direction: "W").Create();

            var actual = sut.Harvest(startRow: 4, startCol: 2);

            Assert.That(actual, Is.EqualTo("8 7 5 6 4 3 1 2"), "plot numbers");
        }

        [TestCase(1, 1, "1 3 4 2")]
        [TestCase(1, 2, "2 4 3 1")]
        public void Two_rows_two_cols_direction_south(int startRow, int startCol, string expected)
        {
            // ↓  ↓
            // 1* 2*
            // 3  4

            var sut = new PlotHarvesterFactory(rows: 2, cols: 2, direction: "S").Create();

            var actual = sut.Harvest(startRow, startCol);

            Assert.That(actual, Is.EqualTo(expected), "plot numbers");
        }

        [TestCase(2, 1, "3 1 2 4")]
        [TestCase(2, 2, "4 2 1 3")]
        public void Two_rows_two_cols_direction_north(int startRow, int startCol, string expected)
        {
            // 1  2
            // 3* 4*
            // ↑  ↑

            var sut = new PlotHarvesterFactory(rows: 2, cols: 2, direction: "N").Create();

            var actual = sut.Harvest(startRow, startCol);

            Assert.That(actual, Is.EqualTo(expected), "plot numbers");
        }

        [TestCase(1, 5, "5 10 9 4 3 8 7 2 1 6")]
        public void Two_rows_five_cols_direction_south(int startRow, int startCol, string expected)
        {
            //              ↓
            // 1  2  3  4   5*
            // 6  7  8  9  10

            var sut = new PlotHarvesterFactory(rows: 2, cols: 5, direction: "S").Create();

            var actual = sut.Harvest(startRow, startCol);

            Assert.That(actual, Is.EqualTo(expected), "plot numbers");
        }

        [TestCase(5, 2, "10 8 6 4 2 1 3 5 7 9")]
        public void Five_rows_two_cols_direction_north(int startRow, int startCol, string expected)
        {
            // 1   2
            // 3   4
            // 5   6
            // 7   8
            // 9  10*
            //     ↑ 

            var sut = new PlotHarvesterFactory(rows: 5, cols: 2, direction: "N").Create();

            var actual = sut.Harvest(startRow, startCol);

            Assert.That(actual, Is.EqualTo(expected), "plot numbers");
        }

        // → 1*  2
        // → 3   4
        //   5   6
        //   7   8
        [TestCase(4, 2, 1, 1, "1 3 2 4 8 6 7 5")]
        public void Even_number_of_rows_and_harvester_width_of_two_going_east(
            int rows, 
            int cols, 
            int startRow, 
            int startCol, 
            string expected)
        {
            var sut = new PlotHarvesterFactory(rows, cols, width: 2).Create();

            var actual = sut.Harvest(startRow, startCol);

            Assert.That(actual, Is.EqualTo(expected), "plot numbers");
        }

        [TestCase(1, 1, "1 3 2 4 8 6 7 5 9 10")]
        public void Odd_number_of_rows_and_harvester_width_of_two_going_east(int startRow, int startCol, string expected)
        {
            // → 1*  2
            // → 3   4
            //   5   6
            //   7   8
            //   9  10

            var sut = new PlotHarvesterFactory(rows: 5, cols: 2, width: 2).Create();

            var actual = sut.Harvest(startRow, startCol);

            Assert.That(actual, Is.EqualTo(expected), "plot numbers");
        }

        [TestCase(4, 1, "7 9 8 10 6 4 5 3 1 2")]
        public void Odd_number_of_rows_and_harvester_width_of_two_going_east_starting_second_to_last_row(
            int startRow, 
            int startCol, 
            string expected)
        {
            //    1   2
            //    3   4
            //    5   6
            //  → 7*  8
            // →  9  10

            var sut = new PlotHarvesterFactory(rows: 5, cols: 2, width: 2).Create();

            var actual = sut.Harvest(startRow, startCol);

            Assert.That(actual, Is.EqualTo(expected), "plot numbers");
        }

        [TestCase(1, 2, "4 2 3 1 5 7 6 8 10 9")]
        public void Odd_number_of_rows_and_harvester_width_of_two_going_west(int startRow, int startCol, string expected)
        {
            // 1  2*  ← 
            // 3  4  ←
            // 5  6
            // 7  8
            // 9 10

            var sut = new PlotHarvesterFactory(rows: 5, cols: 2, width: 2, direction: "W").Create();

            var actual = sut.Harvest(startRow, startCol);

            Assert.That(actual, Is.EqualTo(expected), "plot numbers");
        }

        [TestCase(4, 2, "10 8 9 7 3 5 4 6 2 1")]
        public void Odd_number_of_rows_and_harvester_width_of_two_going_west_starting_in_last_row(
            int startRow, 
            int startCol, 
            string expected)
        {
            // 1  2
            // 3  4
            // 5  6
            // 7  8*  ←
            // 9 10  ←

            var sut = new PlotHarvesterFactory(rows: 5, cols: 2, width: 2, direction: "W").Create();

            var actual = sut.Harvest(startRow, startCol);

            Assert.That(actual, Is.EqualTo(expected), "plot numbers");
        }

        // 1  2
        // 3  4
        // 5  6*  ←
        // 7  8  ←
        [TestCase(4, 2, 4, 2, "8 6 7 5 1 3 2 4")]
        //  1  2  3  4
        //  5  6  7  8
        //  9 10 11 12   ←
        // 13 14 15 16* ←
        [TestCase(4, 4, 4, 4, "16 12 15 11 14 10 13 9 1 5 2 6 3 7 4 8")]
        public void Even_number_of_rows_and_harvester_width_of_two_going_west_starting_in_last_row(
            int rows, 
            int cols, 
            int startRow, 
            int startCol, 
            string expected)
        {
            var sut = new PlotHarvesterFactory(rows, cols, width: 2, direction: "W").Create();

            var actual = sut.Harvest(startRow, startCol);

            Assert.That(actual, Is.EqualTo(expected), "plot numbers");
        }

        
        [TestCase(1, 1, "2 1 7 6 8 9 3 4 5 10")]
        public void Odd_number_of_cols_and_harvester_width_of_two_going_south_starting_top_left(
            int startRow, 
            int startCol, 
            string expected)
        {
            // ↓  ↓
            // 1* 2  3  4  5
            // 6  7  8  9 10

            var sut = new PlotHarvesterFactory(rows: 2, cols: 5, direction: "S", width: 2).Create();

            var actual = sut.Harvest(startRow, startCol);

            Assert.That(actual, Is.EqualTo(expected), "plot numbers");
        }

        [TestCase(1, 4, "5 4 10 9 7 8 2 3 1 6")]
        public void Odd_number_of_cols_and_harvester_width_of_two_going_south_starting_top_right(
            int startRow, 
            int startCol, 
            string expected)
        {
            //          ↓   ↓
            // 1  2  3  4*  5
            // 6  7  8  9  10
            
            var sut = new PlotHarvesterFactory(rows: 2, cols: 5, direction: "S", width: 2).Create();

            var actual = sut.Harvest(startRow, startCol);

            Assert.That(actual, Is.EqualTo(expected), "plot numbers");
        }
        
        [TestCase(2, 1, "6 7 1 2 4 3 9 8 10 5")]
        public void Odd_number_of_cols_and_harvester_width_of_two_going_north_starting_bottom_left(
            int startRow, 
            int startCol, 
            string expected)
        {
            // 1  2  3  4  5
            // 6* 7  8  9 10
            // ↑  ↑

            var sut = new PlotHarvesterFactory(rows: 2, cols: 5, direction: "N", width: 2).Create();

            var actual = sut.Harvest(startRow, startCol);

            Assert.That(actual, Is.EqualTo(expected), "plot numbers");
        }

        [TestCase(2, 4, "9 10 4 5 3 2 8 7 6 1")]
        public void Odd_number_of_cols_and_harvester_width_of_two_going_north_starting_bottom_second_to_end(
            int startRow, 
            int startCol, 
            string expected)
        {
            // 1  2  3  4   5
            // 6  7  8  9* 10
            //          ↑   ↑
            
            var sut = new PlotHarvesterFactory(rows: 2, cols: 5, direction: "N", width: 2).Create();

            var actual = sut.Harvest(startRow, startCol);

            Assert.That(actual, Is.EqualTo(expected), "plot numbers");
        }
    }
}