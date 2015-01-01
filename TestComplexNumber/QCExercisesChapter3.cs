using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LinearAlgebra;


namespace TestComplexNumber
{
    [TestClass]
    public class QCExercisesChapter3
    {
        [TestMethod]
        public void Exercise3o1o1()
        {
            // P. 76
            DoubleMatrix M = new DoubleMatrix(6, 6);
            DoubleVector V = new DoubleVector(6);
            DoubleVector result = new DoubleVector(6);

            M[0, 0] = M[0, 1] = M[0, 2] = M[0, 3] = M[0, 4] = M[0, 5] = 0.0;
            M[1, 0] = M[1, 1] = M[1, 2] = M[1, 3] = M[1, 4] = M[1, 5] = 0.0;
            M[2, 0] = 0.0;
            M[2, 1] = 1.0;
            M[2, 2] = 0.0;
            M[2, 3] = 0.0;
            M[2, 4] = 0.0;
            M[2, 5] = 1.0;
            M[3, 0] = 0.0;
            M[3, 1] = 0.0;
            M[3, 2] = 0.0;
            M[3, 3] = 1.0;
            M[3, 4] = 0.0;
            M[3, 5] = 0.0;
            M[4, 0] = 0.0;            
            M[4, 1] = 0.0;
            M[4, 2] = 1.0;
            M[4, 3] = 0.0;
            M[4, 4] = 0.0;
            M[4, 5] = 0.0;
            M[5, 0] = 1.0;
            M[5, 1] = 0.0;
            M[5, 2] = 0.0;
            M[5, 3] = 0.0;
            M[5, 4] = 1.0;
            M[5, 5] = 0.0;

            V[0] = 5.0;
            V[1] = 5.0;
            V[2] = 0.0;
            V[3] = 2.0;
            V[4] = 0.0;
            V[5] = 15.0;

            result[0] = 0.0;
            result[1] = 0.0;
            result[2] = 20.0;
            result[3] = 2.0;
            result[4] = 0.0;
            result[5] = 5.0;

            Assert.AreEqual(result, M * V);
        }


        [TestMethod]
        public void Exercise3o1o2()
        {
            // P. 78
            DoubleMatrix M = new DoubleMatrix(6, 6);
            DoubleMatrix M6 = new DoubleMatrix(6, 6);
            DoubleVector V = new DoubleVector(6);
            DoubleVector result = new DoubleVector(6);

            M[0, 0] = M[0, 1] = M[0, 2] = M[0, 3] = M[0, 4] = M[0, 5] = 0.0;
            M[1, 0] = M[1, 1] = M[1, 2] = M[1, 3] = M[1, 4] = M[1, 5] = 0.0;
            M[2, 0] = 0.0;
            M[2, 1] = 1.0;
            M[2, 2] = 0.0;
            M[2, 3] = 0.0;
            M[2, 4] = 0.0;
            M[2, 5] = 1.0;
            M[3, 0] = 0.0;
            M[3, 1] = 0.0;
            M[3, 2] = 0.0;
            M[3, 3] = 1.0;
            M[3, 4] = 0.0;
            M[3, 5] = 0.0;
            M[4, 0] = 0.0;
            M[4, 1] = 0.0;
            M[4, 2] = 1.0;
            M[4, 3] = 0.0;
            M[4, 4] = 0.0;
            M[4, 5] = 0.0;
            M[5, 0] = 1.0;
            M[5, 1] = 0.0;
            M[5, 2] = 0.0;
            M[5, 3] = 0.0;
            M[5, 4] = 1.0;
            M[5, 5] = 0.0;

            V[0] = 0.0;
            V[1] = 0.0;
            V[2] = 10.0;
            V[3] = 0.0;
            V[4] = 0.0;
            V[5] = 0.0;

            result[0] = 0.0;
            result[1] = 0.0;
            result[2] = 10.0;
            result[3] = 0.0;
            result[4] = 0.0;
            result[5] = 0.0;

            M6[0, 0] = M6[0, 1] = M6[0, 2] = M6[0, 3] = M6[0, 4] = M6[0, 5] = 0.0;
            M6[1, 0] = M6[1, 1] = M6[1, 2] = M6[1, 3] = M6[1, 4] = M6[1, 5] = 0.0;
            M6[2, 0] = 0.0;
            M6[2, 1] = 0.0;
            M6[2, 2] = 1.0;
            M6[2, 3] = 0.0;
            M6[2, 4] = 0.0;
            M6[2, 5] = 0.0;
            M6[3, 0] = 0.0;
            M6[3, 1] = 0.0;
            M6[3, 2] = 0.0;
            M6[3, 3] = 1.0;
            M6[3, 4] = 0.0;
            M6[3, 5] = 0.0;
            M6[4, 0] = 1.0;
            M6[4, 1] = 0.0;
            M6[4, 2] = 0.0;
            M6[4, 3] = 0.0;
            M6[4, 4] = 1.0;
            M6[4, 5] = 0.0;
            M6[5, 0] = 0.0;
            M6[5, 1] = 1.0;
            M6[5, 2] = 0.0;
            M6[5, 3] = 0.0;
            M6[5, 4] = 0.0;
            M6[5, 5] = 1.0;


            Assert.AreEqual(M6, M^6);
            Assert.AreEqual(result, (M ^ 6) * V);
        }


        [TestMethod]
        public void Exercise3o1o4()
        {
            // P. 78
            DoubleMatrix M = new DoubleMatrix(6, 6);
            DoubleVector V = new DoubleVector(6);
            DoubleVector result = new DoubleVector(6);

            V[0] = 0.0;
            V[1] = 0.0;
            V[2] = 10.0;
            V[3] = 0.0;
            V[4] = 0.0;
            V[5] = 0.0;

            result[0] = 0.0;
            result[1] = 0.0;
            result[2] = -10.0;
            result[3] = 0.0;
            result[4] = 0.0;
            result[5] = 0.0;

            M[0, 0] = M[0, 1] = M[0, 2] = M[0, 3] = M[0, 4] = M[0, 5] = 0.0;
            M[1, 0] = M[1, 1] = M[1, 2] = M[1, 3] = M[1, 4] = M[1, 5] = 0.0;
            M[2, 0] = 0.0;
            M[2, 1] = 0.0;
            M[2, 2] = -1.0;
            M[2, 3] = 0.0;
            M[2, 4] = 0.0;
            M[2, 5] = 0.0;
            M[3, 0] = 0.0;
            M[3, 1] = 0.0;
            M[3, 2] = 0.0;
            M[3, 3] = -1.0;
            M[3, 4] = 0.0;
            M[3, 5] = 0.0;
            M[4, 0] = -1.0;
            M[4, 1] = 0.0;
            M[4, 2] = 0.0;
            M[4, 3] = 0.0;
            M[4, 4] = -1.0;
            M[4, 5] = 0.0;
            M[5, 0] = 0.0;
            M[5, 1] = -1.0;
            M[5, 2] = 0.0;
            M[5, 3] = 0.0;
            M[5, 4] = 0.0;
            M[5, 5] = -1.0;

            Assert.AreEqual(result, M * V);
        }


        [TestMethod]
        public void Example3o2o1()
        {
            // P. 80-81
            DoubleMatrix M = new DoubleMatrix(3, 3);
            DoubleVector V = new DoubleVector(3);
            DoubleVector result = new DoubleVector(3);

            V[0] = (1.0/6.0);
            V[1] = (1.0/6.0);
            V[2] = (2.0/3.0);

            result[0] = (21.0/36.0);
            result[1] = (9.0/36.0);
            result[2] = (6.0/36.0);

            M[0, 0] = 0.0;
            M[0, 1] = (1.0/6.0);
            M[0, 2] = (5.0/6.0);
            M[1, 0] = (1.0/3.0);
            M[1, 1] = (1.0/2.0);
            M[1, 2] = (1.0/6.0);
            M[2, 0] = (2.0/3.0);
            M[2, 1] = (1.0/3.0);
            M[2, 2] = 0.0;


            Assert.AreEqual(result, M * V);
        }


        
        [TestMethod]
        public void Exercise3o2o1()
        {
            // P. 81
            DoubleMatrix M = new DoubleMatrix(3, 3);
            DoubleVector X = new DoubleVector(3);
            DoubleVector result = new DoubleVector(3);

            X[0] = (1.0 / 2.0);
            X[1] = 0.0;
            X[2] = (1.0 / 2.0);

            result[0] = (5.0 / 12.0);
            result[1] = (3.0 / 12.0);
            result[2] = (4.0 / 12.0);

            M[0, 0] = 0.0;
            M[0, 1] = (1.0 / 6.0);
            M[0, 2] = (5.0 / 6.0);
            M[1, 0] = (1.0 / 3.0);
            M[1, 1] = (1.0 / 2.0);
            M[1, 2] = (1.0 / 6.0);
            M[2, 0] = (2.0 / 3.0);
            M[2, 1] = (1.0 / 3.0);
            M[2, 2] = 0.0;


            Assert.AreEqual(result, M * X);
            Assert.IsTrue(result.IsProbabilisticSystem());
        }



        [TestMethod]
        public void Exercise3o2o4()
        {
            // P. 83
            DoubleMatrix M = new DoubleMatrix(2, 2);
            DoubleMatrix N = new DoubleMatrix(2, 2);

            M[0, 0] = (1.0 / 3.0);
            M[0, 1] = (2.0 / 3.0);
            M[1, 0] = (2.0 / 3.0);
            M[1, 1] = (1.0 / 3.0);

            N[0, 0] = (1.0 / 2.0);
            N[0, 1] = (1.0 / 2.0);
            N[1, 0] = (1.0 / 2.0);
            N[1, 1] = (1.0 / 2.0);

            Assert.IsTrue(M.IsDoublyStochastic());
            Assert.IsTrue(N.IsDoublyStochastic());
            Assert.IsTrue((M * N).IsDoublyStochastic());
            Assert.IsTrue((N * M).IsDoublyStochastic());
        }


        [TestMethod]
        public void Exercise3o2o6()
        {
            // P. 85
            DoubleMatrix M1 = new DoubleMatrix(3, 3);
            DoubleMatrix M8 = new DoubleMatrix(3, 3);
            DoubleVector V = new DoubleVector(3);

            // 0 = math major
            // 1 = physics major
            // 2 = computer science major
            M1[0, 0] = 0.1; // became math, was math
            M1[0, 1] = 0.7; // became math, was physics
            M1[0, 2] = 0.2; // became math, was cs
            M1[1, 0] = 0.6; // became physics, was math
            M1[1, 1] = 0.2; // became physics, was physics
            M1[1, 2] = 0.2; // became physics, was cs
            M1[2, 0] = 0.3; // became cs, was math
            M1[2, 1] = 0.1; // became cs, was physics
            M1[2, 2] = 0.6; // became cs, was cs

            M8[0, 0] = 0.335576; // became math, was math
            M8[0, 1] = 0.331309; // became math, was physics
            M8[0, 2] = 0.333115; // became math, was cs
            M8[1, 0] = 0.33167; // became physics, was math
            M8[1, 1] = 0.335215; // became physics, was physics
            M8[1, 2] = 0.333115; // became physics, was cs
            M8[2, 0] = 0.332754; // became cs, was math
            M8[2, 1] = 0.333476; // became cs, was physics
            M8[2, 2] = 0.33377; // became cs, was cs

            Assert.IsTrue(M8.IsDoublyStochastic());
            Assert.AreEqual(M8, M1 ^ 8);

            V[0] = 1.0;
            V[1] = 0.0;
            V[2] = 0.0;

            double[] components = new double[3];
            components[0] = M8[0, 0];
            components[1] = M8[1, 0];
            components[2] = M8[2, 0];

            Assert.AreEqual(new DoubleVector(components), ((M1 ^ 8) * V));

            V[0] = 0.0;
            V[1] = 1.0;
            V[2] = 0.0;

            components = new double[3];
            components[0] = M8[0, 1];
            components[1] = M8[1, 1];
            components[2] = M8[2, 1];

            Assert.AreEqual(new DoubleVector(components), ((M1 ^ 8) * V));

            V[0] = 0.0;
            V[1] = 0.0;
            V[2] = 1.0;

            components = new double[3];
            components[0] = M8[0, 2];
            components[1] = M8[1, 2];
            components[2] = M8[2, 2];

            Assert.AreEqual(new DoubleVector(components), ((M1 ^ 8) * V));

        }


        [TestMethod]
        public void Exercise3o3o1()
        {
            // P. 90
            ComplexMatrix U = new ComplexMatrix(3, 3);

            U[0, 0] = Math.Cos(1.2);
            U[0, 1] = -Math.Sin(1.2);
            U[0, 2] = 0.0;
            U[1, 0] = Math.Sin(1.2);
            U[1, 1] = Math.Cos(1.2);
            U[1, 2] = 0.0;
            U[2, 0] = 0.0;
            U[2, 1] = 0.0;
            U[2, 2] = 1.0;

            Assert.IsTrue(U.IsUnitary());
            Assert.IsTrue(U.ModulusSquared().IsDoublyStochastic());
            Assert.IsTrue(U.ModulusSquared().IsProbabilisticSystem());

        }


        [TestMethod]
        public void Exercise3o4o2()
        {
            // P. 100
            DoubleMatrix N = new DoubleMatrix(2, 2);

            N[0, 0] = 1.0 / 3.0;
            N[0, 1] = 2.0 / 3.0;
            N[1, 0] = 2.0 / 3.0;
            N[1, 1] = 1.0 / 3.0;

            DoubleMatrix result = new DoubleMatrix(4, 4);

            result[0, 0] = 1.0 / 9.0;
            result[0, 1] = 2.0 / 9.0;
            result[0, 2] = 2.0 / 9.0;
            result[0, 3] = 4.0 / 9.0;
            result[1, 0] = 2.0 / 9.0;
            result[1, 1] = 1.0 / 9.0;
            result[1, 2] = 4.0 / 9.0;
            result[1, 3] = 2.0 / 9.0;
            result[2, 0] = 2.0 / 9.0;
            result[2, 1] = 4.0 / 9.0;
            result[2, 2] = 1.0 / 9.0;
            result[2, 3] = 2.0 / 9.0;
            result[3, 0] = 4.0 / 9.0;
            result[3, 1] = 2.0 / 9.0;
            result[3, 2] = 2.0 / 9.0;
            result[3, 3] = 1.0 / 9.0;

            Assert.AreEqual(result, N.TensorProduct(N));

        }


        [TestMethod]
        public void Exercise3o4o3()
        {
            // P. 101
            DoubleMatrix M = new DoubleMatrix(2, 2);
            DoubleMatrix N = new DoubleMatrix(2, 2);

            M[0, 0] = 1.0 / 3.0;
            M[0, 1] = 2.0 / 3.0;
            M[1, 0] = 2.0 / 3.0;
            M[1, 1] = 1.0 / 3.0;


            N[0, 0] = 1.0 / 2.0;
            N[0, 1] = 1.0 / 2.0;
            N[1, 0] = 1.0 / 2.0;
            N[1, 1] = 1.0 / 2.0;

            DoubleMatrix tensor = N.TensorProduct(N);

            DoubleMatrix result = new DoubleMatrix(4, 4);

            result[0, 0] = 1.0 / 6.0;
            result[0, 1] = 1.0 / 6.0;
            result[0, 2] = 2.0 / 6.0;
            result[0, 3] = 2.0 / 6.0;
            result[1, 0] = 1.0 / 6.0;
            result[1, 1] = 1.0 / 6.0;
            result[1, 2] = 2.0 / 6.0;
            result[1, 3] = 2.0 / 6.0;
            result[2, 0] = 2.0 / 6.0;
            result[2, 1] = 2.0 / 6.0;
            result[2, 2] = 1.0 / 6.0;
            result[2, 3] = 1.0 / 6.0;
            result[3, 0] = 2.0 / 6.0;
            result[3, 1] = 2.0 / 6.0;
            result[3, 2] = 1.0 / 6.0;
            result[3, 3] = 1.0 / 6.0;

            Assert.AreEqual(result, M.TensorProduct(N));

        }


    }
}
