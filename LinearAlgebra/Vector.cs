using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinearAlgebra
{
    public interface IVector<T>
    {
        int RSpace { get; }

        int Size { get; }

        T DotProduct(Matrix<T> vector);

    }


    public class Vector<T> : Matrix<T>, IVector<T>
    {
        // A vector is an ordered list of n numbers representing a point. The vector is the line from the orgin to that point 
        // in n-dimensional space.

        // Each number in the vector is called a component

        #region properties
        
        
        // Returns the "R-Space" value for this vector, which is really just its length
        public int RSpace
        {
            get 
            {
                return base.Children.Count(); 
            }
        }

        public int Size { get { return RSpace;} }


        #endregion


        #region constructors

        public Vector() { }

        public Vector(int size) : base(size) { }

        public Vector(T[] components) : base(components.Length)
        {
            for (int i = 0; i < components.Length; i++)
            {
                base[i].Value = components[i];
            }
        }


        public Vector(Vector<T> vector) : base(vector.Size)
        {
            for (int i = 0; i < vector.Size; i++)
            {
                base[i].Value = vector[i];
            }
        }

        public Vector(Matrix<T> vector) : base(vector)
        {
            if (vector.Dimensions.Count() > 1)
                throw new Exception("A Vector has only one dimension");
        }


        #endregion


        #region methods


        // InnerProduct and DotProduct are identical for vectors
        public T DotProduct(Matrix<T> vector)
        {
            return this.InnerProduct(vector);
        }

        #endregion


        #region methods


        
        // Indexer -- this will be used like the array it is
        public T this[int index]
        {
            get { return base[index].Value; }
            set { base[index].Value = value; }
        }


        #endregion



    }
}
