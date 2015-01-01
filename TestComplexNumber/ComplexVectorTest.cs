using LinearAlgebra;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestComplexNumber
{
    
    
    /// <summary>
    ///This is a test class for ComplexVectorTest and is intended
    ///to contain all ComplexVectorTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ComplexVectorTest
    {


        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void EqualsTest()
        {
            ComplexNumber[] components1 = { new ComplexNumber(-1.00001, 0.999999), new ComplexNumber(4.00004, -3.22222) };
            ComplexNumber[] components2 = { new ComplexNumber(-1.000001, 1.0), new ComplexNumber(4.000004, -3.222222) };

            ComplexVector v = new ComplexVector(components1);
            ComplexVector u = new ComplexVector(components2);

            Assert.IsTrue(v.Equals(u));
        }

        /// <summary>
        ///A test for SetToZero
        ///</summary>
        [TestMethod()]
        public void SetToZeroTest()
        {
            ComplexVector u = new ComplexVector(4); 
            u.SetToZero();
            ComplexNumber[] compV = { new ComplexNumber(1.4, 5.4), new ComplexNumber(-5.3, -7.3), new ComplexNumber(8.3, -9.3), new ComplexNumber(-9.2, 1)};
            ComplexVector v = new ComplexVector(compV);

            Assert.AreEqual(v, u + v);
        }

        /// <summary>
        ///A test for GetKet
        ///</summary>
        [TestMethod()]
        public void GetKetTest()
        {
            // This is also Exercise 4.1.9 on p. 114 QCCS
            ComplexVector target = new ComplexVector(2);
            ComplexVector expected = new ComplexVector(2);

            target[0] = new ComplexNumber(3, 1);
            target[1] = new ComplexNumber(0, -2);

            expected[0] = new ComplexNumber(3, -1);
            expected[1] = new ComplexNumber(0, 2);

            ComplexVector actual;
            actual = target.GetKet();
            Assert.AreEqual(expected, actual);
        }




        /// <summary>
        ///A test for GetAmplitude
        ///</summary>
        [TestMethod()]
        public void GetAmplitudeTest()
        {
            // This is also Example 4.1.6 on p. 113 QCCS
            ComplexVector a = new ComplexVector(2);
            ComplexVector b = new ComplexVector(2);

            ComplexNumber expected = new ComplexNumber(0, -1);

            a[0] = new ComplexNumber(1, 0);
            a[1] = new ComplexNumber(0, 1); 
            a = (Math.Sqrt(2) / 2.0) * a;

            b[0] = new ComplexNumber(0, 1);
            b[1] = new ComplexNumber(-1, 0); 
            b = (Math.Sqrt(2) / 2.0) * b;

            ComplexNumber actual;
            //ComplexVector ket;
            //ket = b.GetKet();
            //actual = ket.InnerProduct(a);
            actual = a.GetAmplitude(b);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetNormalizedAmplitude
        ///</summary>
        [TestMethod()]
        public void GetNormalizedAmplitudeTest()
        {
            // This is also example 4.1.7 on p. 114 QCCS
            ComplexVector a = new ComplexVector(2); 
            ComplexVector b = new ComplexVector(2);

            a[0] = new ComplexNumber(1, 0);
            a[1] = new ComplexNumber(0, -1);
            b[0] = new ComplexNumber(0, 1);
            b[1] = new ComplexNumber(1, 0);

            ComplexNumber expected = new ComplexNumber(0, -1); 
            ComplexNumber actual;
            actual = a.GetNormalizedAmplitude(b);
            Assert.AreEqual(expected, actual);
        }


        /// <summary>
        ///A test for GetNormalizedVector
        ///</summary>
        [TestMethod()]
        public void GetNormalizedVectorTest()
        {
            ComplexVector target = new ComplexVector(2);
            target[0] = 2 - 3 * ComplexNumber.I;
            target[1] = 1 + 2 * ComplexNumber.I;

            ComplexVector normalized = target.GetNormalizedVector();

            ComplexVector expected = new ComplexVector(2);
            expected[0] = 0.4714 - 0.70711 * ComplexNumber.I;
            expected[1] = 0.23570 + 0.47140 * ComplexNumber.I;

            Assert.AreEqual(expected, normalized);
            Assert.AreEqual(1.0, normalized.Length());
        }
    }
}
