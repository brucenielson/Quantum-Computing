using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace LinearAlgebra
{
    // This class implements the composite pattern as taught to me by Phil Gilmore
    public class Matrix<T>
    {

        #region Proprerties

        public T Value { get; set; }
        protected List<Matrix<T>> Children { get; set; } 
        public bool HasValue { get { return Children.Count == 0; } }
        public int[] Dimensions { get; set; }

        #endregion


        #region constructors

        public Matrix()
        {
            Children = new List<Matrix<T>>();
        }

        public Matrix(params int[] dimensions) : this()
        {

            // Children is the first element.  It has been created for us.  We must add a collection for each of the first dimensions specified.  Example: for new MatrixElement(6, 7, 8, 9), we add 6 items to the first collection

            if (dimensions.Length > 0)
            {
                Dimensions = dimensions;

                int thisDimension = dimensions[0];
                for (int dimension = 0; dimension < thisDimension; dimension++)
                {
                    // Recursive construction is a nice perk of the composite pattern.  
                    // Skip 1 to avoid stack overflow.  It passes the same values as the current 
                    // recursion level except it omits the first value  [0] in the dimensions array 
                    // and passes only elements 1 through n-1.  Eventually, the last one will have only one element.
                    // In the constructor, we only actually USE element [0], so in this way the recursive constructor
                    // cycles through the dimensions array as it goes down.

                    Type type = this.GetType();
                    int[] nextDimension = dimensions.Skip(1).ToArray<int>();
                    Matrix<T> newElement;
                    if (nextDimension.Length > 0)
                    {
                        newElement = NewMatrix(type, nextDimension);
                    }
                    else
                    {
                        newElement = new Matrix<T>(dimensions.Skip(1).ToArray<int>());
                    }
                    Children.Add(newElement);
                }
            }
        }

        
        // Create a two dimensional matrix (a common form) by passing a 2-dimensional array of components
        public Matrix(T[,] components) : this(components.GetLength(0), components.GetLength(1))
        {
            for (int x = 0; x < components.GetLength(0); x++)
            {
                for (int y = 0; y < components.GetLength(1); y++)
                {
                    this[x, y].Value = components[x, y];
                }
            }
        }


        // Create a one dimensional matrix (a common form) by passing a 1-dimensional array of components
        public Matrix(T[] components)
            : this(components.GetLength(0))
        {
            for (int x = 0; x < components.GetLength(0); x++)
            {
                this[x].Value = components[x];
            }
        }


        public Matrix(Matrix<T> matrix) : this(matrix.Dimensions)
        {
            int thisDimension = matrix.Dimensions[0];
            for (int dimension = 0; dimension < thisDimension; dimension++)
            {
                if (matrix.Children[dimension].HasValue)
                    this.Children[dimension].Value = matrix[dimension].Value;
                else
                {
                    Type type = this.GetType();
                    this.Children[dimension] = NewMatrix(type, matrix[dimension]);
                }
            }
        }


        #endregion


        #region methods


        public Matrix<T> TensorProduct(Matrix<T> matrix)
        {
            if ((object)matrix == null)
                throw new Exception("matrix cannot be null.");

            if (this.Dimensions.Length > 2 || matrix.Dimensions.Length > 2)
                throw new Exception("TensorProduct is only for 1 or 2-dimensional matrices");

            // Dynamically typing the output
            Matrix<T> result;
            Type type = this.GetType();
            int rows1, rows2, columns1, columns2;
            rows1 = this.Dimensions[0];
            rows2 = matrix.Dimensions[0];

            if (this.Dimensions.Length == 1)
                columns1 = 1;
            else
                columns1 = this.Dimensions[1];

            if (matrix.Dimensions.Length == 1)
                columns2 = 1;
            else
                columns2 = matrix.Dimensions[1];

            int rows, columns;
            rows = rows1 * rows2;
            columns = columns1 * columns2;

            // Create result matrix
            if (columns == 1)
                result = NewMatrix(type, rows);
            else
                result = NewMatrix(type, rows, columns);


            int rowcount=0, columncount=0;

            // Loop through first matrix
            for (int x1 = 0; x1 < rows1; x1++)
            {
                for (int y1 = 0; y1 < columns1; y1++)
                {
                    // Loop through second matrix
                    for (int x2 = 0; x2 < rows2; x2++)
                    {
                        rowcount = x1*rows2 + x2;
                        for (int y2 = 0; y2 < columns2; y2++)
                        {
                            columncount = y1*columns2 + y2;
                            if (columns == 1)
                                result[rowcount].Value = (dynamic)this[x1].Value * matrix[x2].Value;
                            else
                            {
                                // handle if some of these are vectors
                                if (this.Dimensions.Length == 1 && matrix.Dimensions.Length == 1)
                                    result[rowcount, columncount].Value = (dynamic)this[x1].Value * matrix[x2].Value;
                                else if (this.Dimensions.Length == 1)
                                    result[rowcount, columncount].Value = (dynamic)this[x1].Value * matrix[x2, y2].Value;
                                else if (matrix.Dimensions.Length == 1)
                                    result[rowcount, columncount].Value = (dynamic)this[x1, y1].Value * matrix[x2].Value;
                                else // No vectors, so just handle it normally
                                    result[rowcount, columncount].Value = (dynamic)this[x1, y1].Value * matrix[x2, y2].Value;
                            }
                        }
                    }
                }
            }

            return result;
        }



        // Find the Inner Product (aka Dot Product) for two Matrices
        public T InnerProduct(Matrix<T> matrix)
        {
            // An "Inner Product" is the same as a Dot Product
            // For two vectors (1-dimensional matrix) you multiply them together and then add the results to get 
            // a final scalar. i.e. {2, -2} dot {2, -2} = 2*2 + -2*-2 = 8
            // For a 2-dimensional matrix, you first multiply the matrixes together (after transposing the first one) 
            // and then take the "Trace" or in other words only add up the diagonal.
            // One gotcha -- if this is a complex number matrix, you have to take the Adjoint instead of the transpose
            // See Quantum Computing for Computer Scientists p. 54-55

            // For this to work the number of columns in A must be equal to the number of rows in B
            // And, this is solely a 1 or 2-dimensional operation (at least for now) 
            if (Dimensions.Count() > 2 || matrix.Dimensions.Count() > 2)
                throw new Exception("Inner Products only works on 1 or 2-dimensional matrices");

            // Take the product of the two matrices
            Matrix<T> product;

            if (this is ComplexMatrix || this is ComplexVector)
                product = ((dynamic)this).Adjoint() * matrix;
            else
                product = this.Transpose() * matrix;

            // Now take the Trace (i.e. add up the diagonal if it has one)
            return product.Trace();
        }



        // Take the Trace (i.e. add up the diagonal) for the Matrix. Used for Inner Product
        protected T Trace()
        {
            // A Trace is only for 2 dimensions or less
            if (Dimensions.Count() > 2)
                throw new Exception("Trace only works on 1 or 2-dimensional matrices");

            // Trace only works on squares or vectors
            if (Dimensions.Count() == 2 && Dimensions[0] != Dimensions[1])
                throw new Exception("Trace requires either a vector or a square matrix");

            // Handle the situation with a single dimension for x and y. Just return the one value we have
            if (Dimensions.Count() == 1 && Dimensions[0] == 1)
                return this[0].Value;

            T result = default(T);

            // Handle the situation where we have a single dimension. i.e. a vector
            if (Dimensions.Count() == 1)
            {
                for (int i = 0; i < Dimensions[0]; i++)
                {
                    result = result + (dynamic)this[i].Value;
                }
            }
            else
            {
                // Handle two dimensions
                for (int i = 0; i < Dimensions[0]; i++)
                {
                    result = result + (dynamic)this[i, i].Value;
                }
            }
            return result;
        }


        // Is this matrix unitary? (i.e. it's inverse is it's Adjoint)
        public bool IsUnitary()
        {
            // Only square matrices can possibly be Unitary
            if (! (Dimensions.Count() == 2 && Dimensions[0] == Dimensions[1]) )
                throw new Exception("IsUnitary only works for a square matrix");

            if (this is ComplexMatrix)
            {
                return (this * ((dynamic)this).Adjoint() == ((dynamic)this).Adjoint() * this &&
                    this * ((dynamic)this).Adjoint() == ComplexMatrix.IdentityMatrix(Dimensions[0]));
            }
            else
                return (this * this.Transpose() == this.Transpose() * this && 
                    this * (dynamic)this.Transpose() == DoubleMatrix.IdentityMatrix(Dimensions[0]));

        }


        // Is this matrix Symmetric? (i.e. A.Transpose = A)
        public bool IsSymmetric()
        {
            // Symmetric matrices must be square
            if (!(Dimensions.Count() == 2 && Dimensions[0] == Dimensions[1]))
                throw new Exception("IsSymmetric only works a square matrix");

                return (this.Transpose() == this);            
        }



        // For vectors (i.e. 1-dimensional matrix) the length is the Sqrt of the inner product of itself. P. 56
        public double Length()
        {
            dynamic result = this.InnerProduct(this);

            if (this is Matrix<ComplexNumber>)
            {
                ComplexNumber cnResult = (ComplexNumber)result;
                if (cnResult.Imaginary == 0.0)
                {
                    double retResult = cnResult.Real;
                    return (dynamic)Math.Sqrt(retResult);
                }
                else
                    throw new Exception("Length function does not work correctly with Complex Number results.");
            }
            else
                return Math.Sqrt((dynamic)this.InnerProduct(this));
        }


        // Determine if two vectors are orthogonal (perpendicular) to each other
        public bool IsOrthogonal(Matrix<T> vector)
        {
            if ((dynamic)this.InnerProduct(vector) == default(T))
                return true;
            else
                return false;
        }



        // For vectors (i.e. 1-dimensional matrix) the distance between two vectors is Vector 1 - Vector 2 then take length
        public double Distance(Matrix<T> vector)
        {
            // Only for vectors of equal size
            if (Dimensions.Count() == 1 && vector.Dimensions.Count() == 1 && Dimensions[0] != vector.Dimensions[0])
                throw new Exception("Distance function only works with vectors (i.e. 1 dimensional matrix)");

            return (this - vector).Length();
        }


        // If this is a 2-dimensional matrix, then the transpose of it simply flips the x and y coordinates
        // If it's a 1-dimensional matrix, then put it into a 2-d matrix flipped
        public Matrix<T> Transpose()
        {
            Matrix<T> transposed;
            if (Dimensions.Count() == 2)
            {
                Type type = this.GetType();
                int[] args = { Dimensions[1], Dimensions[0] };
                transposed = NewMatrix(type, args);

                for (int x = 0; x < Dimensions[0]; x++)
                {
                    for (int y = 0; y < Dimensions[1]; y++)
                    {
                        transposed[y, x].Value = this[x, y].Value;
                    }
                }
            }
            else if (this is IVector<T>)
            {
                // If we are transposing something that is specifically a vector, then just return the same vector
                // Don't bother to turn it in this case.
                return this;
            }
            else if (Dimensions.Count() == 1)
            {
                Type type = this.GetType();
                int[] args = { 1, Dimensions[0] };
                transposed = NewMatrix(type, args);

                for (int x = 0; x < Dimensions[0]; x++)
                {
                    transposed[0, x].Value = this[x].Value;
                }
            }
            else
                throw new Exception("Cannot transpose a matrix with more than 2 dimensions");

            return transposed;
        }



        public bool CompareSize(Matrix<T> matrix)
        {
            if ((object)matrix == null)
                throw new Exception("Matrix cannot be null.");

            for (int i = 0; i < Dimensions.Count(); i++)
            {
                if (this.Dimensions[i] != matrix.Dimensions[i])
                    return false;
            }

            return true;
        }


        public Matrix<T> this[params int[] index] 
        {
            get
            {
                Matrix<T> selected = this;
                foreach (int i in index)
                {
                    if (i >= selected.Children.Count() || i < 0)
                        throw new Exception("Out of bounds error");

                    selected = selected.Children[i];
                }

                return selected;
            }

            set
            {
                Matrix<T> selected = this;
                foreach (int i in index)
                {
                    selected = selected.Children[i];
                }

                selected = value;
            }
        }


        
        // This method will be used by the Equals method but allows for 'approximate' comparisions
        // which are necessary for any sort of double or complex math. 
        // This function works because we're assuming == and != operators are overriden
        public virtual bool Equals(Matrix<T> matrix)
        {
            if ((object)matrix == null || !this.CompareSize(matrix))
                throw new Exception("Cannot compare two matrices of different dimensions");

            int thisDimension = matrix.Dimensions[0];
            for (int dimension = 0; dimension < thisDimension; dimension++)
            {
                if (matrix[dimension].HasValue)
                {
                    if ((dynamic)this.Children[dimension].Value != (dynamic)matrix[dimension].Value)
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




        #endregion


        #region override


        // Pass a call to Equals to our more class specific version
        public override bool Equals(object obj)
        {
            if (obj == null) return base.Equals(obj);

            if (!(obj is Matrix<T>))
                throw new InvalidCastException("The 'obj' argument is not a Matrix<T> object.");
            else
                return Equals((Matrix<T>)obj);
        }



        // A sort of generic override of GetHashCode to get rid of the warning
        // TODO: fix this to really work
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }


        // Output in string format
        public override string ToString()
        {
            string retValue = "";

            int thisDimension = Dimensions[0];
            for (int dimension = 0; dimension < thisDimension; dimension++)
            {
                if (Children[dimension].HasValue)
                {
                    if (retValue != "")
                        retValue = retValue + ", ";

                    retValue = retValue + this[dimension].Value.ToString();
                }
                else
                {
                    if (retValue != "")
                    {
                        if (retValue.First() == '{')
                            retValue = "\r\n" + retValue;
                    }

                    retValue = retValue + this.Children[dimension].ToString();
                }
            }
            
            return "{" + retValue + "}\r\n";
        }

        #endregion




        #region operators


        // matrix addition
        public static Matrix<T> operator +(Matrix<T> matrix1, Matrix<T> matrix2)
        {
            if ((object)matrix1 == null || (object)matrix2 == null)
                throw new Exception("Matrices cannot be null.");

            if (!matrix1.CompareSize(matrix2))
                throw new Exception("Cannot add two Matrices of different dimensions");

            // Dynamically typing the output
            Matrix<T> result;
            Type matrix1Type = matrix1.GetType();
            result = NewMatrix(matrix1Type, matrix1.Dimensions);

            int thisDimension = matrix1.Dimensions[0];
            for (int dimension = 0; dimension < thisDimension; dimension++)
            {
                if (matrix1.Children[dimension].HasValue)
                    result.Children[dimension].Value = (dynamic)matrix1[dimension].Value + (dynamic)matrix2[dimension].Value;
                else
                    result.Children[dimension] = matrix1[dimension] + matrix2[dimension];
            }

            return result;
        }


        
        // matrix subtraction
        public static Matrix<T> operator -(Matrix<T> matrix1, Matrix<T> matrix2)
        {
            if ((object)matrix1 == null || (object)matrix2 == null)
                throw new Exception("Matrices cannot be null.");

            if (!matrix1.CompareSize(matrix2))
                throw new Exception("Cannot add two Matrices of different dimensions");

            // Dynamically typing the output
            Matrix<T> result;
            Type matrix1Type = matrix1.GetType();
            result = NewMatrix(matrix1Type, matrix1.Dimensions);

            int thisDimension = matrix1.Dimensions[0];
            for (int dimension = 0; dimension < thisDimension; dimension++)
            {
                if (matrix1.Children[dimension].HasValue)
                    result.Children[dimension].Value = (dynamic)matrix1[dimension].Value - (dynamic)matrix2[dimension].Value;
                else
                    result.Children[dimension] = matrix1[dimension] - matrix2[dimension];
            }

            return result;
        }



        // matrix inverse
        public static Matrix<T> operator -(Matrix<T> matrix)
        {
            return -1.0 * matrix;
        }

        
        // Matrix equals
        public static bool operator ==(Matrix<T> matrix1, Matrix<T> matrix2)
        {
            if ((object)matrix1 == null || (object)matrix2 == null)
                throw new Exception("Matrices cannot be null.");

            if (matrix1.CompareSize(matrix2) )
            {
                int thisDimension = matrix1.Dimensions[0];
                for (int dimension = 0; dimension < thisDimension; dimension++)
                {
                    if (matrix1.Children[dimension].HasValue)
                    {
                        if (! ApproximatelyEqual(matrix1[dimension].Value, matrix2[dimension].Value) )
                            return false;
                    }
                    else
                    {
                        if (matrix1[dimension] != matrix2[dimension])
                            return false;
                    }
                }

                // all were approximately equal
                return true;
            }
            else
                // they aren't even the same size, so they can't be equal
                return false;
        }



        // Matrix does not equal
        public static bool operator !=(Matrix<T> matrix1, Matrix<T> matrix2)
        {
            return !(matrix1 == matrix2);
        }
        

        // Scalar multiples
        public static Matrix<T> operator *(T scalarMultiple, Matrix<T> matrix)
        {

            if ((object)matrix == null)
                throw new Exception("Matrix cannot be null.");

            // Dynamically typing the output
            Matrix<T> result;
            Type matrixType = matrix.GetType();
            result = NewMatrix(matrixType, matrix.Dimensions);

            int thisDimension = matrix.Dimensions[0];
            for (int dimension = 0; dimension < thisDimension; dimension++)
            {
                if (matrix.Children[dimension].HasValue)
                    result.Children[dimension].Value = scalarMultiple * (dynamic)matrix[dimension].Value;
                else
                    result.Children[dimension] = scalarMultiple * matrix[dimension];
            }

            return result;
        }


        // TODO Do I need this? It's a total duplicate of the above!
        // Scalar multiples
        public static Matrix<T> operator *(double scalarMultiple, Matrix<T> matrix)
        {
            if ((object)matrix == null)
                throw new Exception("Matrix cannot be null.");

            // Dynamically typing the output
            Matrix<T> result;
            Type matrixType = matrix.GetType();            
            result = NewMatrix(matrixType, matrix.Dimensions);

            int thisDimension = matrix.Dimensions[0];
            for (int dimension = 0; dimension < thisDimension; dimension++)
            {
                if (matrix.Children[dimension].HasValue)
                    result.Children[dimension].Value = scalarMultiple * (dynamic)matrix[dimension].Value;
                else
                    result.Children[dimension] = scalarMultiple * matrix[dimension];
            }

            return result;
        }



        // Scalar multiples
        public static Matrix<T> operator *(Matrix<T> matrix, T scalarMultiple)
        {
            return scalarMultiple * matrix;
        }


        // TODO Do I need this?
        // Scalar multiples
        public static Matrix<T> operator *(Matrix<T> matrix, double scalarMultiple)
        {
            return scalarMultiple * matrix;
        }


        // Scalar Divisors
        public static Matrix<T> operator /(Matrix<T> matrix, double divisor)
        {
            return (1.0 / divisor) * matrix;
        }

        // Scalar Divisors
        public static Matrix<T> operator /(Matrix<T> matrix, T divisor)
        {
            T scalarMultiple = (1.0 / (dynamic)divisor); 
            return  scalarMultiple * matrix;
        }


        // Matrix Multiplication
        public static Matrix<T> operator *(Matrix<T> matrix1, Matrix<T> matrix2)
        {
            // Matrix multiplication is done by, given matrices A and B multiplying each element in A[x,-]
            // with the appropriate element in B[-,y] and summing the results. P. 40

            // For this to work the number of columns in A must be equal to the number of rows in B
            // And, this is solely a 1 or 2-dimensional operation (at least for now) 
            if (matrix1.Dimensions.Count() > 2 || matrix2.Dimensions.Count() > 2)
                throw new Exception("Matrix multiplication only works on 1 or 2-dimensional matrices");


            // Dynamically typing the output
            Matrix<T> twoDVector = null;
            Type matrix1Type, matrix2Type;
            matrix1Type = matrix1.GetType();
            matrix2Type = matrix2.GetType();
            // handle vectors - convert a vector for matrix1 from vector[x] to matrix[1,x]
            if (matrix1.Dimensions.Count() == 1)
            {
                int[] parms = { 1, matrix1.Dimensions[0] };
                twoDVector = NewMatrix(matrix1Type, parms);

                for (int i = 0; i < matrix1.Dimensions[0]; i++)
                {
                    twoDVector[0, i].Value = matrix1[i].Value;
                }
                matrix1 = twoDVector;
            }

            // handle vectors - convert a vector for matrix2 from vector[x] to matrix[x,1]
            if (matrix2.Dimensions.Count() == 1)
            {
                if (matrix2 is DoubleVector)
                    matrix2Type = typeof(DoubleMatrix);
                else if (matrix2 is ComplexVector)
                    matrix2Type = typeof(ComplexMatrix);
                else if (matrix2 is IVector<T>)
                    matrix2Type = typeof(Matrix<T>);

                int[] parms = { matrix2.Dimensions[0], 1 };
                twoDVector = NewMatrix(matrix2Type, parms);
                for (int i = 0; i < matrix2.Dimensions[0]; i++)
                {
                    twoDVector[i, 0].Value = matrix2[i].Value;
                }
                matrix2 = twoDVector;
            }


            // Dynamically typing the output
            Matrix<T> result;
            matrix1Type = matrix1.GetType();
            int[] args;
            if (matrix1 is IVector<T>)
            {
                args = new int[1];
                args[0] = matrix1.Dimensions[0];
            }
            else
            {
                args = new int[2];
                args[0] = matrix1.Dimensions[0];
                args[1] = matrix2.Dimensions[1];
            }
            result = NewMatrix(matrix1Type, args);


            for (int x = 0; x < matrix1.Dimensions[0]; x++)
            {
                for (int y = 0; y < matrix2.Dimensions[1]; y++)
                {
                    T product = default(T);

                    for (int i = 0; i < matrix1.Dimensions[1]; i++)
                    {
                        product = product + ((dynamic)matrix1[x, i].Value * (dynamic)matrix2[i, y].Value);
                    }
                    result[x, y].Value = product;
                }
            }

            // handle vectors - if the result of this has a dimension of 1 for the 2nd dimension, 
            // this convert back to a vector for matrix2. I.e. from matrix[x,1] to vector[x]
            if (matrix2.Dimensions[1] == 1)
            {
                Matrix<T> resultVector = NewMatrix(matrix2.GetType(), result.Dimensions[0]);
                for (int i = 0; i < result.Dimensions[0]; i++)
                {
                    resultVector[i].Value = result[i, 0].Value;
                }
                result = resultVector;
            }

            // handle vectors - if the result of this has a dimension of 1 for the 2nd dimension, 
            // this convert back to a vector for matrix1. I.e. from matrix[1,x] to vector[x]
            // Don't bother with this is we're already down to a vector with only 1 dimension. Then we're done.
            if (matrix1.Dimensions[0] == 1 && result.Dimensions.Count() > 1)
            {
                Matrix<T> resultVector = NewMatrix(matrix1.GetType(), result.Dimensions[1]);
                for (int i = 0; i < result.Dimensions[1]; i++)
                {
                    resultVector[i].Value = result[0, i].Value;
                }
                result = resultVector;
            }

            
            return result;
        }
       

        #endregion



        // Matrix power function
        public static Matrix<T> operator ^(Matrix<T> matrix, int power)
        {
            Matrix<T> result = new Matrix<T>(matrix);

            for (int i = 1; i < power; i++)
            {
                result = result * matrix;
            }
            return result;
        }


        #region staticmethods


        // Creates a new Matrix of type using args
        protected static Matrix<T> NewMatrix(Type type, params object[] args)
        {
            // deal with types
            if (type.Name.Contains("Vector"))
            {
                // only bother 'down casting' if I'm trying to pass a Vector but with multiple parameters
                if (args.Length > 1 || (args[0] is int[] && ((int[])args[0]).Length > 1))
                {
                    if (type.Name == "DoubleVector")
                        type = typeof(DoubleMatrix);
                    else if (type.Name == "ComplexVector")
                        type = typeof(ComplexMatrix);
                    else
                        type = typeof(Matrix<T>);
                }
            }

            if (args.Length > 1 && args[0] is int)
            {
                // handle if we get passed an int[]
                return (Matrix<T>)Activator.CreateInstance(type, args);
            }
            else if (args.Length == 1 && args[0] is int[])
            {
                // This is just a single int inside
                if (((int[])args[0]).Length == 1)
                {
                    // handle if we get passed a single int
                    int arg = (int)((int[])args[0])[0];
                    return (Matrix<T>)Activator.CreateInstance(type, arg);
                }
                else // handle if we get passed int[] as the first parameter
                    return (Matrix<T>)Activator.CreateInstance(type, args[0]);
            }
            else if (args.Length == 1 && args[0] is int)
            {
                // handle if we get passed a single int
                int arg = (int)args[0];
                return (Matrix<T>)Activator.CreateInstance(type, arg);
            }
            else if (args.Length == 1 && args[0] is Matrix<T>)
            {
                return (Matrix<T>)Activator.CreateInstance(type, args[0]);
            }
            else
                return (Matrix<T>)Activator.CreateInstance(type, args);
        }

                

        // convert from Matrix<ComplexNumber> to Matrix<double> using Modulus Squared
        protected static Matrix<double> TakeModulusSquaredInMatrix(Matrix<T> matrix)
        {
            Matrix<double> result = new Matrix<double>(matrix.Dimensions);

            int thisDimension = matrix.Dimensions[0];
            for (int dimension = 0; dimension < thisDimension; dimension++)
            {
                if (matrix.Children[dimension].HasValue)
                {
                    T value = matrix.Children[dimension].Value;
                    if (value is ComplexNumber)
                    {
                        // Take the modulus squared by multiplying the value by it's conjugate. Convert to Double by taking Real Value only
                        // There should be no imaginary number after you multiply by a conjugate of itself.

                        ComplexNumber conjugate = ((ComplexNumber)(dynamic)value).Conjugate();
                        result.Children[dimension].Value = ((dynamic)value * conjugate).Real;
                        // value * conjugate is short cut for:
                        // result.Children[dimension].Value = Math.Pow(((ComplexNumber)(dynamic)value).Modulus(), 2.0); // i.e. Modulus-Squared

                    }
                    else if (value is double)
                        result.Children[dimension].Value = (dynamic)value * (dynamic)value;

                }
                else
                {
                    result.Children[dimension] = TakeModulusSquaredInMatrix(matrix[dimension]);
                }
            }
            return result;
        }


        // Convert from Matrix<ComplexNumber> to Matrix<double> 
        // Used for implicit cast operator for ComplexMatrices into DoubleMatrices.
        // Only works if there are no imaginary numbers. Otherwise throws an error
        protected static Matrix<double> ComplexToDouble(Matrix<ComplexNumber> matrix)
        {
            Matrix<double> result = new Matrix<double>(matrix.Dimensions);

            int thisDimension = matrix.Dimensions[0];
            for (int dimension = 0; dimension < thisDimension; dimension++)
            {
                if (matrix.Children[dimension].HasValue)
                {
                    // Take real value only to convert to DoubleMatrix
                    double value = matrix[dimension].Value.Modulus();
                    if (matrix[dimension].Value.Imaginary != 0)
                        throw new Exception("Can't implicitly convert a ComplexMatrix to a DoubleMatrix if any of the cells contains imaginary numbers.");
                    else
                        result.Children[dimension].Value = matrix[dimension].Value.Real;
                }
                else
                {
                    result.Children[dimension] = ComplexToDouble(matrix[dimension]);
                }
            }
            return result;
        }


        // The Commutator function takes two matrices and substracts their products
        public static Matrix<T> Commutator(Matrix<T> matrix1, Matrix<T> matrix2)
        {
            // from page 119 of QCCS
            // The Commutator function takes to matrices and substracts their products
            // [O1, O2] = (O1 * O2) - (O2 * O1)
            
            // Only works for square matrices
            if ( !(matrix1.Dimensions.Count() == 2 && matrix2.Dimensions.Count() == 2 
                 && matrix1.Dimensions[0] == matrix1.Dimensions[1]
                 && matrix2.Dimensions[0] == matrix2.Dimensions[1]) )
                throw new Exception("The Matrix Commutator operationa only works with square 2-dimensional matrices");


            Matrix<T> part1, part2, result;
            part1 = matrix1 * matrix2;
            part2 = matrix2 * matrix1;

            result = part1 - part2;

            return result;
        }

        // We need an approximately equal function in case this is a double with a bit of a rounding error
        public static bool ApproximatelyEqual(T first, T second)
        {
            if (first is double)
            {
                double thisValue = (dynamic)first;
                double compareValue = (dynamic)second;

                double difference = Math.Abs(thisValue - compareValue);
                double larger = Math.Abs(thisValue) > Math.Abs(compareValue) ? Math.Abs(thisValue) : Math.Abs(compareValue);
                larger = larger > 0.000001 ? larger : 0.000001;
                double tolerance = Math.Abs(larger * 0.0001);

                if (difference > tolerance)
                    return false;
                else
                    return true;
            }
            else
                // else just use built in functions (ComplexNumbers will handle themselves)
                return (dynamic)first == second;
        }

        #endregion

    }
}
