using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinearAlgebra
{
    public class ComplexVector : ComplexMatrix, IVector<ComplexNumber>
    {
        #region constructors

        public ComplexVector() : base() {}

        public ComplexVector(int size) : base(size) { }


        public ComplexVector(ComplexNumber[] components) : base(components.Length)
        {
            for (int i = 0; i < components.Length; i++)
            {
                base[i] = components[i];
            }
        }


        public ComplexVector(Vector<ComplexNumber> vector) : base(vector) { }

        public ComplexVector(Matrix<ComplexNumber> vector) : base(vector)
        {
            if (vector.Dimensions.Count() > 1)
                throw new Exception("A Vector has only one dimension");
        }

        #endregion

        #region properties


        // Returns the "R-Space" value for this vector, which is really just its length
        public int RSpace
        {
            get
            {
                return base.Children.Count();
            }
        }

        public int Size { get { return RSpace; } }


        #endregion


        #region overrides

        public static ComplexVector operator *(ComplexVector vector, ComplexNumber scalarMultiple)
        {
            return (ComplexVector)(scalarMultiple * (Matrix<ComplexNumber>)vector);
        }

        public static ComplexVector operator *(ComplexVector vector, double scalarMultiple)
        {
            return (ComplexVector)(scalarMultiple * (Matrix<ComplexNumber>)vector);
        }

        public static ComplexVector operator *(ComplexNumber scalarMultiple, ComplexVector matrix)
        {
            return (ComplexVector)(scalarMultiple * (Matrix<ComplexNumber>)matrix);
        }

        public static ComplexVector operator *(double scalarMultiple, ComplexVector vector)
        {
            return (ComplexVector)(scalarMultiple * (Matrix<ComplexNumber>)vector);
        }

        // Scalar Divisors
        public static ComplexVector operator /(ComplexVector matrix, double divisor)
        {
            return (ComplexVector)((1.0 / divisor) * matrix);
        }

        // Scalar Divisors
        public static ComplexVector operator /(ComplexVector matrix, ComplexNumber divisor)
        {
            return (ComplexVector)((1.0 / divisor) * matrix);
        }


        // Implements default Equals to call class specific Equals
        public override bool Equals(object obj)
        {
            if (obj == null) return base.Equals(obj);

            if (!(obj is Matrix<ComplexNumber>))
                throw new InvalidCastException("The 'obj' argument is not a Matrix<ComplextNumber> object.");
            else
                return Equals((Matrix<ComplexNumber>)obj);
        }




        // Implements a hashcode that isn't really guarenteed to be unique
        // but should be relatively close.
        public override int GetHashCode()
        {
            // TODO: Not sure what to do as a hash code here
            int hashcode=0;

            for (int i = 0; i < this.Size; i++)
            {
                hashcode = hashcode + this[i].GetHashCode() * i;
            }

            return hashcode;
        }



        // This method will be used by the Equals method but allows for 'approximate' comparisions
        // which are necessary for any sort of double math
        public override bool Equals(Matrix<ComplexNumber> vector)
        {
            if ((object)vector == null || !this.CompareSize(vector))
                throw new Exception("Cannot compare two vectors of different dimensions");

            for (int i = 0; i < RSpace; i++)
            {
                if (this[i] != vector[i].Value)
                    return false;
            }

            return true;

        }

        #endregion



        #region methods


        // Takes the vector and sets it to be a zero vector
        public void SetToZero()
        {
            for (int i = 0; i < RSpace; i++)
            {
                this[i] = ComplexNumber.Zero;
            }
        }


        // DotProduct for vector is same as InnerProduct
        public ComplexNumber DotProduct(Matrix<ComplexNumber> vector)
        {
            return this.InnerProduct(vector);
        }



        // Given a "bra" return its "ket" which is just a new vector with all conjugates of the complex numbers
        public ComplexVector GetKet()
        {
            ComplexVector ket = new ComplexVector(this);

            for (int i = 0; i < ket.Size; i++)
            {
                ket[i] = ket[i].Conjugate();
            }

            return ket;
        }



        // Given the current vector and another vector, find the amplitude
        public ComplexNumber GetAmplitude(ComplexVector transitionTo)
        {
            // The Amplitude is the inner product of the second vector's ket and the original vector
            // i.e. given |a> to |b> we find <b|a>
            // See p. 113 in QCCS

            // ComplexVector ket = new ComplexVector(transitionTo);
            // Despite what p. 112 and 113 say, it appears that you don't have to get the Ket because it's just an adjoint, 
            // which is what you do for an inner product anyhow.

            return transitionTo.InnerProduct(this);
        }


        // Given the current vector and another vector, find the amplitude
        public ComplexNumber GetNormalizedAmplitude(ComplexVector transitionTo)
        {
            // See p. 114 in QCCS - example 4.1.7

            double norm = this.Length() * transitionTo.Length();
            ComplexNumber amplitude = this.GetAmplitude(transitionTo);
            double real = amplitude.Real / norm;
            double imaginary = amplitude.Imaginary / norm;
            return  new ComplexNumber(real, imaginary);
        }


        // Normalize is on P. 109 of QCCS
        public ComplexVector GetNormalizedVector()
        {
            // From p. 109, to normalize, just find the length (i.e. modulus) and divide so that length is 1
            return this / this.Length();
        }


        #endregion
    }
}
