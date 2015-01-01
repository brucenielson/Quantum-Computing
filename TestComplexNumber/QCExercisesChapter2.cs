using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LinearAlgebra;


namespace TestComplexNumber
{
    [TestClass]
    public class QCExercisesChapter2
    {


        [TestMethod]
        public void Exercise2o1o1()
        {
            ComplexNumber[] componentsV = { new ComplexNumber(5,13), new ComplexNumber(6,2), new ComplexNumber(0.53,-6), new ComplexNumber(12,0) };
            ComplexNumber[] componentsU = { new ComplexNumber(7,-8), new ComplexNumber(0,4), new ComplexNumber(2,0), new ComplexNumber(9.4,3) };
            ComplexNumber[] componentsE = { new ComplexNumber(12,5), new ComplexNumber(6,6), new ComplexNumber(2.53,-6), new ComplexNumber(21.4,3) };

            ComplexVector v = new ComplexVector(componentsV);
            ComplexVector u = new ComplexVector(componentsU);
            ComplexVector expected = new ComplexVector(componentsE);

            Assert.AreEqual(expected, u+v);
        }


        [TestMethod]
        public void TestEquation2o13()
        {
            ComplexNumber[] componentsV = { new ComplexNumber(5, 13), new ComplexNumber(6, 2), new ComplexNumber(0.53, -6), new ComplexNumber(12, 0) };

            ComplexVector v = new ComplexVector(componentsV);

            Assert.AreEqual(new ComplexNumber(-1.0, 0.0) * v, -v);
            Assert.AreEqual(-1.0 * v, -v);
        }


        [TestMethod]
        public void Exercise2o1o3()
        {
            ComplexNumber[] componentsV = { new ComplexNumber(16, 2.3), new ComplexNumber(0, -7), new ComplexNumber(6, 0), new ComplexNumber(5, -4) };
            ComplexVector v = new ComplexVector(componentsV);

            ComplexNumber scalar = new ComplexNumber(8, -2);

            ComplexNumber[] componentsE = { new ComplexNumber(132.6, -13.6), new ComplexNumber(-14, -56), new ComplexNumber(48, -12), new ComplexNumber(32, -42) };
            ComplexVector expected = new ComplexVector(componentsE);
            
            Assert.AreEqual(expected, v * scalar);
            Assert.AreEqual(expected, scalar * v);
        }



        [TestMethod]
        public void Exercise2o1o4()
        {
            ComplexNumber[] componentsV = { new ComplexNumber(16, 2.3), new ComplexNumber(0, -7), new ComplexNumber(6, 0), new ComplexNumber(5, -4) };
            ComplexVector v = new ComplexVector(componentsV);
            ComplexNumber c1 = new ComplexNumber(8, -2);
            ComplexNumber c2 = new ComplexNumber(-4, 5);

            Assert.AreEqual((c1 + c2) * v, (c1 * v) + (c2 * v));
        }


        [TestMethod]
        public void Exercise2o2o1()
        {
            double[] components = { 2.0, -4.0, 1.0 };
            DoubleVector v = new DoubleVector(components);
            double r1 = 2;
            double r2 = 3;
            Assert.AreEqual(r1 * (r2 * v), (r1 * r2) * v);
        }

        
        [TestMethod]
        public void Exercise2o2o3()
        {
            ComplexNumber c1 = new ComplexNumber(0.0, 2.0);
            ComplexNumber c2 = new ComplexNumber(1.0, 2.0);

            ComplexNumber[,] components1 = new ComplexNumber[2,2];
            components1[0, 0] = new ComplexNumber(1, -1);
            components1[0, 1] = new ComplexNumber(3, 0);
            components1[1, 0] = new ComplexNumber(2, 2);
            components1[1, 1] = new ComplexNumber(4, 1);
            ComplexMatrix A = new ComplexMatrix(components1);

            ComplexNumber[,] components2 = new ComplexNumber[2, 2];
            components2[0, 0] = new ComplexNumber(-2, 6);
            components2[0, 1] = new ComplexNumber(-12, 6);
            components2[1, 0] = new ComplexNumber(-12, -4);
            components2[1, 1] = new ComplexNumber(-18, 4);
            ComplexMatrix expected1 = new ComplexMatrix(components2);

            Assert.AreEqual(expected1, c1 * (c2 * A));
            Assert.AreEqual(expected1, (c1 * c2) * A);
            
            
            ComplexNumber[,] components3 = new ComplexNumber[2, 2];
            components3[0, 0] = new ComplexNumber(5, 3);
            components3[0, 1] = new ComplexNumber(3, 12);
            components3[1, 0] = new ComplexNumber(-6, 10);
            components3[1, 1] = new ComplexNumber(0, 17);
            ComplexMatrix expected2 = new ComplexMatrix(components3);

            Assert.AreEqual(expected2, (c1 * A) + (c2 * A));
            Assert.AreEqual(expected2, (c1 + c2) * A);
        }


        
        [TestMethod]
        public void Exercise2o2o5()
        {
            ComplexMatrix u = new ComplexMatrix(3, 3);
            ComplexMatrix e = new ComplexMatrix(3, 3);

            // load up values
            u[0, 0] = new ComplexNumber(6.0, -3.0);
            u[0, 1] = new ComplexNumber(2.0, 12.0);
            u[0, 2] = new ComplexNumber(0.0, -19.0);
            u[1, 0] = new ComplexNumber(0.0, 0.0);
            u[1, 1] = new ComplexNumber(5.0, 2.1);
            u[1, 2] = new ComplexNumber(17.0, 0.0);
            u[2, 0] = new ComplexNumber(1.0, 0.0);
            u[2, 1] = new ComplexNumber(2.0, 5.0);
            u[2, 2] = new ComplexNumber(3.0, -4.5);

            e[0, 0] = new ComplexNumber(6.0, -3.0);
            e[0, 1] = new ComplexNumber(0.0, 0.0);
            e[0, 2] = new ComplexNumber(1.0, 0.0);
            e[1, 0] = new ComplexNumber(2.0, 12.0);
            e[1, 1] = new ComplexNumber(5.0, 2.1);
            e[1, 2] = new ComplexNumber(2.0, 5.0);
            e[2, 0] = new ComplexNumber(0.0, -19.0);
            e[2, 1] = new ComplexNumber(17.0, 0.0);
            e[2, 2] = new ComplexNumber(3.0, -4.5);

            Assert.AreEqual(e, u.Transpose());

            e[0, 0] = new ComplexNumber(6.0, 3.0);
            e[0, 1] = new ComplexNumber(2.0, -12.0);
            e[0, 2] = new ComplexNumber(0.0, 19.0);
            e[1, 0] = new ComplexNumber(0.0, 0.0);
            e[1, 1] = new ComplexNumber(5.0, -2.1);
            e[1, 2] = new ComplexNumber(17.0, 0.0);
            e[2, 0] = new ComplexNumber(1.0, 0.0);
            e[2, 1] = new ComplexNumber(2.0, -5.0);
            e[2, 2] = new ComplexNumber(3.0, 4.5);

            Assert.AreEqual(e, u.Conjugate());

            e[0, 0] = new ComplexNumber(6.0, 3.0);
            e[0, 1] = new ComplexNumber(0.0, 0.0);
            e[0, 2] = new ComplexNumber(1.0, 0.0);
            e[1, 0] = new ComplexNumber(2.0, -12.0);
            e[1, 1] = new ComplexNumber(5.0, -2.1);
            e[1, 2] = new ComplexNumber(2.0, -5.0);
            e[2, 0] = new ComplexNumber(0.0, 19.0);
            e[2, 1] = new ComplexNumber(17.0, 0.0);
            e[2, 2] = new ComplexNumber(3.0, 4.5);

            Assert.AreEqual(e, u.Adjoint());
        }


        [TestMethod]
        public void Exercise2o2o6()
        {
            ComplexMatrix u = new ComplexMatrix(3, 3);
            double scalar = 3.2;

            // load up values
            u[0, 0] = new ComplexNumber(6.0, -3.0);
            u[0, 1] = new ComplexNumber(2.0, 12.0);
            u[0, 2] = new ComplexNumber(0.0, -19.0);
            u[1, 0] = new ComplexNumber(0.0, 0.0);
            u[1, 1] = new ComplexNumber(5.0, 2.1);
            u[1, 2] = new ComplexNumber(17.0, 0.0);
            u[2, 0] = new ComplexNumber(1.0, 0.0);
            u[2, 1] = new ComplexNumber(2.0, 5.0);
            u[2, 2] = new ComplexNumber(3.0, -4.5);

            Assert.AreEqual(((dynamic)(scalar * u)).Conjugate(), scalar * u.Conjugate());

        }

        [TestMethod]
        public void Exercise2o2o8()
        {
            ComplexMatrix A = new ComplexMatrix(3, 3);
            ComplexMatrix B = new ComplexMatrix(3, 3);
            ComplexMatrix expected = new ComplexMatrix(3, 3);

            // load up values
            A[0, 0] = new ComplexNumber(3.0, 2.0);
            A[0, 1] = new ComplexNumber(0.0, 0.0);
            A[0, 2] = new ComplexNumber(5.0, -6.0);
            A[1, 0] = new ComplexNumber(1.0, 0.0);
            A[1, 1] = new ComplexNumber(4.0, 2.0);
            A[1, 2] = new ComplexNumber(0.0, 1.0);
            A[2, 0] = new ComplexNumber(4.0, -1.0);
            A[2, 1] = new ComplexNumber(0.0, 0.0);
            A[2, 2] = new ComplexNumber(4.0, 0.0);

            B[0, 0] = new ComplexNumber(5.0, 0.0);
            B[0, 1] = new ComplexNumber(2.0, -1.0);
            B[0, 2] = new ComplexNumber(6.0, -4.0);
            B[1, 0] = new ComplexNumber(0.0, 0.0);
            B[1, 1] = new ComplexNumber(4.0, 5.0);
            B[1, 2] = new ComplexNumber(2.0, 0.0);
            B[2, 0] = new ComplexNumber(7.0, -4.0);
            B[2, 1] = new ComplexNumber(2.0, 7.0);
            B[2, 2] = new ComplexNumber(0.0, 0.0);

            expected[0, 0] = new ComplexNumber(26.0, -52.0);
            expected[0, 1] = new ComplexNumber(60.0, 24.0);
            expected[0, 2] = new ComplexNumber(26.0, 0.0);
            expected[1, 0] = new ComplexNumber(9.0, 7.0);
            expected[1, 1] = new ComplexNumber(1.0, 29.0);
            expected[1, 2] = new ComplexNumber(14.0, 0.0);
            expected[2, 0] = new ComplexNumber(48.0, -21.0);
            expected[2, 1] = new ComplexNumber(15.0, 22.0);
            expected[2, 2] = new ComplexNumber(20.0, -22.0);

            Assert.AreEqual(expected, A * B);

            expected[0, 0] = new ComplexNumber(37.0, -13.0);
            expected[0, 1] = new ComplexNumber(10.0, 0.0);
            expected[0, 2] = new ComplexNumber(50.0, -44.0);
            expected[1, 0] = new ComplexNumber(12.0, 3.0);
            expected[1, 1] = new ComplexNumber(6.0, 28.0);
            expected[1, 2] = new ComplexNumber(3.0, 4.0);
            expected[2, 0] = new ComplexNumber(31.0, 9.0);
            expected[2, 1] = new ComplexNumber(-6.0, 32.0);
            expected[2, 2] = new ComplexNumber(4.0, -60.0);

            Assert.AreEqual(expected, B * A);

        }


        [TestMethod]
        public void Exercise2o2o9()
        {
            ComplexMatrix A = new ComplexMatrix(3, 3);
            ComplexMatrix B = new ComplexMatrix(3, 3);
            ComplexMatrix C = new ComplexMatrix(3, 3);

            // load up values
            A[0, 0] = new ComplexNumber(3.0, 2.0);
            A[0, 1] = new ComplexNumber(0.0, 0.0);
            A[0, 2] = new ComplexNumber(5.0, -6.0);
            A[1, 0] = new ComplexNumber(1.0, 0.0);
            A[1, 1] = new ComplexNumber(4.0, 2.0);
            A[1, 2] = new ComplexNumber(0.0, 1.0);
            A[2, 0] = new ComplexNumber(4.0, -1.0);
            A[2, 1] = new ComplexNumber(0.0, 0.0);
            A[2, 2] = new ComplexNumber(4.0, 0.0);

            B[0, 0] = new ComplexNumber(5.0, 0.0);
            B[0, 1] = new ComplexNumber(2.0, -1.0);
            B[0, 2] = new ComplexNumber(6.0, -4.0);
            B[1, 0] = new ComplexNumber(0.0, 0.0);
            B[1, 1] = new ComplexNumber(4.0, 5.0);
            B[1, 2] = new ComplexNumber(2.0, 0.0);
            B[2, 0] = new ComplexNumber(7.0, -4.0);
            B[2, 1] = new ComplexNumber(2.0, 7.0);
            B[2, 2] = new ComplexNumber(0.0, 0.0);

            C[0, 0] = new ComplexNumber(5.0, 0.3);
            C[0, 1] = new ComplexNumber(-2.2, -1.1);
            C[0, 2] = new ComplexNumber(0.0, 4.2);
            C[1, 0] = new ComplexNumber(2.1, 4.5);
            C[1, 1] = new ComplexNumber(4.3, 5.0);
            C[1, 2] = new ComplexNumber(2.1, 0.0);
            C[2, 0] = new ComplexNumber(7.0, -4.1);
            C[2, 1] = new ComplexNumber(2.5, 7.0);
            C[2, 2] = new ComplexNumber(1.0, 3.0);


            Assert.AreEqual((A * B) * C, A * (B * C));
            Assert.AreEqual(A * (B + C), (A * B) + (A * C));
            Assert.AreEqual((B + C) * A, (B * A) + (C * A));

            double scalar = 3.2;

            Assert.AreEqual(scalar * (A * B), (scalar * A) * B);
            Assert.AreEqual(scalar * (A * B), A * (scalar * B));

            Assert.AreEqual((A * B).Transpose(), B.Transpose() * A.Transpose());
            Assert.AreEqual(((dynamic)(A * B)).Conjugate(), A.Conjugate() * B.Conjugate());
            Assert.AreEqual(((dynamic)(A * B)).Adjoint(), B.Adjoint() * A.Adjoint());

        }

        [TestMethod]
        public void Example2o3o5()
        { 
            // Page 51-52
            DoubleVector B1 = new DoubleVector(2);
            DoubleVector B2 = new DoubleVector(2);

            B1[0] = 1.0;
            B1[1] = -3.0;

            B2[0] = -2.0;
            B2[1] = 4.0;

            DoubleVector D1 = new DoubleVector(2);
            DoubleVector D2 = new DoubleVector(2);

            D1[0] = -7.0;
            D1[1] = 9.0;

            D2[0] = -5.0;
            D2[1] = 7.0;

            DoubleVector V = new DoubleVector(2);

            V[0] = 7.0;
            V[1] = -17.0;

            Assert.AreEqual(V, 3.0 * B1 + -2.0 * B2);

            DoubleVector Vb = new DoubleVector(2);

            Vb[0] = 3.0;
            Vb[1] = -2.0;

            Matrix<double> Mdb = new Matrix<double>(2, 2);

            Mdb[0, 0].Value = 2.0;
            Mdb[0, 1].Value = -1.5;
            Mdb[1, 0].Value = -3.0;
            Mdb[1, 1].Value = 2.5;

            DoubleVector Vd = new DoubleVector(2);

            Vd[0] = 9.0;
            Vd[1] = -14.0;

            Assert.AreEqual(Vd, Mdb * Vb);
            Assert.AreEqual(V, Vd[0] * D1 + Vd[1] * D2);
        }


        [TestMethod]
        public void Example2o3o3()
        {
            // P. 47
            DoubleMatrix H = new DoubleMatrix(2, 2);

            H[0, 0] = 1.0 / Math.Sqrt(2);
            H[0, 1] = 1.0 / Math.Sqrt(2);
            H[1, 0] = 1.0 / Math.Sqrt(2);
            H[1, 1] = -1.0 / Math.Sqrt(2);

            Assert.AreEqual(new DoubleMatrix(DoubleMatrix.IdentityMatrix(2)), H * H); 
        }

        [TestMethod]
        public void Exercise2o4o1()
        {
            // P. 55
            DoubleMatrix V1 = new DoubleMatrix(3);
            V1[0] = 2.0;
            V1[1] = 1.0;
            V1[0] = 3.0;

            DoubleMatrix V2 = new DoubleMatrix(3);
            V2[0] = 6.0;
            V2[1] = 2.0;
            V2[0] = 4.0;

            DoubleMatrix V3 = new DoubleMatrix(3);
            V3[0] = 0.0;
            V3[1] = -1.0;
            V3[0] = 2.0;

            Assert.AreEqual((V1+V2).InnerProduct(V3), V1.InnerProduct(V3) + V2.InnerProduct(V3));
            Assert.AreEqual(V1.InnerProduct(V2 + V3), V1.InnerProduct(V2) + V1.InnerProduct(V3));

        }

        [TestMethod]
        public void Exercise2o4o3()
        {
            // P. 55
            DoubleMatrix A = new DoubleMatrix(2, 2);
            A[0, 0] = 1.0;
            A[0, 1] = 2.0;
            A[1, 0] = 0.0;
            A[1, 1] = 1.0;

            DoubleMatrix B = new DoubleMatrix(2, 2);
            B[0, 0] = 0.0;
            B[0, 1] = -1.0;
            B[1, 0] = -1.0;
            B[1, 1] = 0.0;

            DoubleMatrix C = new DoubleMatrix(2, 2);
            C[0, 0] = 2.0;
            C[0, 1] = 1.0;
            C[1, 0] = 1.0;
            C[1, 1] = 3.0;

            Assert.AreEqual((A + B).InnerProduct(C), A.InnerProduct(C) + B.InnerProduct(C));
            Assert.AreEqual(A.InnerProduct(B + C), A.InnerProduct(B) + A.InnerProduct(C));
        }


        [TestMethod]
        public void Exercise2o4o5()
        {
            // Example 2.4.5 on p. 56
            DoubleVector V1 = new DoubleVector(3);
            V1[0] = 3.0;
            V1[1] = -6.0;
            V1[2] = 2.0;

            Assert.AreEqual(7.0, V1.Length());

            // Exercise 2.4.5 on p. 56
            ComplexVector C1 = new ComplexVector(4);
            C1[0] = new ComplexNumber(4, 3);
            C1[1] = new ComplexNumber(6, -4);
            C1[2] = new ComplexNumber(12, -7);
            C1[3] = new ComplexNumber(0, 13);

            Assert.AreEqual(Math.Sqrt(439), C1.Length());
        }

        [TestMethod]
        public void Exercise2o4o6()
        {
            DoubleMatrix M1 = new DoubleMatrix(2, 2);
            M1[0, 0] = 3.0;
            M1[0, 1] = 5.0;
            M1[1, 0] = 2.0;
            M1[1, 1] = 3.0;

            Assert.AreEqual(Math.Sqrt(47), M1.Length());
        }


        [TestMethod]
        public void Exercise2o4o7()
        {
            double[] components1 = { 3.0, 1.0, 2.0 };
            double[] components2 = { 2.0, 2.0, -1.0 };
            DoubleVector V1 = new DoubleVector(components1);
            DoubleVector V2 = new DoubleVector(components2);

            Assert.AreEqual(Math.Sqrt(11), V1.Distance(V2));
        }



        [TestMethod]
        public void Exercise2o4o8()
        {
            // P. 58
            double[] components1 = { 3.0, -1.0, 0.0 };
            double[] components2 = { 2.0, -2.0, 1.0 };
            DoubleVector V1 = new DoubleVector(components1);
            DoubleVector V2 = new DoubleVector(components2);

            Assert.AreEqual(Math.Acos(0.84327404271156781), V1.GetAngle(V2));
        }

        [TestMethod]
        public void Exercise2o5o1()
        {
            // P. 62 - eigenvalues and eigenvectors
            double[] components1 = { 1.0, 1.0, 0.0 };
            double[] components2 = { 1.0, 0.0, -1.0 };
            double[] components3 = { 1.0, 1.0, 2.0 };
            DoubleVector eigenvector1 = new DoubleVector(components1);
            DoubleVector eigenvector2 = new DoubleVector(components2);
            DoubleVector eigenvector3 = new DoubleVector(components3);

            DoubleMatrix matrix = new DoubleMatrix(3, 3);
            matrix[0, 0] = 1.0;
            matrix[0, 1] = -3.0;
            matrix[0, 2] = 3.0;
            matrix[1, 0] = 3.0;
            matrix[1, 1] = -5.0;
            matrix[1, 2] = 3.0;
            matrix[2, 0] = 6.0;
            matrix[2, 1] = -6.0;
            matrix[2, 2] = 4.0;


            DoubleVector result1, result2, result3;

            result1 = matrix * eigenvector1;
            result2 = matrix * eigenvector2;
            result3 = matrix * eigenvector3;

            Assert.AreEqual(matrix * eigenvector1, -2.0 * eigenvector1);
            Assert.AreEqual(matrix * eigenvector2, -2.0 * eigenvector2);
            Assert.AreEqual(matrix * eigenvector3, 4.0 * eigenvector3);
        }


        [TestMethod]
        public void Exercise2o6o1()
        {
            // Hermitian matrices P. 63
            ComplexMatrix matrix = new ComplexMatrix(3, 3);
            matrix[0, 0] = new ComplexNumber(7.0, 0.0);
            matrix[0, 1] = new ComplexNumber(6.0, 5.0);
            matrix[1, 0] = new ComplexNumber(6.0, -5.0);
            matrix[1, 1] = new ComplexNumber(-3.0, 0.0);

            Assert.AreEqual(matrix, matrix.Adjoint());

        }

        [TestMethod]
        public void Exercise2o6o5()
        {
            // P. 65
            DoubleMatrix matrix = new DoubleMatrix(3, 3);
            matrix[0, 0] = Math.Cos(1.0);
            matrix[0, 1] = -Math.Sin(1.0);
            matrix[0, 2] = 0.0;
            matrix[1, 0] = Math.Sin(1.0);
            matrix[1, 1] = Math.Cos(1.0);
            matrix[1, 2] = 0.0;
            matrix[2, 0] = 0.0;
            matrix[2, 1] = 0.0;
            matrix[2, 2] = 1.0;

            Assert.AreEqual(DoubleMatrix.IdentityMatrix(3), matrix * matrix.Transpose());
            Assert.AreEqual(matrix * matrix.Transpose(), matrix.Transpose() * matrix);
            Assert.IsTrue(matrix.IsUnitary());

        }

        [TestMethod]
        public void Exercise2o6o6()
        {
            // P. 65
            ComplexMatrix matrix = new ComplexMatrix(3, 3);
            matrix[0, 0] = new ComplexNumber(1.0 / 2.0, 1.0 / 2.0);
            matrix[0, 1] = new ComplexNumber(0.0, 1.0 / Math.Sqrt(3.0));
            matrix[0, 2] = new ComplexNumber(3.0 / (2.0 * Math.Sqrt(15.0)), 1.0 / (2.0 * Math.Sqrt(15.0)));
            matrix[1, 0] = new ComplexNumber(-1.0 / 2.0, 0.0);
            matrix[1, 1] = new ComplexNumber(1.0 / Math.Sqrt(3.0), 0.0);
            matrix[1, 2] = new ComplexNumber(4.0 / (2.0 * Math.Sqrt(15.0)), 3.0 / (2.0 * Math.Sqrt(15.0)));
            matrix[2, 0] = new ComplexNumber(1.0 / 2.0, 0.0);
            matrix[2, 1] = new ComplexNumber(0.0, -1.0 / Math.Sqrt(3.0));
            matrix[2, 2] = new ComplexNumber(0.0, 5.0 / (2.0 * Math.Sqrt(15.0)));

            Assert.AreEqual(ComplexMatrix.IdentityMatrix(3), matrix * matrix.Adjoint());
            Assert.AreEqual(matrix * matrix.Adjoint(), matrix.Adjoint() * matrix);
            Assert.IsTrue(matrix.IsUnitary());

        }


        [TestMethod]
        public void Exercise2o6o8()
        {
            // P. 66
            DoubleMatrix matrix = new DoubleMatrix(3, 3);

            matrix[0, 0] = Math.Cos(1.0);
            matrix[0, 1] = -Math.Sin(1.0);
            matrix[0, 2] = 0.0;
            matrix[1, 0] = Math.Sin(1.0);
            matrix[1, 1] = Math.Cos(1.0);
            matrix[1, 2] = 0.0;
            matrix[2, 0] = 0.0;
            matrix[2, 1] = 0.0;
            matrix[2, 2] = 1.0;

            Assert.IsTrue(matrix.IsUnitary());

            DoubleVector V1 = new DoubleVector(3);
            DoubleVector V2 = new DoubleVector(3);
            V1[0] = 3.0;
            V1[1] = 1.0;
            V1[2] = 2.0;

            V2[0] = 2.0;
            V2[1] = 2.0;
            V2[2] = -1.0;

            Assert.AreEqual(Math.Sqrt(11), V1.Distance(V2));
            DoubleVector UV1 = matrix * V1;
            DoubleVector UV2 = matrix * V2;
            double ip1 = V1.InnerProduct(V2);
            double ip2 = UV1.InnerProduct(UV2);

            Assert.AreEqual(V1.Distance(V2), UV1.Distance(UV2));

        }


        [TestMethod]
        public void Exercise2o6o9()
        {
            // P. 66

            ComplexMatrix identity = ComplexMatrix.IdentityMatrix(5);

            Assert.IsTrue(identity.IsUnitary());
            Assert.IsTrue(identity.IsHermitian());
            Assert.IsTrue(identity.IsSymmetric());

            identity = -identity;

            Assert.IsTrue(identity.IsUnitary());
            Assert.IsTrue(identity.IsHermitian());
            Assert.IsTrue(identity.IsSymmetric());
        }



        [TestMethod]
        public void Exercise2o7o1()
        {
            // P. 71
            DoubleVector V1 = new DoubleVector(3);
            DoubleVector V2 = new DoubleVector(2);

            V1[0] = 3.0;
            V1[1] = 4.0;
            V1[2] = 7.0;

            V2[0] = -1.0;
            V2[1] = 2.0;

            DoubleVector expected = new DoubleVector(6);
            expected[0] = -3.0;
            expected[1] = 6.0;
            expected[2] = -4.0;
            expected[3] = 8.0;
            expected[4] = -7.0;
            expected[5] = 14.0;

            Assert.AreEqual(expected, V1.TensorProduct(V2));

        }

        
        [TestMethod]
        public void Exercise2o7o3()
        {
            // P. 72
            ComplexMatrix M1 = new ComplexMatrix(3, 3);
            ComplexMatrix M2 = new ComplexMatrix(3, 3);

            M1[0, 0] = new ComplexNumber(3.0, 2.0);
            M1[0, 1] = new ComplexNumber(5.0, -1.0);
            M1[0, 2] = new ComplexNumber(0.0, 2.0);
            M1[1, 0] = new ComplexNumber(0.0, 0.0);
            M1[1, 1] = new ComplexNumber(12.0, 0.0);
            M1[1, 2] = new ComplexNumber(6.0, -3.0);
            M1[2, 0] = new ComplexNumber(2.0, 0.0);
            M1[2, 1] = new ComplexNumber(4.0, 4.0);
            M1[2, 2] = new ComplexNumber(9.0, 3.0);

            M2[0, 0] = new ComplexNumber(1.0, 0.0);
            M2[0, 1] = new ComplexNumber(3.0, 4.0);
            M2[0, 2] = new ComplexNumber(5.0, -7.0);
            M2[1, 0] = new ComplexNumber(10.0, 2.0);
            M2[1, 1] = new ComplexNumber(6.0, 0.0);
            M2[1, 2] = new ComplexNumber(2.0, 5.0);
            M2[2, 0] = new ComplexNumber(0.0, 0.0);
            M2[2, 1] = new ComplexNumber(1.0, 0.0);
            M2[2, 2] = new ComplexNumber(2.0, 9.0);


            ComplexNumber[,] components = { 
            {new ComplexNumber(3.0, 2.0), new ComplexNumber(1.0, 18.0), new ComplexNumber(29.0, -11.0), new ComplexNumber(5, -1.0), new ComplexNumber(19.0, 17.0), new ComplexNumber(18.0, -40.0), new ComplexNumber(0.0, 2.0), new ComplexNumber(-8.0, 6.0), new ComplexNumber(14.0, 10.0) },
            {new ComplexNumber(26.0, 26.0), new ComplexNumber(18.0, 12.0), new ComplexNumber(-4.0, 19.0), new ComplexNumber(52.0, 0.0), new ComplexNumber(30.0, -6.0), new ComplexNumber(15.0, 23.0), new ComplexNumber(-4.0, 20.0), new ComplexNumber(0.0, 12.0), new ComplexNumber(-10.0, 4.0) },
            {new ComplexNumber(0.0, 0.0), new ComplexNumber(3.0, 2.0), new ComplexNumber(-12.0, 31.0), new ComplexNumber(0.0, 0.0), new ComplexNumber(5.0, -1.0), new ComplexNumber(19.0, 43.0), new ComplexNumber(0.0, 0.0), new ComplexNumber(0.0, 2.0), new ComplexNumber(-18.0, 4.0) },
            {new ComplexNumber(0.0, 0.0), new ComplexNumber(0.0, 0.0), new ComplexNumber(0.0, 0.0), new ComplexNumber(12.0, 0.0), new ComplexNumber(36.0, 48.0), new ComplexNumber(60.0, -84.0), new ComplexNumber(6.0, -3.0), new ComplexNumber(30.0, 15.0), new ComplexNumber(9.0, -57.0) },
            {new ComplexNumber(0.0, 0.0), new ComplexNumber(0.0, 0.0), new ComplexNumber(0.0, 0.0), new ComplexNumber(120.0, 24.0), new ComplexNumber(72.0, 0.0), new ComplexNumber(24.0, 60.0), new ComplexNumber(66.0, -18.0), new ComplexNumber(36.0, -18.0), new ComplexNumber(27.0, 24.0) },            
            {new ComplexNumber(0.0, 0.0), new ComplexNumber(0.0, 0.0), new ComplexNumber(0.0, 0.0), new ComplexNumber(0.0, 0.0), new ComplexNumber(12.0, 0.0), new ComplexNumber(24.0, 108.0), new ComplexNumber(0.0, 0.0), new ComplexNumber(6.0, -3.0), new ComplexNumber(39.0, 48.0) },
            {new ComplexNumber(2.0, 0.0), new ComplexNumber(6.0, 8.0), new ComplexNumber(10.0, -14.0), new ComplexNumber(4.0, 4.0), new ComplexNumber(-4.0, 28.0), new ComplexNumber(48.0, -8.0), new ComplexNumber(9.0, 3.0), new ComplexNumber(15.0, 45.0), new ComplexNumber(66.0, -48.0) },
            {new ComplexNumber(20.0, 4.0), new ComplexNumber(12.0, 0.0), new ComplexNumber(4.0, 10.0), new ComplexNumber(32.0, 48.0), new ComplexNumber(24.0, 24.0), new ComplexNumber(-12.0, 28.0), new ComplexNumber(84.0, 48.0), new ComplexNumber(54.0, 18.0), new ComplexNumber(3.0, 51.0) },
            {new ComplexNumber(0.0, 0.0), new ComplexNumber(2.0, 0.0), new ComplexNumber(4.0, 18.0), new ComplexNumber(0.0, 0.0), new ComplexNumber(4.0, 4.0), new ComplexNumber(-28.0, 44.0), new ComplexNumber(0.0, 0.0), new ComplexNumber(9.0, 3.0), new ComplexNumber(-9.0, 87.0) },            
            };

            ComplexMatrix exected = new ComplexMatrix(components);

            Assert.AreEqual(exected, M1.TensorProduct(M2));
        }


        [TestMethod]
        public void Exercise2o7o4()
        {
            // P. 72
            DoubleMatrix M1 = new DoubleMatrix(2, 2);
            DoubleMatrix M2 = new DoubleMatrix(2, 2);

            M1[0, 0] = 2.0;
            M1[0, 1] = 2.0;
            M1[1, 0] = 2.0;
            M1[1, 1] = 2.0;

            M2[0, 0] = 1.0;
            M2[0, 1] = 2.0;
            M2[1, 0] = 3.0;
            M2[1, 1] = 4.0;

            DoubleMatrix result1 = M1.TensorProduct(M2);
            DoubleMatrix result2 = M2.TensorProduct(M1);

            // Tensor product is "almost" communtative
            // The "nice" rearrangment of rows and columns
            Assert.IsTrue(result1[0, 0] == result2[0, 0] && result1[0, 1] == result2[0, 2] && result1[0, 2] == result2[0, 1] && result1[0, 3] == result2[0, 3]);
            Assert.IsTrue(result1[3, 0] == result2[3, 0] && result1[3, 1] == result2[3, 2] && result1[3, 2] == result2[3, 1] && result1[3, 3] == result2[3, 3]);

            Assert.IsTrue(result1[1, 0] == result2[2, 0] && result1[1, 1] == result2[2, 2] && result1[1, 2] == result2[2, 1] && result1[1, 3] == result2[2, 3]);
            Assert.IsTrue(result1[2, 0] == result2[1, 0] && result1[2, 1] == result2[1, 2] && result1[2, 2] == result2[1, 1] && result1[2, 3] == result2[1, 3]);
        }


        [TestMethod]
        public void Exercise2o7o5()
        {
            // P. 72
            DoubleMatrix M1 = new DoubleMatrix(2, 2);
            DoubleMatrix M2 = new DoubleMatrix(2, 2);
            DoubleMatrix M3 = new DoubleMatrix(2, 2);

            M1[0, 0] = 1.0;
            M1[0, 1] = 2.0;
            M1[1, 0] = 0.0;
            M1[1, 1] = 1.0;

            M2[0, 0] = 3.0;
            M2[0, 1] = 2.0;
            M2[1, 0] = -1.0;
            M2[1, 1] = 0.0;

            M3[0, 0] = 6.0;
            M3[0, 1] = 5.0;
            M3[1, 0] = 3.0;
            M3[1, 1] = 2.0;

            // Tensor product is associative
            Assert.AreEqual(M1.TensorProduct(M2.TensorProduct(M3)), M1.TensorProduct(M2).TensorProduct(M3));
        }



        [TestMethod]
        public void Exercise2o7o7()
        {
            // P. 72
            ComplexMatrix M1 = new ComplexMatrix(2);
            ComplexMatrix M2 = new ComplexMatrix(2, 2);

            // uses the implicit cast for ComplexNumbers
            M1[0] = 2.0;
            M1[0] = 3.0;

            M2[0, 0] = 1.0;
            M2[0, 1] = 2.0;
            M2[1, 0] = 3.0;
            M2[1, 1] = 4.0;

            ComplexMatrix test = M1.TensorProduct(M2);

            // Tensor is commutative with Adjoint
            Assert.AreEqual(M1.TensorProduct(M2).Adjoint(), M1.Adjoint().TensorProduct(M2.Adjoint()));

        }


        [TestMethod]
        public void Exercise2o7o9()
        {
            // P. 72
            DoubleMatrix M1 = new DoubleMatrix(2, 2);
            DoubleMatrix M2 = new DoubleMatrix(2, 2);
            DoubleMatrix M3 = new DoubleMatrix(2, 2);
            DoubleMatrix M4 = new DoubleMatrix(2, 2);

            M1[0, 0] = 1.0;
            M1[0, 1] = 2.0;
            M1[1, 0] = 0.0;
            M1[1, 1] = 1.0;

            M2[0, 0] = 3.0;
            M2[0, 1] = 2.0;
            M2[1, 0] = -1.0;
            M2[1, 1] = 0.0;

            M3[0, 0] = 6.0;
            M3[0, 1] = 5.0;
            M3[1, 0] = 3.0;
            M3[1, 1] = 2.0;

            M4[0, 0] = 4.0;
            M4[0, 1] = 6.0;
            M4[1, 0] = 1.0;
            M4[1, 1] = 3.0;

            // (A * A') Tensor (B * B') == (A Tensor B) * (A' Tensor B')
            Assert.AreEqual((M1 * M2).TensorProduct(M3 * M4), M1.TensorProduct(M3) * M2.TensorProduct(M4));

        }
    
    }
}
