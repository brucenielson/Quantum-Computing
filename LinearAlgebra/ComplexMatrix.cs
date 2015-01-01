using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinearAlgebra
{
    public class ComplexMatrix : Matrix<ComplexNumber>
    {

        #region constructors

        public ComplexMatrix() : base() { }

        public ComplexMatrix(ComplexNumber[,] components) : base(components) { }

        public ComplexMatrix(ComplexNumber[] components) : base(components) { }

        public ComplexMatrix(Matrix<ComplexNumber> matrix) : base(matrix) { }

        public ComplexMatrix(params int[] dimensions) : base(dimensions) { }

        public ComplexMatrix(Matrix<double> matrix) : this(matrix.Dimensions)
        {
            int thisDimension = matrix.Dimensions[0];
            for (int dimension = 0; dimension < thisDimension; dimension++)
            {
                if (matrix[dimension].HasValue)
                    this.Children[dimension].Value = matrix[dimension].Value;
                else
                {
                    Type type = this.GetType();
                    this.Children[dimension] = NewMatrix(type, matrix[dimension]);
                }
            }
        }


        #endregion



        #region overrides

        // Implements default Equals to call class specific Equals
        public override bool Equals(object obj)
        {
            if (obj == null) return base.Equals(obj);

            // Allow conversions from DoubleMatrix to ComplexMatrix
            if (obj is DoubleMatrix)
            {
                ComplexMatrix converted = (DoubleMatrix)obj;
                return Equals(converted);
            }

            if (!(obj is Matrix<ComplexNumber>))
                throw new InvalidCastException("The 'obj' argument is not a Matrix<ComplextNumber> object.");
            else
                return Equals((Matrix<ComplexNumber>)obj);
        }



        // Implements a hashcode that isn't really guarenteed to be unique
        // but should be relatively close.
        public override int GetHashCode()
        {
            return (int)this.InnerProduct(this).Real;
        }

       
        public static ComplexVector operator *(ComplexMatrix matrix, ComplexVector vector)
        {
            return new ComplexVector((Matrix<ComplexNumber>)matrix * (Matrix<ComplexNumber>)vector);
        }


        public static ComplexMatrix operator -(ComplexMatrix matrix)
        {
            return (ComplexMatrix)(-(Matrix<ComplexNumber>)matrix);
        }

        
        // Same as base class Transpose except returns a ComplexMatrix instead for convenience
        public new ComplexMatrix Transpose()
        {
            return (ComplexMatrix)(base.Transpose());
        }

        public static ComplexMatrix operator +(ComplexMatrix matrix1, ComplexMatrix matrix2)
        {
            return (ComplexMatrix)((Matrix<ComplexNumber>)matrix1 + (Matrix<ComplexNumber>)matrix2);
        }

        public static ComplexMatrix operator -(ComplexMatrix matrix1, ComplexMatrix matrix2)
        {
            return (ComplexMatrix)((Matrix<ComplexNumber>)matrix1 - (Matrix<ComplexNumber>)matrix2);
        }


        public static ComplexMatrix operator *(ComplexNumber scalarMultiple, ComplexMatrix matrix)
        {
            return (ComplexMatrix)(scalarMultiple * (Matrix<ComplexNumber>)matrix);
        }

        public static ComplexMatrix operator *(double scalarMultiple, ComplexMatrix matrix)
        {
            return (ComplexMatrix)(scalarMultiple * (Matrix<ComplexNumber>)matrix);
        }


        public static ComplexMatrix operator *(ComplexMatrix matrix1, ComplexMatrix matrix2)
        {
            return (ComplexMatrix)((Matrix<ComplexNumber>)matrix1 * (Matrix<ComplexNumber>)matrix2);
        }

        // Scalar Divisors
        public static ComplexMatrix operator /(ComplexMatrix matrix, double divisor)
        {
            return (ComplexMatrix)((1.0 / divisor) * matrix);
        }

        // Scalar Divisors
        public static ComplexMatrix operator /(ComplexMatrix matrix, ComplexNumber divisor)
        {
            return (ComplexMatrix)((1.0 / divisor) * matrix);
        }



        public static ComplexMatrix operator ^(ComplexMatrix matrix, int power)
        {
            return new ComplexMatrix((Matrix<ComplexNumber>)matrix ^ power);
        }
        

        public ComplexMatrix TensorProduct(ComplexMatrix matrix)
        {
            return (ComplexMatrix)this.TensorProduct((Matrix<ComplexNumber>)matrix);
        }


        // implicit cast operator for complex numbers from doubles
        public static implicit operator ComplexMatrix(DoubleMatrix value)
        {
            return (new ComplexMatrix(value));
        }




        #endregion



        #region method

        // This is a combination of Transpose and Conjugate
        public ComplexMatrix Adjoint()
        {
            ComplexMatrix result = new ComplexMatrix(Dimensions);

            result = ((ComplexMatrix)this.Conjugate().Transpose());

            return result;
        }


        // Is this matrix Hermitian? (i.e. A.Adjoint = A)
        public bool IsHermitian()
        {
            // P. 62
            // Hermitian matrices must be square
            if (!(Dimensions.Count() == 2 && Dimensions[0] == Dimensions[1]))
                throw new Exception("IsHermitian only works a square matrix");

            return (((dynamic)this).Adjoint() == this);
        }



        // Flips the sign of the imaginary part of the number in the matrix
        public ComplexMatrix Conjugate()
        {
            ComplexMatrix result = new ComplexMatrix(Dimensions);

            int thisDimension = this.Dimensions[0];
            for (int dimension = 0; dimension < thisDimension; dimension++)
            {
                if (this.Children[dimension].HasValue)
                {
                    result.Children[dimension].Value = this.Children[dimension].Value.Conjugate();
                }
                else
                {
                    result.Children[dimension] = ((ComplexMatrix)this.Children[dimension]).Conjugate(); 
                }
            }

            return result;
        }

        
        
        // Returns a DoubleMatrix that has the Modulus of each element in the Complex Matrix
        public DoubleMatrix ModulusSquared()
        {
            return new DoubleMatrix(Matrix<ComplexNumber>.TakeModulusSquaredInMatrix(this));
        }


        // Find the Expected Value of matrix Omega depending on State Vector v. Or <Omega>v. See p. 120
        public ComplexNumber ExpectedValue(ComplexVector stateVector)
        {
            // <Omega>v = <omega v, v> = omega v innerproduct v = v.adjoint * omega v - see p. 120
            // This is equivalent to omega v Get Amplitude v
            return (this * stateVector).GetAmplitude(stateVector);
        }


        // Get the Variance of this matrix omega depending on the vector v. Or <(omega - <omega>v) * (omega - <omega>v)>v. See p. 122
        public ComplexNumber Variance(ComplexVector stateVector)
        {
            ComplexMatrix result;

            // Matrices must be square to take a variance
            if (!(Dimensions.Count() == 2 && Dimensions[0] == Dimensions[1]))
                throw new Exception("Variance only works a square matrix");

            // <(omega - <omega>v) * (omega - <omega>v)>v is equivalent to:
            // First find averge value, which is the expected value or <omega>v
            ComplexNumber expectedValue = ExpectedValue(stateVector);

            // now create the diagonal matrix with the expected values to subtract from
            ComplexMatrix mu = new ComplexMatrix(Dimensions);

            for (int i = 0; i < Dimensions[0]; i++)
            {
                mu[i, i] = expectedValue;
            }

            // Now omega - mu
            result = this - mu;

            // Take square
            result = result * result;

            // Get expected value 
            return result.ExpectedValue(stateVector);
        }



        // Indexer -- this will be used like the array it is
        public new ComplexNumber this[params int[] index]
        {
            get { return base[index].Value; }
            set { base[index].Value = value; }
        }



        #endregion


        #region staticmethods

        // returns the identity matrix for a 2-dimensional matrix
        public static ComplexMatrix IdentityMatrix(int size)
        {
            ComplexMatrix identityMatrix = new ComplexMatrix(size, size);

            for (int i = 0; i < size; i++)
            {
                identityMatrix[i, i] = ComplexNumber.One;
            }

            return identityMatrix;
        }

        #endregion


    }
}
