using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SodukuLib;

namespace UnitTestSodoku
{
    [TestClass]
    public class UnitTest_Sodoku
    {
        [TestMethod]
        public void Test_Sodoku_Init()
        {
            Sodoku sodoku = new Sodoku();
            Assert.AreEqual(0, sodoku.GetColumnAt(0).ContentCount);
            Assert.AreEqual(0, sodoku.GetColumnAt(8).ContentCount);

            Assert.AreEqual(0, sodoku.GetSodoku3x3At(0, 0).ContentCount);
            Assert.AreEqual(0, sodoku.GetSodoku3x3At(1, 2).ContentCount);
            Assert.AreEqual(0, sodoku.GetSodoku3x3At(2, 1).ContentCount);
            Assert.AreEqual(0, sodoku.GetSodoku3x3At(2, 2).ContentCount);

        }

        [TestMethod]
        public void Test_Sodoku_Grid()
        {
            Sodoku sodoku = new Sodoku();

            // top left
            sodoku[2, 2] = new CellContent(9);
            int rank = sodoku[2, 2].Rank;
            Assert.AreEqual(9, sodoku[2, 2].Rank);
            SodokuItem row = sodoku.GetRowAt(2);
            Assert.AreEqual(9, row[2].Rank);
            SodokuItem col = sodoku.GetColumnAt(2);
            Assert.AreEqual(9, col[2].Rank);
            Sodoku3x3 sgrid = sodoku.GetSodoku3x3AtGridPosition(2, 2);
            Assert.AreEqual(9, sgrid[2, 2].Rank);

            // bottom right
            sodoku[7, 8] = new CellContent(1);
            rank = sodoku[7, 8].Rank;
            Assert.AreEqual(rank, sodoku[7, 8].Rank);
            row = sodoku.GetRowAt(8);
            Assert.AreEqual(rank, row[7].Rank);
            col = sodoku.GetColumnAt(7);
            Assert.AreEqual(rank, col[8].Rank);
            sgrid = sodoku.GetSodoku3x3AtGridPosition(7, 8);
            Assert.AreEqual(rank, sgrid[1, 2].Rank);

            // middle
            sodoku[4, 5] = new CellContent(5);
            rank = sodoku[4, 5].Rank;
            Assert.AreEqual(rank, sodoku[4, 5].Rank);
            row = sodoku.GetRowAt(5);
            Assert.AreEqual(rank, row[4].Rank);
            col = sodoku.GetColumnAt(4);
            Assert.AreEqual(rank, col[5].Rank);
            sgrid = sodoku.GetSodoku3x3AtGridPosition(4, 5);
            Assert.AreEqual(rank, sgrid[1, 2].Rank);
            
            // middle bottom
            sodoku[5, 7] = new CellContent(2);
            rank = sodoku[5, 7].Rank;
            Assert.AreEqual(rank, sodoku[5, 7].Rank);
            row = sodoku.GetRowAt(7);
            Assert.AreEqual(rank, row[5].Rank);
            col = sodoku.GetColumnAt(5);
            Assert.AreEqual(rank, col[7].Rank);
            sgrid = sodoku.GetSodoku3x3AtGridPosition(5, 7);
            Assert.AreEqual(rank, sgrid[2, 1].Rank);

            Assert.IsTrue(sodoku.Validate());
            //sodoku.
            
        }


        [TestMethod]
        public void Test_Sodoku_Available()
        {
            Sodoku sodoku = new Sodoku();

            List<int> available = sodoku.AvailableRanks(4, 4);

            Assert.AreEqual(9, available.Count);

            sodoku[4, 1] = new CellContent(1);

            var col = sodoku.GetColumnAt(4);
            Assert.AreEqual(8, col.AvailableRanks().Count);

            available = sodoku.AvailableRanks(4, 4);
            Assert.AreEqual(8, available.Count);

            sodoku[1, 4] = new CellContent(1);
            available = sodoku.AvailableRanks(4, 4);
            Assert.AreEqual(8, available.Count);

            sodoku[2, 4] = new CellContent(2);
            available = sodoku.AvailableRanks(4, 4);
            Assert.AreEqual(7, available.Count);

            sodoku[4, 8] = new CellContent(3);
            available = sodoku.AvailableRanks(4, 4);
            Assert.AreEqual(6, available.Count);

            sodoku[3, 3] = new CellContent(4);
            available = sodoku.AvailableRanks(4, 4);
            Assert.AreEqual(5, available.Count);

            sodoku[5, 5] = new CellContent(5);
            available = sodoku.AvailableRanks(4, 4);
            Assert.AreEqual(4, available.Count);

            sodoku[7, 4] = new CellContent(6);
            available = sodoku.AvailableRanks(4, 4);
            Assert.AreEqual(3, available.Count);

            sodoku[4, 7] = new CellContent(7);
            available = sodoku.AvailableRanks(4, 4);
            Assert.AreEqual(2, available.Count);

            sodoku[3, 5] = new CellContent(8);
            available = sodoku.AvailableRanks(4, 4);
            Assert.AreEqual(1, available.Count);

            Assert.AreEqual(9, available[0]);

        }



        [TestMethod]
        public void Test_Sodoku_Available_T2()
        {
            Sodoku sodoku = new Sodoku();

            List<int> available = sodoku.AvailableRanks(0, 6);
            Assert.AreEqual(9, available.Count);

            sodoku[0, 6] = new CellContent(1);
            available = sodoku.AvailableRanks(0, 6);
            Assert.AreEqual(0, available.Count);

            available = sodoku.AvailableRanks(1, 7);
            Assert.AreEqual(8, available.Count);

            // check if rank 1 is available:
            int cnt = available.Where(q => q == 1).Count();
            Assert.AreEqual(0, cnt);

            Assert.IsFalse(sodoku.ValidateMove(1, 7, 1));
        }


    }
}
