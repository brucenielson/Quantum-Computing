using LinearAlgebra;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestComplexNumber
{
    
    
    /// <summary>
    ///This is a test class for DoubleMatrixTest and is intended
    ///to contain all DoubleMatrixTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DoubleMatrixTest
    {

        /// <summary>
        ///A test for IsProbabilisticSystem
        ///</summary>
        [TestMethod()]
        public void IsProbabilisticSystemTest()
        {
            DoubleMatrix M = new DoubleMatrix(3, 3);
            DoubleVector V = new DoubleVector(3);
            DoubleVector result = new DoubleVector(3);

            V[0] = (1.0 / 6.0);
            V[1] = (1.0 / 6.0);
            V[2] = (2.0 / 3.0);

            result[0] = (21.0 / 36.0);
            result[1] = (9.0 / 36.0);
            result[2] = (6.0 / 36.0);

            M[0, 0] = 0.0;
            M[0, 1] = (1.0 / 6.0);
            M[0, 2] = (5.0 / 6.0);
            M[1, 0] = (1.0 / 3.0);
            M[1, 1] = (1.0 / 2.0);
            M[1, 2] = (1.0 / 6.0);
            M[2, 0] = (2.0 / 3.0);
            M[2, 1] = (1.0 / 3.0);
            M[2, 2] = 0.0;

            Assert.IsTrue(M.IsProbabilisticSystem());
            Assert.IsTrue(V.IsProbabilisticSystem());
            Assert.IsTrue(result.IsProbabilisticSystem());
            Assert.AreEqual(result, M * V);
        }
    }
}
