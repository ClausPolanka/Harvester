﻿using System;
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
        [TestCase(2, 5, "1 2 3 4 5 10 9 8 7 6")]
        [TestCase(5, 2, "1 2 4 3 5 6 8 7 9 10")]
        [TestCase(23, 12,
            "1 2 3 4 5 6 7 8 9 10 11 12 24 23 22 21 20 19 18 17 16 15 14 13 25 26 27 28 29 30 31 32 33 34 35 36 48 47 46 45 44 43 42 41 40 39 38 37 49 50 51 52 53 54 55 56 57 58 59 60 72 71 70 69 68 67 66 65 64 63 62 61 73 74 75 76 77 78 79 80 81 82 83 84 96 95 94 93 92 91 90 89 88 87 86 85 97 98 99 100 101 102 103 104 105 106 107 108 120 119 118 117 116 115 114 113 112 111 110 109 121 122 123 124 125 126 127 128 129 130 131 132 144 143 142 141 140 139 138 137 136 135 134 133 145 146 147 148 149 150 151 152 153 154 155 156 168 167 166 165 164 163 162 161 160 159 158 157 169 170 171 172 173 174 175 176 177 178 179 180 192 191 190 189 188 187 186 185 184 183 182 181 193 194 195 196 197 198 199 200 201 202 203 204 216 215 214 213 212 211 210 209 208 207 206 205 217 218 219 220 221 222 223 224 225 226 227 228 240 239 238 237 236 235 234 233 232 231 230 229 241 242 243 244 245 246 247 248 249 250 251 252 264 263 262 261 260 259 258 257 256 255 254 253 265 266 267 268 269 270 271 272 273 274 275 276"
            )]
        public void Level_1_spec_examples_also_level_1_inputs(int rows, int cols, string expected)
        {
            var actual = Harvest(rows, cols, startRow: 1, startCol: 1, direction: "");
            Assert.That(actual, Is.EqualTo(expected), "plot numbers");
        }

        [TestCase(2, 5, 2, 1, "6 7 8 9 10 5 4 3 2 1")]
        [TestCase(5, 2, 5, 2, "10 9 7 8 6 5 3 4 2 1")]
        [TestCase(23, 12, 1, 12,
            "12 11 10 9 8 7 6 5 4 3 2 1 13 14 15 16 17 18 19 20 21 22 23 24 36 35 34 33 32 31 30 29 28 27 26 25 37 38 39 40 41 42 43 44 45 46 47 48 60 59 58 57 56 55 54 53 52 51 50 49 61 62 63 64 65 66 67 68 69 70 71 72 84 83 82 81 80 79 78 77 76 75 74 73 85 86 87 88 89 90 91 92 93 94 95 96 108 107 106 105 104 103 102 101 100 99 98 97 109 110 111 112 113 114 115 116 117 118 119 120 132 131 130 129 128 127 126 125 124 123 122 121 133 134 135 136 137 138 139 140 141 142 143 144 156 155 154 153 152 151 150 149 148 147 146 145 157 158 159 160 161 162 163 164 165 166 167 168 180 179 178 177 176 175 174 173 172 171 170 169 181 182 183 184 185 186 187 188 189 190 191 192 204 203 202 201 200 199 198 197 196 195 194 193 205 206 207 208 209 210 211 212 213 214 215 216 228 227 226 225 224 223 222 221 220 219 218 217 229 230 231 232 233 234 235 236 237 238 239 240 252 251 250 249 248 247 246 245 244 243 242 241 253 254 255 256 257 258 259 260 261 262 263 264 276 275 274 273 272 271 270 269 268 267 266 265"
            )]
        public void Level_2_spec_examples(
            int rows,
            int cols,
            int startRow,
            int startCol,
            string expected)
        {
            var actual = Harvest(rows, cols, startRow, startCol, direction: "");
            Assert.That(actual, Is.EqualTo(expected), "plot numbers");
        }

        [TestCase(3, 4, 1, 1, "1 2 3 4 8 7 6 5 9 10 11 12")]
        [TestCase(2, 5, 2, 1, "6 7 8 9 10 5 4 3 2 1")]
        [TestCase(5, 2, 5, 2, "10 9 7 8 6 5 3 4 2 1")]
        [TestCase(23, 12, 1, 12,
          "12 11 10 9 8 7 6 5 4 3 2 1 13 14 15 16 17 18 19 20 21 22 23 24 36 35 34 33 32 31 30 29 28 27 26 25 37 38 39 40 41 42 43 44 45 46 47 48 60 59 58 57 56 55 54 53 52 51 50 49 61 62 63 64 65 66 67 68 69 70 71 72 84 83 82 81 80 79 78 77 76 75 74 73 85 86 87 88 89 90 91 92 93 94 95 96 108 107 106 105 104 103 102 101 100 99 98 97 109 110 111 112 113 114 115 116 117 118 119 120 132 131 130 129 128 127 126 125 124 123 122 121 133 134 135 136 137 138 139 140 141 142 143 144 156 155 154 153 152 151 150 149 148 147 146 145 157 158 159 160 161 162 163 164 165 166 167 168 180 179 178 177 176 175 174 173 172 171 170 169 181 182 183 184 185 186 187 188 189 190 191 192 204 203 202 201 200 199 198 197 196 195 194 193 205 206 207 208 209 210 211 212 213 214 215 216 228 227 226 225 224 223 222 221 220 219 218 217 229 230 231 232 233 234 235 236 237 238 239 240 252 251 250 249 248 247 246 245 244 243 242 241 253 254 255 256 257 258 259 260 261 262 263 264 276 275 274 273 272 271 270 269 268 267 266 265"
          )]
        public void Level_2_inputs(
            int rows,
            int cols,
            int startRow,
            int startCol,
            string expected)
        {
            var actual = Harvest(rows, cols, startRow, startCol, direction: "");
            Assert.That(actual, Is.EqualTo(expected), "plot numbers");
        }

        [TestCase(3, 4, 1, 1, "S", "1 5 9 10 6 2 3 7 11 12 8 4")]
        [TestCase(5, 2, 5, 2, "N", "10 8 6 4 2 1 3 5 7 9")]
        public void Level_3_spec_examples(
            int rows,
            int cols,
            int startRow,
            int startCol,
            string direction,
            string expected)
        {
            var actual = Harvest(rows, cols, startRow, startCol, direction);
            Assert.That(actual, Is.EqualTo(expected), "plot numbers");
        }

        private string Harvest(int rows, int cols, int startRow, int startCol, string direction)
        {
            var plotRows = CreatePlotrows(rows, cols, direction);

            if (direction == "N" || direction == "S")
            {
                var tmpRow = startRow;
                startRow = startCol;
                startCol = tmpRow;
            }

            if ((startRow%2 == 0) && startCol == plotRows[0].Count)
            {
                ReverseRowWithEvenRowIndex(plotRows);
                return ConvertPlotsToStringStartingAtSpecificRow(plotRows, startRow);
            }
            else if (startRow == 1 && startCol == plotRows[0].Count)
            {
                ReverseRowWithOddRowIndex(plotRows);
                return ConvertPlotsToString(plotRows);
            }
            else if (startRow == 1 && startCol == 1)
            {
                ReverseRowWithEvenRowIndex(plotRows);
                return ConvertPlotsToString(plotRows);
            }
            else
            {
                ReverseRowWithOddRowIndex(plotRows);
                return ConvertPlotsToStringStartingAtSpecificRow(plotRows, startRow);
            }
        }

        private static List<List<int>> CreatePlotrows(int rows, int cols, string direction)
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

            if (direction == "N" || direction == "S")
            {
                var newPlotRows = new List<List<int>>();

                for (var i = 0; i < cols; i++)
                    newPlotRows.Add(allRows.Select(pr => pr[i]).ToList());

                return newPlotRows;
            }

            return allRows;
        }

        private static void ReverseRowWithEvenRowIndex(List<List<int>> allRows)
        {
            for (var rowIndex = 0; rowIndex < allRows.Count; rowIndex++)
            {
                if (isEven(rowIndex))
                    allRows[rowIndex].Reverse();
            }
        }

        private static void ReverseRowWithOddRowIndex(List<List<int>> allRows)
        {
            for (var rowIndex = 0; rowIndex < allRows.Count; rowIndex++)
            {
                if (IsOdd(rowIndex))
                    allRows[rowIndex].Reverse();
            }
        }

        private static bool IsOdd(int rowIndex)
        {
            return rowIndex%2 == 0;
        }

        private static bool isEven(int i)
        {
            return i%2 == 1;
        }

        private static string ConvertPlotsToString(IEnumerable<List<int>> allRows)
        {
            var allRowsAsString = allRows.Select(r => string.Join(BLANK, r)).ToList();
            return string.Join(BLANK, allRowsAsString);
        }

        private static string ConvertPlotsToStringStartingAtSpecificRow(IEnumerable<List<int>> allRows, int startRow)
        {
            var allRowsAsString = allRows.Select(r => string.Join(BLANK, r)).ToList();

            allRowsAsString.Reverse();

            return string.Join(BLANK, allRowsAsString);
        }
    }
}