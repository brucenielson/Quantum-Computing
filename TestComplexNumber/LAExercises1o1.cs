using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LinearAlgebra;

namespace ExercisesChapter1Tests
{
    [TestClass]
    public class LAExercise1o1
    {
        [TestMethod]
        public void Number7()
        {
            DoubleVector a = new DoubleVector(new double[] {3.0, 0.0});
            DoubleVector b = new DoubleVector(new double[] { 2.0, 3.0 });

            Assert.AreEqual(new DoubleVector(new double[] { 5.0, 3.0 }), a + b);
        }

        [TestMethod]
        public void Number9()
        {
            DoubleVector d = new DoubleVector(new double[] { 3.0, -2.0 });
            DoubleVector c = new DoubleVector(new double[] { -2.0, 3.0 });
            Assert.IsTrue(d - c == new DoubleVector(new double[] { 5.0, -5.0 }));
        }


        [TestMethod]
        public void Number11()
        {
            DoubleVector a = new DoubleVector(new double[] { 0.0, 2.0, 0.0 });
            DoubleVector c = new DoubleVector(new double[] { 1.0, -2.0, 1.0 });
            Assert.IsTrue(2*a + 3*c == new DoubleVector(new double[] { 3.0, -2.0, 3.0 }));
        }

        [TestMethod]
        public void Number13()
        {
            DoubleVector u = new DoubleVector(new double[] { (1.0/2.0), (Math.Sqrt(3)/2.0) });
            DoubleVector v = new DoubleVector(new double[] { -(Math.Sqrt(3) / 2.0), -(1.0 / 2.0) });
            Assert.IsTrue(u + v == new DoubleVector(new double[] { (1.0 - Math.Sqrt(3.0)) / 2.0, (Math.Sqrt(3.0) - 1.0) / 2.0 }));
            Assert.IsTrue(u - v == new DoubleVector(new double[] { (1.0 + Math.Sqrt(3.0)) / 2.0, (Math.Sqrt(3.0) + 1.0) / 2.0 }));
        }


    }
}
