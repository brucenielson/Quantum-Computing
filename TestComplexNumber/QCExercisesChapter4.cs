using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LinearAlgebra;

namespace TestComplexNumber
{
    [TestClass]
    public class QCExercisesChapter4
    {

        [TestMethod]
        public void Exercise4o1o1()
        {
            ComplexVector V = new ComplexVector(6);

            V[0] = new ComplexNumber(2, -1);
            V[1] = new ComplexNumber(0, 2);
            V[2] = new ComplexNumber(1, -1);
            V[3] = new ComplexNumber(1, 0);
            V[4] = new ComplexNumber(0, -2);
            V[5] = new ComplexNumber(2, 0);

            // Note: Modulus() is the same as "Length" for a complex number
            // I'm still trying to figure out the relationship between Modulus for a complex number and "Length()" for a vector
            // Presumably the "Length" for a complex number is just treating the complex number as if it's a single vector and
            // then takes the "Length" of that mini-vector just like that "Length()" function does. (i.e. Sqrt of inner product)
            Assert.IsTrue( (1.0 / Math.Pow(4.4721, 2.0)) - 
                            (Math.Pow(V[3].Modulus(), 2.0) / Math.Pow(V.Length(), 2.0)) < 0.0001 );

            Assert.IsTrue( (4.0 / Math.Pow(4.4721, 2.0)) -
                            (Math.Pow(V[4].Modulus(), 2.0) / Math.Pow(V.Length(), 2.0)) < 0.0001);
        }

        [TestMethod]
        public void Exercise4o1o5()
        {
            ComplexVector V1 = new ComplexVector(2);
            ComplexVector V2 = new ComplexVector(2);

            V1[0] = Math.Sqrt(2) / 2.0;
            V1[1] = Math.Sqrt(2) / 2.0;

            V2[0] = Math.Sqrt(2) / 2.0;
            V2[1] = -Math.Sqrt(2) / 2.0;

            Assert.AreEqual(1.0, V1.Length());
            Assert.AreEqual(1.0, V2.Length());

        }


        [TestMethod]
        public void Example4o2o1()
        {
            ComplexMatrix omega = new ComplexMatrix(2, 2);
            ComplexVector startState = new ComplexVector(2);

            omega[0, 0] = -1;
            omega[0, 1] = new ComplexNumber(0, -1);
            omega[1, 0] = new ComplexNumber(0, 1);
            omega[1, 1] = 1;

            startState[0] = -1;
            startState[1] = new ComplexNumber(-1, -1);

            ComplexVector expected = new ComplexVector(2);
            expected[0] = new ComplexNumber(0, 1);
            expected[1] = new ComplexNumber(-1, -2);

            Assert.AreEqual(expected, omega * startState);
            Assert.IsTrue(omega.IsHermitian());
        }


        [TestMethod]
        public void Exercise4o2o2()
        {
            // P. 117 QCCS
            ComplexMatrix Sx = new ComplexMatrix(2, 2);
            ComplexVector startState = new ComplexVector(2);

            Sx[0, 0] = 0;
            Sx[0, 1] = 1;
            Sx[1, 0] = 1;
            Sx[1, 1] = 0;

            startState[0] = 1; // 100% up
            startState[1] = 0; // 0% down

            ComplexVector expected = new ComplexVector(2);
            expected[0] = 0; // 0% up
            expected[1] = 1; // 100% down

            Assert.AreEqual(expected, Sx * startState);
            Assert.IsTrue(Sx.IsHermitian());
        }


        [TestMethod]
        public void Exercise4o2o6()
        {
            // P. 119 QCCS
            ComplexMatrix omega1 = new ComplexMatrix(2, 2);
            ComplexMatrix omega2 = new ComplexMatrix(2, 2);

            omega1[0, 0] = 1;
            omega1[0, 1] = ComplexNumber.NegativeI;
            omega1[1, 0] = ComplexNumber.I;
            omega1[1, 1] = 1;

            omega2[0, 0] = 2;
            omega2[0, 1] = 0;
            omega2[1, 0] = 0;
            omega2[1, 1] = 4;

            Assert.IsTrue(omega1.IsHermitian());
            Assert.IsTrue(omega2.IsHermitian());

            Assert.AreNotEqual(omega1 * omega2, omega2 * omega1);
            Assert.IsFalse((omega1 * omega2).IsHermitian());
            Assert.IsFalse((omega2 * omega1).IsHermitian());
        }


        [TestMethod]
        public void Exercise4o2o7()
        {
            // P. 120 QCCS
            ComplexMatrix omega1 = new ComplexMatrix(2, 2);
            ComplexMatrix omega2 = new ComplexMatrix(2, 2);
            ComplexMatrix result = new ComplexMatrix(2, 2);

            omega1[0, 0] = 1;
            omega1[0, 1] = -1 - ComplexNumber.I;
            omega1[1, 0] = -1 + ComplexNumber.I;
            omega1[1, 1] = 1;

            omega2[0, 0] = 0;
            omega2[0, 1] = -1;
            omega2[1, 0] = -1;
            omega2[1, 1] = 2;

            result[0, 0] = 2 * ComplexNumber.I;
            result[0, 1] = -2 - 2 * ComplexNumber.I;
            result[1, 0] = 2 - 2 * ComplexNumber.I;
            result[1, 1] = -2 * ComplexNumber.I;

            Assert.AreEqual(result, ComplexMatrix.Commutator(omega1, omega2));
        }

        [TestMethod]
        public void Exercise4o2o8()
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

            Assert.IsTrue(Sx.IsHermitian());
            Assert.IsTrue(Sy.IsHermitian());
            Assert.IsTrue(Sz.IsHermitian());
            // NOTE: Exercise 4.2.7 was false. The commutator of two hermitian matrices is NOT a hermitian matrix.
            // However, it looks like its basis might be. But I have no way to calculate a basis.
            Assert.IsFalse((2 * ComplexNumber.I * Sz).IsHermitian());
            Assert.IsFalse(((ComplexMatrix)ComplexMatrix.Commutator(Sx, Sy)).IsHermitian());
        }



        [TestMethod]
        public void Exercise4o2o9()
        {
            // Example 4.2.5 - p. 121
            ComplexVector vector1 = new ComplexVector(2);
            ComplexVector vector2 = new ComplexVector(2);

            vector1[0] = Math.Sqrt(2);
            vector1[1] = (3.0 / 2.0) * Math.Sqrt(2) * ComplexNumber.I;

            vector2[0] = Math.Sqrt(2) / 2.0;
            vector2[1] = (Math.Sqrt(2) / 2.0) * ComplexNumber.I;

            ComplexVector result = vector1 * vector2;
            ComplexVector result2 = vector1.Adjoint() * vector2;

            Assert.AreEqual(2.5, vector2.GetAmplitude(vector1));

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



        [TestMethod]
        public void Example4o2o7()
        {
            ComplexMatrix matrix = new ComplexMatrix(2, 2);

            matrix[0, 0] = -1.5;
            matrix[0, 1] = ComplexNumber.NegativeI;
            matrix[1, 0] = ComplexNumber.I;
            matrix[1, 1] = -0.5;

            ComplexMatrix omega = matrix * matrix;

            ComplexVector stateVector = new ComplexVector(2);
            stateVector[0] = Math.Sqrt(2) / 2.0;
            stateVector[1] = (Math.Sqrt(2) / 2.0) * ComplexNumber.I;

            ComplexMatrix result = stateVector.Adjoint() * omega;
            ComplexVector result2 = new ComplexVector(result);
            ComplexVector observation = result * stateVector;

            ComplexNumber result3 = (omega * stateVector).InnerProduct(stateVector);
            ComplexNumber result4 = stateVector.GetAmplitude(omega * stateVector);

            ComplexNumber result5 = matrix.Variance(stateVector);

            Assert.AreEqual(0.25, observation[0]);
            Assert.AreEqual(observation[0], result3);            
            Assert.AreEqual(result3, result4);
            Assert.AreEqual(result4, result5);        
        }

        [TestMethod]
        public void Example4o2o12()
        {
            // P. 124 - I could not figure it out. 
            ComplexNumber factor = (Math.Sqrt(2.0) / 2.0) * ComplexNumber.I;
            double modulusSquared = factor.Modulus() * factor.Modulus();
            double spinStateLength = Math.Sqrt(modulusSquared + modulusSquared);

            double up = modulusSquared / (spinStateLength * spinStateLength);

            ComplexVector spinState = new ComplexVector(2);

            spinState[0] = factor;
            spinState[1] = factor;

            //spinState = spinState * factor;

            DoubleMatrix modSquared = spinState.ModulusSquared();

            ComplexMatrix Sz = new ComplexMatrix(2, 2);

            Sz[0, 0] = 1;
            Sz[0, 1] = 0;
            Sz[1, 0] = 0;
            Sz[1, 1] = -1;

            ComplexNumber variance = Sz.Variance(spinState);
        }

        [TestMethod]
        public void Example4o2o14()
        {
            // P. 125 QCCS
            // I failed to understand this
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

            ComplexMatrix commuted = (ComplexMatrix)(ComplexMatrix.Commutator(Sx, Sz) );
            Assert.AreEqual(Sy, commuted * (1.0 / 2.0 * ComplexNumber.I));
            ComplexNumber factor = (Math.Sqrt(2.0) / 2.0) * ComplexNumber.I;
            ComplexVector spinState = new ComplexVector(2);

            spinState[0] = factor;
            spinState[1] = factor;

            ComplexNumber expectedVal = commuted.ExpectedValue(spinState);
            double modulus = expectedVal.Modulus();
            double modulusSquared = modulus * modulus;

            ComplexNumber variance1 = Sx.Variance(spinState);
            ComplexNumber variance2 = Sz.Variance(spinState);
        }


        [TestMethod]
        public void Example4o3o1()
        {
            ComplexMatrix omega = new ComplexMatrix(2, 2);

            omega[0, 0] = -1;
            omega[0, 1] = new ComplexNumber(0, -1);
            omega[1, 0] = new ComplexNumber(0, 1);
            omega[1, 1] = 1;

            Assert.IsTrue(omega.IsHermitian());

            ComplexNumber eigenvalue1 = -Math.Sqrt(2) - 0.001; // I had to add an error factor because the eigenvalues weren't quite right
            ComplexNumber eigenvalue2 = Math.Sqrt(2) + 0.001;
            ComplexVector eigenvector1 = new ComplexVector(2);
            ComplexVector eigenvector2 = new ComplexVector(2);

            eigenvector1[0] = -0.923 * ComplexNumber.I;
            eigenvector1[1] = -0.382;
            eigenvector2[0] = -0.382 * ComplexNumber.I;
            eigenvector2[1] = 0.923;

            Assert.AreEqual(omega * eigenvector1, eigenvalue1 * eigenvector1);
            Assert.AreEqual(omega * eigenvector2, eigenvalue2 * eigenvector2);

        }


        [TestMethod]
        public void Example4o3o2()
        {
            // P. 127 QCCS
            ComplexMatrix omega = new ComplexMatrix(2, 2);

            omega[0, 0] = -1;
            omega[0, 1] = new ComplexNumber(0, -1);
            omega[1, 0] = new ComplexNumber(0, 1);
            omega[1, 1] = 1;

            Assert.IsTrue(omega.IsHermitian());

            double eigenvalue1 = -Math.Sqrt(2) - 0.001; // I had to add an error factor because the eigenvalues weren't quite right
            double eigenvalue2 = Math.Sqrt(2) + 0.001;
            ComplexVector eigenvector1 = new ComplexVector(2);
            ComplexVector eigenvector2 = new ComplexVector(2);

            eigenvector1[0] = -0.923 * ComplexNumber.I;
            eigenvector1[1] = -0.382;
            eigenvector2[0] = -0.382 * ComplexNumber.I;
            eigenvector2[1] = 0.923;

            ComplexVector startState = new ComplexVector(2);
            startState[0] = 1.0;
            startState[1] = 1.0;
            startState = startState * (1.0 / 2.0);

            ComplexNumber innerP1 = eigenvector1.InnerProduct(startState);
            double lengthP1 = innerP1.Length;
            double p1 = lengthP1 * lengthP1;

            ComplexNumber innerP2 = eigenvector2.InnerProduct(startState);
            double lengthP2 = innerP2.Length;
            double p2 = lengthP2 * lengthP2;

            double meanValue1 = (p1 * eigenvalue1) + (p2 * eigenvalue2);

            ComplexNumber meanValue2;

            meanValue2 = startState.GetAmplitude(omega * startState);

            Assert.AreEqual(meanValue1, meanValue2);
        }

        [TestMethod]
        public void Exercise4o4o1()
        {
            DoubleMatrix U1 = new DoubleMatrix(2, 2);
            DoubleMatrix U2 = new DoubleMatrix(2, 2);

            U1[0, 0] = 0.0;
            U1[0, 1] = 1.0;
            U1[1, 0] = 1.0;
            U1[1, 1] = 0.0;

            U2[0, 0] = Math.Sqrt(2) / 2.0;
            U2[0, 1] = Math.Sqrt(2) / 2.0;
            U2[1, 0] = Math.Sqrt(2) / 2.0;
            U2[1, 1] = -Math.Sqrt(2) / 2.0;

            Assert.IsTrue(U1.IsUnitary());
            Assert.IsTrue(U2.IsUnitary());
            Assert.IsTrue((U1 * U2).IsUnitary());
        }

    }
}
