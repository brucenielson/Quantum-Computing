using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinearAlgebra
{
    public class DoubleMatrix : Matrix<double>
    {

        #region constructors


        public DoubleMatrix() : base() { }

        public DoubleMatrix(double[,] components) : base() { }

        public DoubleMatrix(Matrix<double> matrix) : base(matrix) { }

        public DoubleMatrix(params int[] dimensions) : base(dimensions) { }

        #endregion




        #region overrides

        // Implements default Equals to call class specific Equals
        public override bool Equals(object obj)
        {
            if (obj == null) return base.Equals(obj);

            
            // allow for conversions from ComplexMatrix to DoubleMatrix
            if (obj is ComplexMatrix)
            {
                DoubleMatrix converted = (ComplexMatrix)obj;
                return Equals(converted);
            }

            if (!(obj is Matrix<double>))
                throw new InvalidCastException("The 'obj' argument is not a Matrix<double> object.");
            else
                return Equals((Matrix<double>)obj);
        }



        // Implements a hashcode that isn't really guarenteed to be unique
        // but should be relatively close.
        public override int GetHashCode()
        {
            return (int)this.InnerProduct(this);
        }


        
        // This method will be used by the Equals method but allows for 'approximate' comparisions
        // which are necessary for any sort of double or complex math. 
        // This function works because we're assuming == and != operators are overriden
        public override bool Equals(Matrix<double> matrix) 
        {
            if ((object)matrix == null || !this.CompareSize(matrix))
                throw new Exception("Cannot compare two matrices of different dimensions");

            int thisDimension = matrix.Dimensions[0];
            for (int dimension = 0; dimension < thisDimension; dimension++)
            {
                if (matrix[dimension].HasValue)
                {
                    double thisValue = this.Children[dimension].Value;
                    double compareValue = matrix[dimension].Value;

                    double difference = Math.Abs(thisValue - compareValue);
                    double larger = Math.Abs(thisValue) > Math.Abs(compareValue) ? Math.Abs(thisValue) : Math.Abs(compareValue);
                    larger = larger > 0.000001 ? larger : 0.000001;
                    double tolerance = Math.Abs(larger * 0.0001);

                    if (difference > tolerance)
                        return false;
                }
                else
                {
                    if (!this.Children[dimension].Equals(matrix[dimension])) 
                        return false;
                }
            }

            return true;

        }


        // Allows a seemless conversion when you multiply a matrix by a vector
        public static DoubleVector operator *(DoubleMatrix matrix, DoubleVector vector)
        {
            return new DoubleVector((Matrix<double>)matrix * (Matrix<double>)vector);
        }

        // Scalar Divisors
        public static DoubleMatrix operator /(DoubleMatrix matrix, double divisor)
        {
            return (1.0 / divisor) * matrix;
        }

        public static DoubleMatrix operator -(DoubleMatrix matrix)
        {
            return (DoubleMatrix)(-(Matrix<double>)matrix);
        }


        // Same as base class Transpose except returns a ComplexMatrix instead for convenience        
        public new DoubleMatrix Transpose()
        {
            return (DoubleMatrix)(base.Transpose());
        }

        public static DoubleMatrix operator +(DoubleMatrix matrix1, DoubleMatrix matrix2)
        {
            return (DoubleMatrix)((Matrix<double>)matrix1 + (Matrix<double>)matrix2);
        }

        public static DoubleMatrix operator -(DoubleMatrix matrix1, DoubleMatrix matrix2)
        {
            return (DoubleMatrix)((Matrix<double>)matrix1 - (Matrix<double>)matrix2);
        }

        public static DoubleMatrix operator *(double scalarMultiple, DoubleMatrix matrix)
        {
            return (DoubleMatrix)(scalarMultiple * (Matrix<double>)matrix);
        }

        public static DoubleMatrix operator *(DoubleMatrix matrix, double scalarMultiple)
        {
            return (DoubleMatrix)(scalarMultiple * matrix);
        }


        public static DoubleMatrix operator *(DoubleMatrix matrix1, DoubleMatrix matrix2)
        {
            return (DoubleMatrix)((Matrix<double>)matrix1 * (Matrix<double>)matrix2);
        }


        public static DoubleMatrix operator ^(DoubleMatrix matrix, int power)
        {
            return new DoubleMatrix((Matrix<Double>)matrix ^ power);
        }


        public DoubleMatrix TensorProduct(DoubleMatrix matrix)
        {
            return (DoubleMatrix)this.TensorProduct((Matrix<double>)matrix);
        }


        // Indexer -- this will be used like an array
        public new double this[params int[] index]
        {
            get { return base[index].Value; }
            set { base[index].Value = value; }
        }



        #endregion


        #region methods


        // Determines the angle between two vectors if they are vectors made of doubles
        public double GetAngle(DoubleMatrix vector)
        {
            // Only for vectors of equal size
            if (Dimensions.Count() == 1 && vector.Dimensions.Count() == 1 && Dimensions[0] != vector.Dimensions[0])
                throw new Exception("Distance function only works with vectors (i.e. 1 dimensional matrix)");

            // Only for vectors with degree of 2 or 3
            if (Dimensions[0] < 2 || Dimensions[0] > 3)
                throw new Exception("GetAngle only applies to vectors of degree 2 or 3");

            double dotproduct = this.InnerProduct(vector);
            double denominator = this.Length() * vector.Length();

            // Angle between two vectors is based on Cos a = u*v / Length(u) * Length(v)
            return Math.Acos(dotproduct / denominator);
        }


        // verifies this is a legitimate probabilistic system by confirming that rows and columns all equal 1.0 (approx.)
        public bool IsProbabilisticSystem()
        {

            // Only for 2 dimensions or less
            if (Dimensions.Count() > 2)
                throw new Exception("IsProbabilisticSystem() only works on 1 or 2-dimensional matrices");

            // Only works on squares or vectors
            if (Dimensions.Count() == 2 && Dimensions[0] != Dimensions[1])
                throw new Exception("IsProbabilisticSystem() requires either a vector or a square matrix");

            // work with matrices
            if (Dimensions.Count() > 1)
            {
                return IsDoublyStochastic();
            }
            else // work with vectors
            {
                // check rows
                double total = 0.0;
                for (int x = 0; x < Dimensions[0]; x++)
                {
                    total = total + this[x];
                }

                if (!(Math.Abs(1.0 - total) <= 0.001))
                    return false;
                else
                    return true;

            }
        }


        // verifies this is a legitimate probabilistic system by confirming that rows and columns all equal 1.0 (approx.)
        public bool IsDoublyStochastic()
        {

            // Only for 2 dimensions
            if (Dimensions.Count() != 2)
                throw new Exception("IsDoublyStochastic() only works on 2-dimensional matrices");

            // Only works on squares or vectors
            if (Dimensions[0] != Dimensions[1])
                throw new Exception("IsDoublyStochastic() requires a square matrix");

            // check rows
            double total;
            for (int x = 0; x < Dimensions[0]; x++)
            {
                total = 0.0;

                for (int y = 0; y < Dimensions[1]; y++)
                {
                    total = total + this[x, y];
                }
                if (!(Math.Abs(1.0 - total) <= 0.001))
                    return false;
            }


            // check columns
            for (int y = 0; y < Dimensions[0]; y++)
            {
                total = 0.0;

                for (int x = 0; x < Dimensions[1]; x++)
                {
                    total = total + this[x, y];
                }
                if (!(Math.Abs(1.0 - total) <= 0.001))
                    return false;
            }


            // otherwise return true
            return true;        
        }


        #endregion

        #region staticmethods

        // returns the identity matrix for a 2-dimensional matrix
        public static DoubleMatrix IdentityMatrix(int size)
        {
            DoubleMatrix identityMatrix = new DoubleMatrix(size, size);

            for (int i = 0; i < size; i++)
            {
                identityMatrix[i, i] = 1.0;
            }

            return identityMatrix;
        }


        
        
        
      
        // Implicit cast operator for ComplexMatrices into DoubleMatrices.
        // Only works if there are no imaginary numbers. Otherwise throws an error
        public static implicit operator DoubleMatrix(ComplexMatrix value)
        {
            return new DoubleMatrix(Matrix<double>.ComplexToDouble(value));
        }


        #endregion

    }
}
