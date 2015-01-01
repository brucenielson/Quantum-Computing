using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinearAlgebra
{
    public class DoubleVector : DoubleMatrix, IVector<double>
    {


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


        #region constructors

        public DoubleVector() : base() {}

        public DoubleVector(int size) : base(size) {}


        public DoubleVector(double[] components) : base(components.Length)
        {
            for (int i = 0; i < components.Length; i++)
            {
                base[i] = components[i];
            }
        }

        public DoubleVector(Vector<double> vector) : base(vector) { }

        public DoubleVector(Matrix<double> vector) : base(vector)
        {
            if (vector.Dimensions.Count() > 1)
                throw new Exception("A Vector has only one dimension");
        }

        #endregion



        #region overrides


        // Implements default Equals to call class specific Equals
        public override bool Equals(object obj)
        {
            if (obj == null) return base.Equals(obj);

            if (!(obj is Matrix<double>))
                throw new InvalidCastException("The 'obj' argument is not a Vector<double> object.");
            else
                return Equals((Matrix<double>)obj);
        }


        // Implements a hashcode that isn't really guarenteed to be unique
        // but should be relatively close.
        public override int GetHashCode()
        {
            // TODO: Not sure what to do as a hash code here
            double hashcode = 0.0;
            for (int i = 0; i < this.Size; i++)
            {
                hashcode = hashcode + this[i] * i;
            }
            return (int)hashcode;
        }



        // This method will be used by the Equals method but allows for 'approximate' comparisions
        // which are necessary for any sort of double math
        public override bool Equals(Matrix<double> vector)
        {
            if ((object)vector == null || !this.CompareSize(vector))
                throw new Exception("Cannot compare two Matrices of different dimensions");

            for (int i = 0; i < RSpace; i++)
            {
                double difference = Math.Abs(this[i] - vector[i].Value);
                double larger = Math.Abs(this[i]) > Math.Abs(vector[i].Value) ? Math.Abs(this[i]) : Math.Abs(vector[i].Value);
                larger = larger > 0.000001 ? larger : 0.000001;
                double tolerance = Math.Abs(larger * 0.0001);

                if (difference > tolerance)
                    return false;
            }

            return true;

        }


        #endregion



        #region methods

        // DotProduct for vector is same as InnerProduct
        public double DotProduct(Matrix<double> vector)
        {
            return this.InnerProduct(vector);
        }

        #endregion

    }
}
