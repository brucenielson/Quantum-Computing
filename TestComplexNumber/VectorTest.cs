using LinearAlgebra;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestComplexNumber
{


    /// <summary>
    ///This is a test class for VectorTest and is intended
    ///to contain all VectorTest Unit Tests
    ///</summary>
    [TestClass()]
    public class VectorTest
    {

        /// <summary>
        ///A test for Vector Constructor
        ///</summary>
        [TestMethod()]
        public void VectorConstructorTest()
        {
            double[] components = { 1, 2, 3, 4 };
            DoubleVector target = new DoubleVector(components);
            Assert.IsNotNull(target);
            Assert.AreEqual(1, target[0]);
            Assert.AreEqual(2, target[1]);
            Assert.AreEqual(3, target[2]);
            Assert.AreEqual(4, target[3]);
            Assert.AreEqual(4, target.RSpace);
        }

        /// <summary>
        ///A test for Vector Constructor
        ///</summary>
        [TestMethod()]
        public void VectorConstructorTest1()
        {
            DoubleVector target = new DoubleVector(5);
            target[0] = 1;
            target[1] = 2;
            target[2] = 3;
            target[3] = 4;
            Assert.IsNotNull(target);
            Assert.AreEqual(1, target[0]);
            Assert.AreEqual(2, target[1]);
            Assert.AreEqual(3, target[2]);
            Assert.AreEqual(4, target[3]);
            Assert.AreEqual(5, target.RSpace);
        }

        /// <summary>
        ///A test for Vector Constructor
        ///</summary>
        [TestMethod()]
        public void VectorConstructorTest2()
        {
            DoubleVector target = new DoubleVector();
            Assert.IsNotNull(target);
            Assert.AreEqual(0, target.RSpace);
        }


        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void EqualsTest()
        {
            double[] components1 = { 1.00001, 2.00002 };
            double[] components2 = { 1.000001, 2.000002 };
            DoubleVector target = new DoubleVector(components1);
            DoubleVector vector = new DoubleVector(components2);
            Assert.IsTrue(target.Equals(vector));
        }


        /// <summary>
        ///A test for Distance
        ///</summary>
        [TestMethod()]
        public void DistanceTest()
        {
            double[] components1 = { 1.0, 1.0 };
            double[] components2 = { 0.0, 0.0 };
            DoubleVector target = new DoubleVector(components1);
            DoubleVector vector = new DoubleVector(components2);
            double expected = Math.Sqrt(2);
            double actual;
            actual = target.Distance(vector);
            Assert.AreEqual(expected, actual);

            // From Example 6 p. 22
            double[] componentsU = { Math.Sqrt(2.0), 1.0, -1.0 };
            double[] componentsV = { 0.0, 2.0, -2.0 };
            DoubleVector u = new DoubleVector(componentsU);
            DoubleVector v = new DoubleVector(componentsV);
            actual = u.Distance(v);
            Assert.AreEqual(2.0, actual);

        }



        /// <summary>
        ///A test for Length
        ///</summary>
        [TestMethod()]
        public void LengthTest()
        {
            double[] components1 = { 1.0, 1.0 };
            DoubleVector target = new DoubleVector(components1);
            double expected = Math.Sqrt(2);
            double actual;
            actual = target.Length();
            Assert.AreEqual(expected, actual);
        }


        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void ToStringTest()
        {
            double[] components = { 2.3, -8.1 };
            DoubleVector target = new DoubleVector(components);
            string expected = "{2.3, -8.1}\r\n";
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }


        /// <summary>
        ///A test for op_Addition
        ///</summary>
        [TestMethod()]
        public void op_AdditionTest()
        {
            double[] components1 = { 2.3, -8.1 };
            double[] components2 = { 3.2, -1.8 };
            double[] components3 = { 5.5, -9.9 };
            DoubleVector vector1 = new DoubleVector(components1);
            DoubleVector vector2 = new DoubleVector(components2);
            DoubleVector expected = new DoubleVector(components3);
            Assert.AreEqual(expected, vector1 + vector2);
        }

        /// <summary>
        ///A test for op_Equality
        ///</summary>
        [TestMethod()]
        public void op_EqualityTest()
        {
            double[] components1 = { 2.3, -8.1 };
            double[] components2 = { 2.3, -8.1 };
            DoubleVector vector1 = new DoubleVector(components1);
            DoubleVector vector2 = new DoubleVector(components2);
            Assert.IsTrue(vector1 == vector2);
        }

        /// <summary>
        ///A test for op_Inequality
        ///</summary>
        [TestMethod()]
        public void op_InequalityTest()
        {
            double[] components1 = { 2.3, -8.1 };
            double[] components2 = { 2.3, -8.2 };
            DoubleVector vector1 = new DoubleVector(components1);
            DoubleVector vector2 = new DoubleVector(components2);
            Assert.IsTrue(vector1 != vector2);
        }

        /// <summary>
        ///A test for op_Multiply
        ///</summary>
        [TestMethod()]
        public void op_MultiplyTest()
        {
            double[] components1 = { 2.3, -8.1 };
            double[] components2 = { 4.6, -16.2 };
            double scalarMultiple = 2.0;
            DoubleVector vector = new DoubleVector(components1);
            DoubleVector expected = new DoubleVector(components2);
            Assert.AreEqual(expected, scalarMultiple * vector);
        }

        /// <summary>
        ///A test for op_Multiply
        ///</summary>
        [TestMethod()]
        public void op_MultiplyTest1()
        {
            double[] components1 = { 2.3, -8.1 };
            double[] components2 = { 4.6, -16.2 };
            double scalarMultiple = 2.0;
            DoubleVector vector = new DoubleVector(components1);
            DoubleVector expected = new DoubleVector(components2);
            Assert.AreEqual(expected, vector * scalarMultiple);
        }

        /// <summary>
        ///A test for op_DotProduct
        ///</summary>
        [TestMethod()]
        public void op_DotProductTest()
        {
            double[] components1 = { 2.0, -2.0 };
            double[] components2 = { 2.0, -2.0 };
            DoubleVector vector1 = new DoubleVector(components1);
            DoubleVector vector2 = new DoubleVector(components2);
            double actual;
            actual = vector1.DotProduct(vector2);
            Assert.AreEqual(8.0, actual);
        }

        /// <summary>
        ///A test for op_Subtraction
        ///</summary>
        [TestMethod()]
        public void op_SubtractionTest()
        {
            double[] components1 = { 2.3, -8.1 };
            double[] components2 = { 3.2, -1.8 };
            double[] components3 = { -0.9, -6.3 };
            DoubleVector vector1 = new DoubleVector(components1);
            DoubleVector vector2 = new DoubleVector(components2);
            DoubleVector expected = new DoubleVector(components3);
            Assert.AreEqual(expected, vector1 - vector2);
        }

        /// <summary>
        ///A test for GetAngle
        ///</summary>
        [TestMethod()]
        public void GetAngleTest()
        {
            double[] componentsU = { 2, 1, -2 };
            double[] componentsV = { 1, 1, 1 };
            DoubleVector u = new DoubleVector(componentsU);
            DoubleVector v = new DoubleVector(componentsV);
            Assert.AreEqual(Math.Acos(1 / (3 * Math.Sqrt(3.0))), v.GetAngle(u));
        }

        /// <summary>
        ///A test for IsOrthogonal
        ///</summary>
        [TestMethod()]
        public void IsOrthogonalTest()
        {
            double[] componentsU = { 2, 1, 2, 1 };
            double[] componentsV = { -2, -1, 2, 1 };
            DoubleVector u = new DoubleVector(componentsU);
            DoubleVector v = new DoubleVector(componentsV);
            Assert.IsTrue(u.IsOrthogonal(v));
        }


        [TestMethod()]
        public void op_UnaryNegationTest()
        {
            ComplexNumber[] componentsV = { new ComplexNumber(5, 13), new ComplexNumber(6, 2), new ComplexNumber(0.53, -6), new ComplexNumber(12, 0) };

            ComplexMatrix m = new ComplexMatrix(componentsV);
            Assert.AreEqual(new ComplexNumber(-1, 0) * m, -m);
            Assert.AreEqual(-1 * m, -m);

            ComplexVector v = new ComplexVector(componentsV);
            Assert.AreEqual(new ComplexNumber(-1, 0) * v, -v);
            Assert.AreEqual(-1 * v, -v);

            ComplexVector zeroComplex = new ComplexVector(4);
            zeroComplex.SetToZero();
            Assert.AreEqual(zeroComplex, v + (-v));

            double[] componentsU = { 3.0, 5.2, -42 };
            Vector<double> u = new Vector<double>(componentsU);
            Assert.AreEqual(-1 * u, -u);

            double[] componentsZ = { 0.0, 0.0, 0.0 };
            Vector<double> zeroDouble = new Vector<double>(componentsZ);
            Assert.AreEqual(zeroDouble, u + (-u));
        }
    }

}
