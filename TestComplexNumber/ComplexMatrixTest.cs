using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LinearAlgebra;

namespace TestComplexNumber
{
    
    
    /// <summary>
    ///This is a test class for ComplexMatrixTest and is intended
    ///to contain all ComplexMatrixTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ComplexMatrixTest
    {

        
        /// <summary>
        ///A test for Conjugate
        ///</summary>
        [TestMethod()]
        public void ConjugateTest()
        {
            ComplexMatrix u = new ComplexMatrix(2, 2);
            ComplexMatrix v = new ComplexMatrix(2, 2);

            // load up values
            u[0, 0] = new ComplexNumber(1.0, 4.0);
            u[0, 1] = new ComplexNumber(2.0, 3.0);
            u[1, 0] = new ComplexNumber(3.0, 2.0);
            u[1, 1] = new ComplexNumber(4.0, 1.0);

            v[0, 0] = new ComplexNumber(1.0, -4.0);
            v[0, 1] = new ComplexNumber(2.0, -3.0);
            v[1, 0] = new ComplexNumber(3.0, -2.0);
            v[1, 1] = new ComplexNumber(4.0, -1.0);

            Assert.AreEqual(v, u.Conjugate());
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void EqualsTest()
        {
            ComplexMatrix u1 = new ComplexMatrix(2, 2);
            ComplexMatrix v = new ComplexMatrix(2, 2);
            ComplexMatrix u2 = new ComplexMatrix(2, 2);

            // load up values
            u1[0, 0] = new ComplexNumber(1.0, 0.0);
            u1[0, 1] = new ComplexNumber(2.0, 0.0);
            u1[1, 0] = new ComplexNumber(3.0, 0.0);
            u1[1, 1] = new ComplexNumber(4.0, 0.0);

            v[0, 0] = new ComplexNumber(4.0, 0.0);
            v[0, 1] = new ComplexNumber(3.0, 0.0);
            v[1, 0] = new ComplexNumber(2.0, 0.0);
            v[1, 1] = new ComplexNumber(1.0, 0.0);

            u2[0, 0] = new ComplexNumber(1.0, 0.0);
            u2[0, 1] = new ComplexNumber(2.0, 0.0);
            u2[1, 0] = new ComplexNumber(3.0, 0.0);
            u2[1, 1] = new ComplexNumber(4.0, 0.0);

            Assert.IsTrue(u1.Equals(u2));
            Assert.IsFalse(u1.Equals(v));
            Assert.IsFalse(u2.Equals(v));
            Assert.IsTrue(u2.Equals(u1));
            Assert.IsFalse(v.Equals(u1));
            Assert.IsFalse(v.Equals(u2));


            ComplexMatrix u = new ComplexMatrix(2);
            ComplexMatrix w = new ComplexMatrix(2);

            
            u[0] = new ComplexNumber(1.00001, 2.00002);
            u[1] = new ComplexNumber(0.99999, 1.99999);

            w[0] = new ComplexNumber(1.0, 2.0);
            w[1] = new ComplexNumber(1.0, 2.0);

            Assert.AreEqual(u, w);

        }


        /// <summary>
        ///A test for Inner Product
        ///</summary>
        [TestMethod()]
        public void InnerProductTest()
        {
            ComplexMatrix matrix1 = new ComplexMatrix(1);
            matrix1[0] = new ComplexNumber(2.0, -2.0);
            ComplexNumber expected = matrix1[0].Conjugate() * matrix1[0]; 
            Assert.AreEqual(expected, matrix1.InnerProduct(matrix1));

            matrix1 = new ComplexMatrix(3);
            matrix1[0] = new ComplexNumber(2.0, -2.0);
            matrix1[1] = new ComplexNumber(2.0, -2.0);
            matrix1[2] = new ComplexNumber(2.0, -2.0);
            Assert.AreEqual(expected * 3, matrix1.InnerProduct(matrix1));

            
            matrix1 = new ComplexMatrix(2, 2);
            matrix1[0, 0] = new ComplexNumber(2.0, -2.0);
            matrix1[0, 1] = new ComplexNumber(2.0, -2.0);
            matrix1[1, 0] = new ComplexNumber(2.0, -2.0);
            matrix1[1, 1] = new ComplexNumber(2.0, -2.0);

            Assert.AreEqual(new ComplexNumber(32.0, 0.0), matrix1.InnerProduct(matrix1));
        }


        /// <summary>
        ///A test for Adjoint
        ///</summary>
        [TestMethod()]
        public void AdjointTest()
        {
            ComplexMatrix u = new ComplexMatrix(2, 3);
            ComplexMatrix e = new ComplexMatrix(3, 2);

            // load up values
            u[0, 0] = new ComplexNumber(1.0, 1.0);
            u[0, 1] = new ComplexNumber(2.0, 2.0);
            u[0, 2] = new ComplexNumber(3.0, 3.0);
            u[1, 0] = new ComplexNumber(4.0, 4.0);
            u[1, 1] = new ComplexNumber(5.0, 5.0);
            u[1, 2] = new ComplexNumber(6.0, 6.0);

            e[0, 0] = new ComplexNumber(1.0, -1.0);
            e[0, 1] = new ComplexNumber(4.0, -4.0);
            e[1, 0] = new ComplexNumber(2.0, -2.0);
            e[1, 1] = new ComplexNumber(5.0, -5.0);
            e[2, 0] = new ComplexNumber(3.0, -3.0);
            e[2, 1] = new ComplexNumber(6.0, -6.0);

            Assert.AreNotEqual(e, u.Transpose());
            Assert.AreEqual(e, u.Adjoint());

        }

        /// <summary>
        ///A test for Transpose
        ///</summary>
        [TestMethod()]
        public void TransposeTest()
        {
            ComplexMatrix u = new ComplexMatrix(2, 3);
            ComplexMatrix e = new ComplexMatrix(3, 2);

            // load up values
            u[0, 0] = new ComplexNumber(1.0, 1.0);
            u[0, 1] = new ComplexNumber(2.0, 2.0);
            u[0, 2] = new ComplexNumber(3.0, 3.0);
            u[1, 0] = new ComplexNumber(4.0, 4.0);
            u[1, 1] = new ComplexNumber(5.0, 5.0);
            u[1, 2] = new ComplexNumber(6.0, 6.0);

            e[0, 0] = new ComplexNumber(1.0, 1.0);
            e[0, 1] = new ComplexNumber(4.0, 4.0);
            e[1, 0] = new ComplexNumber(2.0, 2.0);
            e[1, 1] = new ComplexNumber(5.0, 5.0);
            e[2, 0] = new ComplexNumber(3.0, 3.0);
            e[2, 1] = new ComplexNumber(6.0, 6.0);

            Assert.AreEqual(e, u.Transpose());

            u = new ComplexMatrix(3);
            e = new ComplexMatrix(1, 3);

            // load up values
            u[0] = new ComplexNumber(1.0, 1.0);
            u[1] = new ComplexNumber(2.0, 2.0);
            u[2] = new ComplexNumber(3.0, 3.0);

            e[0, 0] = new ComplexNumber(1.0, 1.0);
            e[0, 1] = new ComplexNumber(2.0, 2.0);
            e[0, 2] = new ComplexNumber(3.0, 3.0);

            Assert.AreEqual(e, u.Transpose());
        }

        /// <summary>
        ///A test for Item
        ///</summary>
        [TestMethod()]
        public void ItemTest()
        {
            ComplexMatrix u = new ComplexMatrix(2,2);
            u[1, 1] = new ComplexNumber(1.1, -2.3);
            Assert.AreEqual(new ComplexNumber(1.1, -2.3), u[1, 1]);
        }


        [TestMethod()]
        public void IdentityMatrixTest()
        {
            ComplexMatrix u = new ComplexMatrix(2, 3);

            // load up values
            u[0, 0] = new ComplexNumber(1.0, -1.0);
            u[0, 1] = new ComplexNumber(2.3, -4.1);
            u[0, 2] = new ComplexNumber(5.6, -7.8);
            u[1, 0] = new ComplexNumber(-2.4, 3.5);
            u[1, 1] = new ComplexNumber(6.7, 19.0);
            u[1, 2] = new ComplexNumber(9.0, 27.3);

            Assert.AreEqual(u, u * ComplexMatrix.IdentityMatrix(3));
            Assert.AreEqual(u, ComplexMatrix.IdentityMatrix(2) * u);

        }

        /// <summary>
        ///A test for op_Multiply
        ///</summary>
        [TestMethod()]
        public void op_MultiplyMatrixAndVectorTest()
        {
            ComplexMatrix A = new ComplexMatrix(2, 2);
            ComplexVector B = new ComplexVector(2);
            ComplexVector expected = new ComplexVector(2);

            // load up values
            A[0, 0] = new ComplexNumber(2.0, 2.0);
            A[0, 1] = new ComplexNumber(2.0, 2.0);
            A[1, 0] = new ComplexNumber(2.0, 2.0);
            A[1, 1] = new ComplexNumber(2.0, 2.0);

            B[0] = new ComplexNumber(2.0, 0.0);
            B[1] = new ComplexNumber(0.0, 2.0);

            expected[0] = (A[0, 0] * B[0]) + (A[0, 1] * B[1]);
            expected[1] = (A[1, 0] * B[0]) + (A[1, 1] * B[1]);

            Assert.AreEqual(expected, A * B);
        }

        /// <summary>
        ///A test for IsHermitian
        ///</summary>
        [TestMethod()]
        public void IsHermitianTest()
        {
            // Hermitian matrices P. 63
            ComplexMatrix matrix = new ComplexMatrix(3, 3);
            matrix[0, 0] = new ComplexNumber(7.0, 0.0);
            matrix[0, 1] = new ComplexNumber(6.0, 5.0);
            matrix[1, 0] = new ComplexNumber(6.0, -5.0);
            matrix[1, 1] = new ComplexNumber(-3.0, 0.0);

            Assert.IsTrue(matrix.IsHermitian());

            matrix[0, 0] = new ComplexNumber(7.0, -1.0);
            Assert.IsFalse(matrix.IsHermitian());

        }

        /// <summary>
        ///A test for op_Implicit
        ///</summary>
        [TestMethod()]
        public void op_ImplicitTest()
        {
            DoubleMatrix DM = new DoubleMatrix(2, 2);
            ComplexMatrix CM = new DoubleMatrix(2, 2);

            DM[0, 0] = 3.0;
            DM[0, 1] = 2.0;
            DM[1, 0] = -1.0;
            DM[1, 1] = 0.0;

            CM = DM;

            Assert.AreEqual(new ComplexNumber(3.0, 0.0), CM[0, 0]);
            Assert.AreEqual(DM, CM);
            Assert.AreEqual(CM, DM);

        }

        /// <summary>
        ///A test for op_ExclusiveOr
        ///</summary>
        [TestMethod()]
        public void op_PowerTest()
        {
            ComplexMatrix M = new ComplexMatrix(6, 6);
            ComplexMatrix M2 = new ComplexMatrix(6, 6);

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

        /// <summary>
        ///A test for ExpectedValue
        ///</summary>
        [TestMethod()]
        public void ExpectedValueTest()
        {
            // Exercise4o2o9 - p. 121-122
            ComplexVector stateVector = new ComplexVector(2);
            stateVector[0] = Math.Sqrt(2) / 2.0;
            stateVector[1] = -Math.Sqrt(2) / 2.0;

            ComplexMatrix omega = new ComplexMatrix(2, 2);
            omega[0, 0] = 3;
            omega[0, 1] = 1 + (2 * ComplexNumber.I);
            omega[1, 0] = 1 - (2 * ComplexNumber.I);
            omega[1, 1] = -1;

            ComplexVector observation = omega * stateVector;
            ComplexNumber probability = stateVector.GetAmplitude(observation);
            ComplexVector test = observation.GetKet() * stateVector;

            Assert.AreEqual(test[0], probability); // no way to verify this.

            ComplexNumber expected = omega.ExpectedValue(stateVector);
            Assert.AreEqual(expected, probability);

        }

        /// <summary>
        ///A test for Variance
        ///</summary>
        [TestMethod()]
        public void VarianceTest()
        {
            ComplexMatrix matrix = new ComplexMatrix(2, 2);

            matrix[0, 0] = -1.5;
            matrix[0, 1] = ComplexNumber.NegativeI;
            matrix[1, 0] = ComplexNumber.I;
            matrix[1, 1] = -0.5;

            ComplexVector stateVector = new ComplexVector(2);
            stateVector[0] = Math.Sqrt(2) / 2.0;
            stateVector[1] = (Math.Sqrt(2) / 2.0) * ComplexNumber.I;

            ComplexNumber result5 = matrix.Variance(stateVector);

            Assert.AreEqual(0.25, result5);
        }
    }
}
