using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LinearAlgebra;

namespace TestComplexNumber
{
    /// <summary>
    /// Summary description for QCExercisesChapter5
    /// </summary>
    [TestClass]
    public class QCExercisesChapter5
    {
        [TestMethod]
        public void Exercise5o1o2()
        {
            ComplexVector V = new ComplexVector(2);

            V[0] = 15 - 3.4 * ComplexNumber.I;
            V[1] = 2.1 - 16 * ComplexNumber.I;

            ComplexVector result = V.GetNormalizedVector();

            ComplexVector expected = new ComplexVector(2);

            expected[0] = 0.67286 - 0.15252 * ComplexNumber.I;
            expected[1] = 0.09420 - 0.71772 * ComplexNumber.I;

            Assert.AreEqual(expected, result);

        }

        [TestMethod]
        public void Exercise5o4o6()
        {

        }
    }
}
