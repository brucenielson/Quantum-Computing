using LinearAlgebra;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestComplexNumber
{
    
    
    /// <summary>
    ///This is a test class for MatrixTest and is intended
    ///to contain all MatrixTest Unit Tests
    ///</summary>
    [TestClass()]
    public class MatrixTest
    {

        [TestMethod()]
        public void MatrixConstructorTest()
        {
            Matrix<double> target = new Matrix<double>(2, 3, 4, 5);
            // load up values
            target[0, 0, 0, 0].Value = 1.0;
            target[1, 1, 1, 1].Value = 2.0;
            target[0, 2, 2, 2].Value = 3.0;
            target[0, 0, 3, 3].Value = 4.0;
            target[0, 0, 0, 4].Value = 5.0;

            Assert.AreEqual(1.0, target[0, 0, 0, 0].Value);
            Assert.AreEqual(2.0, target[1, 1, 1, 1].Value);
            Assert.AreEqual(3.0, target[0, 2, 2, 2].Value);
            Assert.AreEqual(4.0, target[0, 0, 3, 3].Value);
            Assert.AreEqual(5.0, target[0, 0, 0, 4].Value);

        }

        [TestMethod()]
        public void op_UnaryNegationTest()
        {
            Matrix<double> target = new Matrix<double>(2, 2);
            // load up values
            target[0, 0].Value = 1.0;
            target[0, 1].Value = 2.0;
            target[1, 0].Value = 3.0;
            target[1, 1].Value = 4.0;

            Assert.AreEqual(-1.0 * target, -target);
        }



        [TestMethod()]
        public void op_SubtractionTest()
        {
            Matrix<double> u = new Matrix<double>(2, 2);
            Matrix<double> v = new Matrix<double>(2, 2);
            Matrix<double> e = new Matrix<double>(2, 2);
            
            // load up values
            u[0, 0].Value = 1.0;
            u[0, 1].Value = 2.0;
            u[1, 0].Value = 3.0;
            u[1, 1].Value = 4.0;

            v[0, 0].Value = 4.0;
            v[0, 1].Value = 3.0;
            v[1, 0].Value = 2.0;
            v[1, 1].Value = 1.0;

            e[0, 0].Value = -3.0;
            e[0, 1].Value = -1.0;
            e[1, 0].Value = 1.0;
            e[1, 1].Value = 3.0;

            Assert.AreEqual(e, u - v);
        }



        [TestMethod()]
        public void op_AdditionTest()
        {
            Matrix<double> u = new Matrix<double>(2, 2);
            Matrix<double> v = new Matrix<double>(2, 2);
            Matrix<double> e = new Matrix<double>(2, 2);

            // load up values
            u[0, 0].Value = 1.0;
            u[0, 1].Value = 2.0;
            u[1, 0].Value = 3.0;
            u[1, 1].Value = 4.0;

            v[0, 0].Value = 4.0;
            v[0, 1].Value = 3.0;
            v[1, 0].Value = 2.0;
            v[1, 1].Value = 1.0;

            e[0, 0].Value = 5.0;
            e[0, 1].Value = 5.0;
            e[1, 0].Value = 5.0;
            e[1, 1].Value = 5.0;

            Assert.AreEqual(e, u + v);
        }



        [TestMethod()]
        public void op_MultiplyTest()
        {
            Matrix<double> u = new Matrix<double>(2, 2);
            Matrix<ComplexNumber> uC = new Matrix<ComplexNumber>(2, 2);
            double scalarDouble = 2.0;
            ComplexNumber scalarComplex = new ComplexNumber(2.0, 0.0);
            Matrix<double> e = new Matrix<double>(2, 2);
            Matrix<ComplexNumber> eC = new Matrix<ComplexNumber>(2, 2);

            // load up values
            u[0, 0].Value = 1.0;
            u[0, 1].Value = 2.0;
            u[1, 0].Value = 3.0;
            u[1, 1].Value = 4.0;

            uC[0, 0].Value = new ComplexNumber(1.0, 1.0);
            uC[0, 1].Value = new ComplexNumber(2.0, 2.0);
            uC[1, 0].Value = new ComplexNumber(3.0, 3.0);
            uC[1, 1].Value = new ComplexNumber(4.0, 4.0);

            e[0, 0].Value = 2.0;
            e[0, 1].Value = 4.0;
            e[1, 0].Value = 6.0;
            e[1, 1].Value = 8.0;

            eC[0, 0].Value = new ComplexNumber(2.0, 2.0);
            eC[0, 1].Value = new ComplexNumber(4.0, 4.0);
            eC[1, 0].Value = new ComplexNumber(6.0, 6.0);
            eC[1, 1].Value = new ComplexNumber(8.0, 8.0);

            Assert.AreEqual(e, u * scalarDouble);
            Assert.AreEqual(eC, uC * scalarComplex);
            Assert.AreEqual(e, scalarDouble * u);
            Assert.AreEqual(eC, scalarComplex * uC);
        }

        

        [TestMethod()]
        public void op_InequalityTest()
        {
            Matrix<double> u1 = new Matrix<double>(2, 2);
            Matrix<double> v = new Matrix<double>(2, 2);
            Matrix<double> u2 = new Matrix<double>(2, 2);

            // load up values
            u1[0, 0].Value = 1.0;
            u1[0, 1].Value = 2.0;
            u1[1, 0].Value = 3.0;
            u1[1, 1].Value = 4.0;

            v[0, 0].Value = 4.0;
            v[0, 1].Value = 3.0;
            v[1, 0].Value = 2.0;
            v[1, 1].Value = 1.0;

            u2[0, 0].Value = 1.0;
            u2[0, 1].Value = 2.0;
            u2[1, 0].Value = 3.0;
            u2[1, 1].Value = 4.0;

            Assert.IsFalse(u1 != u2);
            Assert.IsTrue(u1 != v);
            Assert.IsTrue(u2 != v);
            Assert.IsFalse(u2 != u1);
            Assert.IsTrue(v != u1);
            Assert.IsTrue(v != u2);
        }


        
        [TestMethod()]
        public void op_EqualityTest()
        {
            Matrix<double> u1 = new Matrix<double>(2, 2);
            Matrix<double> v = new Matrix<double>(2, 2);
            Matrix<double> u2 = new Matrix<double>(2, 2);

            // load up values
            u1[0, 0].Value = 1.0;
            u1[0, 1].Value = 2.0;
            u1[1, 0].Value = 3.0;
            u1[1, 1].Value = 4.0;

            v[0, 0].Value = 4.0;
            v[0, 1].Value = 3.0;
            v[1, 0].Value = 2.0;
            v[1, 1].Value = 1.0;

            u2[0, 0].Value = 1.0;
            u2[0, 1].Value = 2.0;
            u2[1, 0].Value = 3.0;
            u2[1, 1].Value = 4.0;

            Assert.IsTrue(u1 == u2);
            Assert.IsFalse(u1 == v);
            Assert.IsFalse(u2 == v);
            Assert.IsTrue(u2 == u1);
            Assert.IsFalse(v == u1);
            Assert.IsFalse(v == u2);
        }


        
        [TestMethod()]
        public void EqualsTest()
        {
            Matrix<double> u1 = new Matrix<double>(2, 2);
            Matrix<double> v = new Matrix<double>(2, 2);
            Matrix<double> u2 = new Matrix<double>(2, 2);

            // load up values
            u1[0, 0].Value = 1.0;
            u1[0, 1].Value = 2.0;
            u1[1, 0].Value = 3.0;
            u1[1, 1].Value = 4.0;

            v[0, 0].Value = 4.0;
            v[0, 1].Value = 3.0;
            v[1, 0].Value = 2.0;
            v[1, 1].Value = 1.0;

            u2[0, 0].Value = 1.0;
            u2[0, 1].Value = 2.0;
            u2[1, 0].Value = 3.0;
            u2[1, 1].Value = 4.0;

            Assert.IsTrue(u1.Equals(u2));
            Assert.IsFalse(u1.Equals(v));
            Assert.IsFalse(u2.Equals(v));
            Assert.IsTrue(u2.Equals(u1));
            Assert.IsFalse(v.Equals(u1));
            Assert.IsFalse(v.Equals(u2));
        }

        

        [TestMethod()]
        public void CompareSizeTest()
        {
            Matrix<double> u1 = new Matrix<double>(2, 2);
            Matrix<double> v = new Matrix<double>(3, 3);
            Matrix<double> u2 = new Matrix<double>(2, 2);

            Assert.IsTrue(u1.CompareSize(u2));
            Assert.IsFalse(u1.CompareSize(v));
            Assert.IsFalse(u2.CompareSize(v));
            Assert.IsTrue(u2.CompareSize(u1));
            Assert.IsFalse(v.CompareSize(u1));
            Assert.IsFalse(v.CompareSize(u2));
        }



        [TestMethod()]
        public void TransposeTest()
        {
            Matrix<double> u = new Matrix<double>(2, 3);
            Matrix<double> e = new Matrix<double>(3, 2);

            // load up values
            u[0, 0].Value = 1.0;
            u[0, 1].Value = 2.0;
            u[0, 2].Value = 3.0;
            u[1, 0].Value = 4.0;
            u[1, 1].Value = 5.0;
            u[1, 2].Value = 6.0;

            e[0, 0].Value = 1.0;
            e[0, 1].Value = 4.0;
            e[1, 0].Value = 2.0;
            e[1, 1].Value = 5.0;
            e[2, 0].Value = 3.0;
            e[2, 1].Value = 6.0;

            Assert.AreEqual(e, u.Transpose());

            u = new Matrix<double>(3);
            e = new Matrix<double>(1, 3);

            // load up values
            u[0].Value = 1.0;
            u[1].Value = 2.0;
            u[2].Value = 3.0;

            e[0, 0].Value = 1.0;
            e[0, 1].Value = 2.0;
            e[0, 2].Value = 3.0;

            Assert.AreEqual(e, u.Transpose());
        }


        [TestMethod()]
        public void op_MultiplyTest1()
        {
            Matrix<ComplexNumber> A = new Matrix<ComplexNumber>(3, 3);
            Matrix<ComplexNumber> B = new Matrix<ComplexNumber>(3, 3);
            Matrix<ComplexNumber> expected = new Matrix<ComplexNumber>(3, 3);

            // load up values
            A[0, 0].Value = new ComplexNumber(3.0, 2.0);
            A[0, 1].Value = new ComplexNumber(0.0, 0.0);
            A[0, 2].Value = new ComplexNumber(5.0, -6.0);
            A[1, 0].Value = new ComplexNumber(1.0, 0.0);
            A[1, 1].Value = new ComplexNumber(4.0, 2.0);
            A[1, 2].Value = new ComplexNumber(0.0, 1.0);
            A[2, 0].Value = new ComplexNumber(4.0, -1.0);
            A[2, 1].Value = new ComplexNumber(0.0, 0.0);
            A[2, 2].Value = new ComplexNumber(4.0, 0.0);

            B[0, 0].Value = new ComplexNumber(5.0, 0.0);
            B[0, 1].Value = new ComplexNumber(2.0, -1.0);
            B[0, 2].Value = new ComplexNumber(6.0, -4.0);
            B[1, 0].Value = new ComplexNumber(0.0, 0.0);
            B[1, 1].Value = new ComplexNumber(4.0, 5.0);
            B[1, 2].Value = new ComplexNumber(2.0, 0.0);
            B[2, 0].Value = new ComplexNumber(7.0, -4.0);
            B[2, 1].Value = new ComplexNumber(2.0, 7.0);
            B[2, 2].Value = new ComplexNumber(0.0, 0.0);

            expected[0, 0].Value = new ComplexNumber(26.0, -52.0);
            expected[0, 1].Value = new ComplexNumber(60.0, 24.0);
            expected[0, 2].Value = new ComplexNumber(26.0, 0.0);
            expected[1, 0].Value = new ComplexNumber(9.0, 7.0);
            expected[1, 1].Value = new ComplexNumber(1.0, 29.0);
            expected[1, 2].Value = new ComplexNumber(14.0, 0.0);
            expected[2, 0].Value = new ComplexNumber(48.0, -21.0);
            expected[2, 1].Value = new ComplexNumber(15.0, 22.0);
            expected[2, 2].Value = new ComplexNumber(20.0, -22.0);


            Assert.AreEqual(expected, A * B);


            Matrix<double> C = new Matrix<double>(2, 3);
            Matrix<double> D = new Matrix<double>(3, 2);
            Matrix<double> result = new Matrix<double>(2, 2);

            // load up values
            C[0, 0].Value = 2.0;
            C[0, 1].Value = 2.0;
            C[0, 2].Value = 2.0;
            C[1, 0].Value = 2.0;
            C[1, 1].Value = 2.0;
            C[1, 2].Value = 2.0;

            D[0, 0].Value = 2.0;
            D[0, 1].Value = 2.0;
            D[1, 0].Value = 2.0;
            D[1, 1].Value = 2.0;
            D[2, 0].Value = 2.0;
            D[2, 1].Value = 2.0;

            result[0, 0].Value = 12.0;
            result[0, 1].Value = 12.0;
            result[1, 0].Value = 12.0;
            result[1, 1].Value = 12.0;

            Assert.AreEqual(result, C * D);

            result = D * C;
            
            result = new Matrix<double>(3, 3);

            result[0, 0].Value = 8.0;
            result[0, 1].Value = 8.0;
            result[0, 2].Value = 8.0;
            result[1, 0].Value = 8.0;
            result[1, 1].Value = 8.0;
            result[1, 2].Value = 8.0;
            result[2, 0].Value = 8.0;
            result[2, 1].Value = 8.0;
            result[2, 2].Value = 8.0;

            Assert.AreEqual(result, D * C);

            
            A = new Matrix<ComplexNumber>(2, 3);
            B = new Matrix<ComplexNumber>(3, 2);
            expected = new Matrix<ComplexNumber>(2, 2);

            // load up values
            A[0, 0].Value = new ComplexNumber(2.0, 2.0);
            A[0, 1].Value = new ComplexNumber(2.0, 2.0);
            A[0, 2].Value = new ComplexNumber(2.0, 2.0);
            A[1, 0].Value = new ComplexNumber(2.0, 2.0);
            A[1, 1].Value = new ComplexNumber(2.0, 2.0);
            A[1, 2].Value = new ComplexNumber(2.0, 2.0);

            B[0, 0].Value = new ComplexNumber(2.0, 2.0);
            B[0, 1].Value = new ComplexNumber(2.0, 2.0);
            B[1, 0].Value = new ComplexNumber(2.0, 2.0);
            B[1, 1].Value = new ComplexNumber(2.0, 2.0);
            B[2, 0].Value = new ComplexNumber(2.0, 2.0);
            B[2, 1].Value = new ComplexNumber(2.0, 2.0);

            expected[0, 0].Value = new ComplexNumber(0.0, 24.0);
            expected[0, 1].Value = new ComplexNumber(0.0, 24.0);
            expected[1, 0].Value = new ComplexNumber(0.0, 24.0);
            expected[1, 1].Value = new ComplexNumber(0.0, 24.0);

            Assert.AreEqual(expected, A * B);

            expected = new Matrix<ComplexNumber>(3, 3);

            expected[0, 0].Value = new ComplexNumber(0.0, 16.0);
            expected[0, 1].Value = new ComplexNumber(0.0, 16.0);
            expected[0, 2].Value = new ComplexNumber(0.0, 16.0);
            expected[1, 0].Value = new ComplexNumber(0.0, 16.0);
            expected[1, 1].Value = new ComplexNumber(0.0, 16.0);
            expected[1, 2].Value = new ComplexNumber(0.0, 16.0);
            expected[2, 0].Value = new ComplexNumber(0.0, 16.0);
            expected[2, 1].Value = new ComplexNumber(0.0, 16.0);
            expected[2, 2].Value = new ComplexNumber(0.0, 16.0);            
        }



        [TestMethod()]
        public void op_MultiplyMatrixWithVector()
        {
            DoubleVector Vb = new DoubleVector(2);

            Vb[0] = 2.0;
            Vb[1] = -2.0;

            Matrix<double> Mdb = new Matrix<double>(2, 2);

            Mdb[0, 0].Value = 2.0;
            Mdb[0, 1].Value = -1.5;
            Mdb[1, 0].Value = -3.0;
            Mdb[1, 1].Value = 2.5;

            DoubleVector Vd = new DoubleVector(2);

            Vd[0] = 7.0;
            Vd[1] = -11.0;

            Assert.AreEqual(Vd, Mdb * Vb);

            Vb[0] = 3.0;
            Vb[1] = -2.0;

            Vd[0] = 12.0;
            Vd[1] = -9.5;

            Assert.AreEqual(Vd, Vb * Mdb);

            // Now try out multiplying two vectors together - do we end up with a single value?
            DoubleMatrix Md = new DoubleMatrix(1);
            Md[0] = 13.0;

            DoubleMatrix Mb = new DoubleMatrix(2);
            Mb[0] = 3.0;
            Mb[1] = -2.0;

            // 3*3 + -2*-2 = 13
            Assert.AreEqual(Md, Mb * Mb);
            // should make no difference if we transpose it or not
            Assert.AreEqual(Md, Mb.Transpose() * Mb);
        }



        [TestMethod()]
        public void IdentityMatrixTest()
        {
            Matrix<double> u = new Matrix<double>(2, 3);

            // load up values
            u[0, 0].Value = 1.0;
            u[0, 1].Value = 2.0;
            u[0, 2].Value = 3.0;
            u[1, 0].Value = 4.0;
            u[1, 1].Value = 5.0;
            u[1, 2].Value = 6.0;

            Assert.AreEqual(u, u * DoubleMatrix.IdentityMatrix(3));
            Assert.AreEqual(u, DoubleMatrix.IdentityMatrix(2) * u);

        }



        
        [TestMethod()]
        [DeploymentItem("LinearAlgebra.dll")]
        public void TraceTest()
        {
            Matrix_Accessor<double> target = new Matrix_Accessor<double>(1);
            target[0].Value = 1.0;

            Assert.AreEqual(1.0, target.Trace());

            target = new Matrix_Accessor<double>(3);
            target[0].Value = 1.0;
            target[1].Value = 1.0;
            target[2].Value = 1.0;

            Assert.AreEqual(3.0, target.Trace());

            target = new Matrix_Accessor<double>(2,2);
            target[0, 0].Value = 1.0;
            target[0, 1].Value = 1.0;
            target[1, 0].Value = 1.0;
            target[1, 1].Value = 1.0;

            Assert.AreEqual(2.0, target.Trace());
        }



        [TestMethod()]
        public void InnerProductTest()
        {
            Matrix<double> matrix1 = new Matrix<double>(1);
            matrix1[0].Value = 2.0;
            Assert.AreEqual(4.0, matrix1.InnerProduct(matrix1));

            matrix1 = new Matrix<double>(3);
            matrix1[0].Value = 2.0;
            matrix1[1].Value = 2.0;
            matrix1[2].Value = 2.0;
            Assert.AreEqual(12.0, matrix1.InnerProduct(matrix1));

            matrix1 = new Matrix<double>(2, 2);
            matrix1[0, 0].Value = 2.0;
            matrix1[0, 1].Value = 2.0;
            matrix1[1, 0].Value = 2.0;
            matrix1[1, 1].Value = 2.0;

            Matrix<double> matrix2 = new Matrix<double>(2, 2);
            matrix2[0, 0].Value = 3.0;
            matrix2[0, 1].Value = 3.0;
            matrix2[1, 0].Value = 3.0;
            matrix2[1, 1].Value = 3.0;

            Assert.AreEqual(24.0, matrix1.InnerProduct(matrix2));
            Assert.AreEqual(24.0, matrix2.InnerProduct(matrix1));

        }


        [TestMethod()]
        public void TensorProductTest()
        {
            DoubleVector V1 = new DoubleVector(2);
            DoubleVector V2 = new DoubleVector(3);

            V1[0] = 2.0;
            V1[1] = 3.0;

            V2[0] = 4.0;
            V2[1] = 6.0;
            V2[2] = 3.0;

            DoubleVector expected = new DoubleVector(6);
            expected[0] = 8.0;
            expected[1] = 12.0;
            expected[2] = 6.0;
            expected[3] = 12.0;
            expected[4] = 18.0;
            expected[5] = 9.0;

            Assert.AreEqual(expected, V1.TensorProduct(V2));


            DoubleMatrix M1 = new DoubleMatrix(2,2);
            DoubleMatrix M2 = new DoubleMatrix(2,2);

            M1[0, 0] = 2.0;
            M1[0, 1] = 2.0;
            M1[1, 0] = 2.0;
            M1[1, 1] = 2.0;

            M2[0, 0] = 1.0;
            M2[0, 1] = 2.0;
            M2[1, 0] = 3.0;
            M2[1, 1] = 4.0;

            DoubleMatrix expected2 = new DoubleMatrix(4,4);
            expected2[0, 0] = 2.0;
            expected2[0, 1] = 4.0;
            expected2[0, 2] = 2.0;
            expected2[0, 3] = 4.0;
            expected2[1, 0] = 6.0;
            expected2[1, 1] = 8.0;
            expected2[1, 2] = 6.0;
            expected2[1, 3] = 8.0;
            expected2[2, 0] = 2.0;
            expected2[2, 1] = 4.0;
            expected2[2, 2] = 2.0;
            expected2[2, 3] = 4.0;
            expected2[3, 0] = 6.0;
            expected2[3, 1] = 8.0;
            expected2[3, 2] = 6.0;
            expected2[3, 3] = 8.0;

            Assert.AreEqual(expected2, M1.TensorProduct(M2));
        }


        [TestMethod()]
        public void op_PowerTest()
        {
            DoubleMatrix M = new DoubleMatrix(6, 6);
            DoubleMatrix M2 = new DoubleMatrix(6, 6);

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

            M2[0, 0] = M2[0, 1] = M2[0, 2] = M2[0, 3] = M2[0, 4] = M2[0, 5] = 0.0;
            M2[1, 0] = M2[1, 1] = M2[1, 2] = M2[1, 3] = M2[1, 4] = M2[1, 5] = 0.0;
            M2[2, 0] = 1.0;
            M2[2, 1] = 0.0;
            M2[2, 2] = 0.0;
            M2[2, 3] = 0.0;
            M2[2, 4] = 1.0;
            M2[2, 5] = 0.0;
            M2[3, 0] = 0.0;
            M2[3, 1] = 0.0;
            M2[3, 2] = 0.0;
            M2[3, 3] = 1.0;
            M2[3, 4] = 0.0;
            M2[3, 5] = 0.0;
            M2[4, 0] = 0.0;
            M2[4, 1] = 1.0;
            M2[4, 2] = 0.0;
            M2[4, 3] = 0.0;
            M2[4, 4] = 0.0;
            M2[4, 5] = 1.0;
            M2[5, 0] = 0.0;
            M2[5, 1] = 0.0;
            M2[5, 2] = 1.0;
            M2[5, 3] = 0.0;
            M2[5, 4] = 0.0;
            M2[5, 5] = 0.0;

            Assert.AreEqual(M2, M ^ 2);
        }

        [TestMethod()]
        [DeploymentItem("LinearAlgebra.dll")]
        public void TakeModulusSquaredTest()
        {
            ComplexMatrix C = new ComplexMatrix(2, 2);
            DoubleMatrix D = new DoubleMatrix(2, 2);
            DoubleMatrix result = new DoubleMatrix(2, 2);

            C[0, 0] = new ComplexNumber(1 / Math.Sqrt(2), 0);
            C[0, 1] = new ComplexNumber(1 / Math.Sqrt(2), 0);
            C[1, 0] = new ComplexNumber(0, -1 / Math.Sqrt(2));
            C[1, 1] = new ComplexNumber(0, 1 / Math.Sqrt(2));

            result[0, 0] = 0.5;
            result[0, 1] = 0.5;
            result[1, 0] = 0.5;
            result[1, 1] = 0.5;

            // Try implicit conversion
            D = new DoubleMatrix(Matrix_Accessor<ComplexNumber>.TakeModulusSquaredInMatrix(C));

            Assert.AreEqual(result, D);
        }


        [TestMethod()]
        [DeploymentItem("LinearAlgebra.dll")]
        public void ComplexToDoubleTest()
        {
            DoubleMatrix D = new DoubleMatrix(2, 2);
            DoubleMatrix result = new DoubleMatrix(2, 2);

            D[0, 0] = 1 / Math.Sqrt(2);
            D[0, 1] = 1 / Math.Sqrt(2);
            D[1, 0] = -1 / Math.Sqrt(2);
            D[1, 1] = 1 / Math.Sqrt(2);

            D[0, 0] = D[0, 0] * D[0, 0];
            D[0, 1] = D[0, 1] * D[0, 1];
            D[1, 0] = D[1, 0] * D[1, 0];
            D[1, 1] = D[1, 1] * D[1, 1];

            result[0, 0] = 0.5;
            result[0, 1] = 0.5;
            result[1, 0] = 0.5;
            result[1, 1] = 0.5;

            Assert.AreEqual(result, D);
        }

        [TestMethod()]
        public void CommutatorTest()
        {
            ComplexMatrix Sx = new ComplexMatrix(2, 2);
            ComplexMatrix Sy = new ComplexMatrix(2, 2);
            ComplexMatrix Sz = new ComplexMatrix(2, 2);
            ComplexVector startState = new ComplexVector(2);

            Sx[0, 0] = 0;
            Sx[0, 1] = 1;
            Sx[1, 0] = 1;
            Sx[1, 1] = 0;

            Sy[0, 0] = 0;
            Sy[0, 1] = ComplexNumber.NegativeI;
            Sy[1, 0] = ComplexNumber.I;
            Sy[1, 1] = 0;

            Sz[0, 0] = 1;
            Sz[0, 1] = 0;
            Sz[1, 0] = 0;
            Sz[1, 1] = -1;

            Assert.AreEqual(2.0 * ComplexNumber.I * Sz, ComplexMatrix.Commutator(Sx, Sy));
            Assert.AreEqual(2.0 * ComplexNumber.I * Sx, ComplexMatrix.Commutator(Sy, Sz));
            Assert.AreEqual(2.0 * ComplexNumber.I * Sy, ComplexMatrix.Commutator(Sz, Sx));

        }

        /// <summary>
        ///A test for op_Division
        ///</summary>
        public void op_DivisionTestHelper<T>()
        {
            Matrix<T> matrix = null; // TODO: Initialize to an appropriate value
            T divisor = default(T); // TODO: Initialize to an appropriate value
            Matrix<T> expected = null; // TODO: Initialize to an appropriate value
            Matrix<T> actual;
            actual = (matrix / divisor);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void op_DivisionTest()
        {
            Matrix<double> u = new Matrix<double>(2, 2);
            Matrix<ComplexNumber> uC = new Matrix<ComplexNumber>(2, 2);
            double scalarDouble = 2.0;
            ComplexNumber scalarComplex = new ComplexNumber(2.0, 0.0);
            Matrix<double> e = new Matrix<double>(2, 2);
            Matrix<ComplexNumber> eC = new Matrix<ComplexNumber>(2, 2);

            // load up values
            u[0, 0].Value = 1.0;
            u[0, 1].Value = 2.0;
            u[1, 0].Value = 3.0;
            u[1, 1].Value = 4.0;

            uC[0, 0].Value = new ComplexNumber(1.0, 1.0);
            uC[0, 1].Value = new ComplexNumber(2.0, 2.0);
            uC[1, 0].Value = new ComplexNumber(3.0, 3.0);
            uC[1, 1].Value = new ComplexNumber(4.0, 4.0);

            e[0, 0].Value = 0.5;
            e[0, 1].Value = 1.0;
            e[1, 0].Value = 1.5;
            e[1, 1].Value = 2.0;

            eC[0, 0].Value = new ComplexNumber(0.5, 0.5);
            eC[0, 1].Value = new ComplexNumber(1.0, 1.0);
            eC[1, 0].Value = new ComplexNumber(1.5, 1.5);
            eC[1, 1].Value = new ComplexNumber(2.0, 2.0);

            Assert.AreEqual(e, u / scalarDouble);
            Assert.AreEqual(eC, uC / scalarComplex);
        }

    }
}
