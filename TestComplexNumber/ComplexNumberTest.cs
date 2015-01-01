using LinearAlgebra;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestComplexNumber
{
    
    /// <summary>
    ///This is a test class for ComplexNumberTest and is intended
    ///to contain all ComplexNumberTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ComplexNumberTest
    {


        /// <summary>
        ///A test for Zero
        ///</summary>
        [TestMethod()]
        public void ZeroTest()
        {
            ComplexNumber actual;
            actual = ComplexNumber.Zero;
            Assert.AreEqual(new ComplexNumber(0.0, 0.0), actual);
        }

        /// <summary>
        ///A test for One
        ///</summary>
        [TestMethod()]
        public void OneTest()
        {
            ComplexNumber actual;
            actual = ComplexNumber.One;
            Assert.AreEqual(new ComplexNumber(1.0, 0.0), actual);
        }

        /// <summary>
        ///A test for NegativeOne
        ///</summary>
        [TestMethod()]
        public void NegativeOneTest()
        {
            ComplexNumber actual;
            actual = ComplexNumber.NegativeOne;
            Assert.AreEqual(new ComplexNumber(-1.0, 0.0), actual);
        }

        /// <summary>
        ///A test for NegativeI
        ///</summary>
        [TestMethod()]
        public void NegativeITest()
        {
            ComplexNumber actual;
            actual = ComplexNumber.NegativeI;
            Assert.AreEqual(new ComplexNumber(0.0, -1.0), actual);
        }



        /// <summary>
        ///A test for Real
        ///</summary>
        [TestMethod()]
        public void RealTest()
        {
            ComplexNumber target = new ComplexNumber(); 
            double expected = 1.3F; 
            double actual;
            target.Real = expected;
            actual = target.Real;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Imaginary
        ///</summary>
        [TestMethod()]
        public void ImaginaryTest()
        {
            ComplexNumber target = new ComplexNumber(); 
            double expected = 1.3F; 
            double actual;
            target.Imaginary = expected;
            actual = target.Imaginary;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for I
        ///</summary>
        [TestMethod()]
        public void ITest()
        {
            ComplexNumber actual;
            actual = ComplexNumber.I;
            Assert.AreEqual(new ComplexNumber(0.0, 1.0), actual);
        }

        /// <summary>
        ///A test for op_Addition
        ///</summary>
        [TestMethod()]
        public void op_AdditionTest()
        {
            ComplexNumber first = new ComplexNumber(2.2, 3.2); 
            ComplexNumber second = new ComplexNumber(3.2, 2.2); 
            ComplexNumber expected = new ComplexNumber(5.4, 5.4); 
            ComplexNumber actual;
            actual = (first + second);
            Assert.AreEqual(expected, actual);

            // a few other tests
            Assert.IsTrue(first + ComplexNumber.I == new ComplexNumber(2.2, 4.2));
            Assert.IsTrue(first + ComplexNumber.NegativeI == new ComplexNumber(2.2, 2.2));
            Assert.IsTrue(first + ComplexNumber.NegativeOne == new ComplexNumber(1.2, 3.2));
            Assert.IsTrue(first + ComplexNumber.Zero == new ComplexNumber(2.2, 3.2));

        }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void ToStringTest()
        {
            ComplexNumber target = new ComplexNumber(2.1, 3.4); 
            string expected = "2.1 + 3.4i"; 
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for op_Multiply
        ///</summary>
        [TestMethod()]
        public void op_MultiplyTest()
        {
            ComplexNumber first = new ComplexNumber(3.0, -1.0); 
            ComplexNumber second = new ComplexNumber(1, 4.0); 
            ComplexNumber expected = new ComplexNumber(7.0, 11.0); 
            ComplexNumber actual;
            actual = (first * second);
            Assert.IsTrue(expected == actual);

            // a few other tests
            Assert.IsTrue(first * ComplexNumber.I == new ComplexNumber(1.0, 3.0));
            Assert.IsTrue(first * ComplexNumber.NegativeI == new ComplexNumber(-1.0, -3.0));
            Assert.IsTrue(first * ComplexNumber.NegativeOne == new ComplexNumber(-3.0, 1.0));
            Assert.IsTrue(first * ComplexNumber.Zero == new ComplexNumber(0.0, 0.0));

        }

        /// <summary>
        ///A test for op_Division
        ///</summary>
        [TestMethod()]
        public void op_DivisionTest()
        {
            ComplexNumber c1 = new ComplexNumber(-2.0, 1.0); 
            ComplexNumber c2 = new ComplexNumber(1.0, 2.0); 
            ComplexNumber expected = new ComplexNumber(0.0, 1.0); 
            ComplexNumber actual;
            actual = (c1 / c2);
            Assert.IsTrue(expected == actual);
        }

        /// <summary>
        ///A test for Modulus
        ///</summary>
        [TestMethod()]
        public void ModulusTest()
        {
            ComplexNumber target = new ComplexNumber(1.0, -1.0);
            double expected = Math.Sqrt(2.0); 
            double actual;
            actual = target.Modulus();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Conjugate
        ///</summary>
        [TestMethod()]
        public void ConjugateTest()
        {
            ComplexNumber target = new ComplexNumber(3.0, 5.0); 
            ComplexNumber expected = new ComplexNumber(3.0, -5.0);
            ComplexNumber actual;
            actual = target.Conjugate();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetAngle
        ///</summary>
        [TestMethod()]
        public void GetAngleTest()
        {
            ComplexNumber target = new ComplexNumber(1.0, 1.0); 
            double expected = Math.PI/4.0; 
            double actual;
            actual = target.GetAngle();
            Assert.IsTrue(expected == actual);
        }

        /// <summary>
        ///A test for GetAngleDegrees
        ///</summary>
        [TestMethod()]
        public void GetAngleDegreesTest()
        {
            ComplexNumber target = new ComplexNumber(1.0, 1.0);
            double expected = 45.0;
            double actual;
            actual = target.GetAngleDegrees();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for SetPolarCoordinates
        ///</summary>
        [TestMethod()]
        public void SetPolarCoordinatesTest()
        {
            ComplexNumber target = new ComplexNumber(); 
            double length = Math.Sqrt(2.0); 
            double angle = Math.PI/4.0; 
            target.SetPolarCoordinates(length, angle);
            Assert.AreEqual(new ComplexNumber(1.0, 1.0), target);
        }

        /// <summary>
        ///A test for SetCartesianCoordinates
        ///</summary>
        [TestMethod()]
        public void SetCartesianCoordinatesTest()
        {
            ComplexNumber target = new ComplexNumber(1.0, 1.0); 
            double real = 2.0;  
            double imaginary = 2.0; 
            target.SetCartesianCoordinates(real, imaginary);
            Assert.AreEqual(new ComplexNumber(2.0, 2.0), target);
        }

        /// <summary>
        ///A test for Divide
        ///</summary>
        [TestMethod()]
        public void DivideTest()
        {
            ComplexNumber byCartesian = new ComplexNumber(5.6, -8.9);
            ComplexNumber byPolar = new ComplexNumber(5.6, -8.9);
            ComplexNumber divisor = new ComplexNumber(-3.2, 2.4);
            byCartesian = byCartesian / divisor;
            byPolar.DividePolar(divisor);

            Assert.AreEqual(byCartesian, byPolar);
        }

        /// <summary>
        ///A test for op_Power
        ///</summary>
        [TestMethod()]
        public void op_PowerTest()
        {
            ComplexNumber complexNumber = new ComplexNumber();
            complexNumber.SetPolarCoordinates(2.0, 2.0);
            ComplexNumber actual = complexNumber ^ 2.0;
            ComplexNumber expected = new ComplexNumber();
            expected.SetPolarCoordinates(4.0, 4.0);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Root
        ///</summary>
        [TestMethod()]
        public void RootTest()
        {
            ComplexNumber c = new ComplexNumber(2.0, -1.5);
            c = c ^ 5.0;
            c.Root(5.0, 4);
            Assert.AreEqual(new ComplexNumber(2.0, -1.5), c);
        }

        /// <summary>
        ///A test for PolarCoordinates
        ///</summary>
        [TestMethod()]
        public void PolarCoordinatesTest()
        {
            ComplexNumber target = new ComplexNumber(1.0, 1.0);
            double length = Math.Sqrt(2.0);
            double angle = Math.PI / 4.0;
            Assert.AreEqual(length, target.Length);
            Assert.AreEqual(angle, target.Angle);

            string expected = "p=1.4142135623731 angle=0.785398163397448"; 
            string actual;
            actual = target.PolarCoordinatesString();
            Assert.AreEqual(expected, actual);
        }



        /// <summary>
        ///A test for CartesianCoordinates
        ///</summary>
        [TestMethod()]
        public void CartesianCoordinatesTest()
        {
            ComplexNumber target = new ComplexNumber(1.0, 1.0);
            double length = Math.Sqrt(2.0);
            double angle = Math.PI / 4.0;
            Assert.AreEqual(length, target.Length);
            Assert.AreEqual(angle, target.Angle);

            string expected = "Real=1 Imaginary=1";
            string actual;
            actual = target.CartesianCoordinatesString();
            Assert.AreEqual(expected, actual);
        }



        /// <summary>
        ///A test for ExponentialForm
        ///</summary>
        [TestMethod()]
        public void ExponentialFormTest()
        {
            ComplexNumber target = new ComplexNumber(1.0, 1.0);
            double length = Math.Sqrt(2.0);
            double angle = Math.PI / 4.0;
            Assert.AreEqual(length, target.Length);
            Assert.AreEqual(angle, target.Angle);

            string expected = "1.4142135623731 * e^(i*0.785398163397448)";
            string actual;
            actual = target.ExponentialFormString();
            Assert.AreEqual(expected, actual);
        }


        /// <summary>
        ///A test for op_Subtraction
        ///</summary>
        [TestMethod()]
        public void op_SubtractionTest()
        {
            ComplexNumber first = new ComplexNumber(2.2, 3.2);
            ComplexNumber second = new ComplexNumber(3.2, 2.2);
            ComplexNumber expected = new ComplexNumber(-1.0, 1.0);
            ComplexNumber actual;
            actual = (first - second);
            Assert.AreEqual(expected, actual);

            // a few other tests
            Assert.AreEqual(new ComplexNumber(2.2, 2.2), first - ComplexNumber.I);
            Assert.AreEqual(new ComplexNumber(2.2, 4.2), first - ComplexNumber.NegativeI);
            Assert.AreEqual(new ComplexNumber(3.2, 3.2), first - ComplexNumber.NegativeOne);
            Assert.AreEqual(new ComplexNumber(2.2, 3.2), first - ComplexNumber.Zero);
        }

        /// <summary>
        ///A test for op_Multiply
        ///</summary>
        [TestMethod()]
        public void op_MultiplyTest1()
        {
            ComplexNumber first = new ComplexNumber(3.0, -1.0);
            ComplexNumber actual;
            actual = (first * 2.0);
            Assert.AreEqual(new ComplexNumber(6.0, -2.0), actual);
        }


        /// <summary>
        ///A test for op_Multiply
        ///</summary>
        [TestMethod()]
        public void op_MultiplyTest2()
        {
            ComplexNumber first = new ComplexNumber(3.0, -1.0);
            ComplexNumber actual;
            actual = (2.0 * first);
            Assert.AreEqual(new ComplexNumber(6.0, -2.0), actual);
        }


        /// <summary>
        ///A test for InnerProduct
        ///</summary>
        [TestMethod()]
        public void InnerProductTest()
        {
            ComplexNumber number1 = new ComplexNumber(3, -4); 
            ComplexNumber number2 = new ComplexNumber(2, 3);
            double expected = -6;
            ComplexNumber actual;
            actual = number1.InnerProduct(number2);
            Assert.AreEqual(expected, actual);
        }
    }
}
