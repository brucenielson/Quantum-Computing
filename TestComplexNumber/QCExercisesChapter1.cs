using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LinearAlgebra;

namespace TestComplexNumber
{
    [TestClass]
    public class QCExercisesChapter1
    {
        [TestMethod]
        public void Exercise1o2o3()
        {
            ComplexNumber c1 = new ComplexNumber(0.0, 3.0);
            ComplexNumber c2 = new ComplexNumber(-1.0, -1.0);
            ComplexNumber expected = new ComplexNumber(-3.0/2.0, -3.0/2.0);
            ComplexNumber actual;
            actual = (c1 / c2);
            Assert.IsTrue(expected == actual);
        }

        [TestMethod]
        public void Exercise1o2o4()
        {
            ComplexNumber c = new ComplexNumber(4.0, -3.0);
            double expected = 5.0;
            double actual;
            actual = c.Modulus();
            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void Exercise1o2o12()
        {
            ComplexNumber c = new ComplexNumber(4.0, -3.0);
            Assert.AreEqual(0.0, (c * c.Conjugate()).Imaginary); 
            Assert.AreEqual((c * c.Conjugate()).Real, (c * c).Modulus());
        }


        [TestMethod]
        public void Exercise1o3o2()
        {
            ComplexNumber target = new ComplexNumber(1.0, 1.0);
            double expected = Math.PI / 4.0;
            double actual;
            actual = target.GetAngle();
            Assert.IsTrue(expected == actual);

            expected = Math.Sqrt(2);
            actual = target.Modulus();
            Assert.IsTrue(expected == actual);

            // in degrees
            expected = 45.0;
            actual = target.GetAngleDegrees();
            Assert.AreEqual(expected, actual);
        }



        [TestMethod]
        public void Exercise1o3o3()
        {
            ComplexNumber target = new ComplexNumber();
            target.SetPolarCoordinates(3.0, Math.PI / 3.0);

            Assert.AreEqual(new ComplexNumber(1.5, 2.6), target);        
        }


        [TestMethod]
        public void Exercise1o3o4()
        {
            ComplexNumber c1 = new ComplexNumber(-2.0, -1.0);
            ComplexNumber c2 = new ComplexNumber(-1.0, -2.0);
            double c1len = c1.Length;
            double c1angle = c1.Angle;
            double c2len = c2.Length;
            double c2angle = c2.Angle;
            double c1degrees = c1.GetAngleDegrees();
            double c2degrees = c2.GetAngleDegrees();

            ComplexNumber p1 = new ComplexNumber();
            ComplexNumber p2 = new ComplexNumber();
            p1.SetPolarCoordinates(c1len, c1angle);
            p2.SetPolarCoordinates(c2len, c2angle);

            Assert.AreEqual(c1 * c2, p1 * p2);            
        }

        [TestMethod]
        public void Exercise1o3o7()
        {
            ComplexNumber byCartesian = new ComplexNumber(2.0, 2.0);
            ComplexNumber byPolar = new ComplexNumber(2.0, 2.0);
            ComplexNumber divisor = new ComplexNumber(1.0, 1.0);
            byCartesian = byCartesian / divisor;
            byPolar.DividePolar(divisor);

            Assert.AreEqual(byCartesian, byPolar);
        }


        [TestMethod]
        public void Exercise1o3o8()
        {
            ComplexNumber c = new ComplexNumber(1.0, -1.0);
            c = c ^ 5.0;

            Assert.AreEqual(new ComplexNumber(-4.0, 4.0), c);
        }


        [TestMethod]
        public void Exercise1o3o9()
        {
            ComplexNumber c = new ComplexNumber(1.0, 1.0);
            c.Root(3.0, 0);
            Assert.AreEqual(new ComplexNumber(1.0842, 0.2905), c);
            c = new ComplexNumber(1.0, 1.0);
            c.Root(3.0, 1);
            Assert.AreEqual(new ComplexNumber(-0.7937, 0.7937), c);
            c = new ComplexNumber(1.0, 1.0);
            c.Root(3.0, 2);
            Assert.AreEqual(new ComplexNumber(-0.2905, -1.0842), c);            
        }

    }
}
