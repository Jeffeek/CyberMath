using System;
using System.Collections.Generic;
using System.Text;
using CyberMath.Structures.Matrix.JaggedMatrix.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CyberMath.JaggedMatrix.Tests
{
    [TestClass]
    public class SortingRowsOperations
    {
        public void SortByRowsTest()
        {
            int rowsCount = 3;
            int[] columnArr = {2, 3, 1};
            var juggered = new JuggedMatrix<int>(rowsCount, columnArr)
            {
                [0,0] 
            };
            
        }
    }
}
