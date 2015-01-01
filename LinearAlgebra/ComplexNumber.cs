using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinearAlgebra
{
    public struct ComplexNumber
    {

        #region properties

        public double Real { get; set; }
        
        public double Imaginary { get; set; }

        public double Length { get {return Modulus();} }

        public double Angle { get {return GetAngle();} }

        #endregion


        #region constructors

        // create a complex number using cartesian representation
        public ComplexNumber(double real, double imaginary) : this()
        {
            Real = real;
            Imaginary = imaginary;
        }

        public ComplexNumber(double real) : this()
        {
            Real = real;
            Imaginary = 0.0;
        }

        #endregion

        #region operators

        public static ComplexNumber operator +(ComplexNumber first, ComplexNumber second)
        {
            return first.Add(second);
        }


        public static ComplexNumber operator -(ComplexNumber first, ComplexNumber second)
        {
            return first.Subtract(second);
        }


        public static ComplexNumber operator *(ComplexNumber first, ComplexNumber second)
        {
            return first.Multiply(second);
        }


        public static ComplexNumber operator *(ComplexNumber first, double second)
        {
            return first.Multiply(second);
        }


        public static ComplexNumber operator *(double first, ComplexNumber second)
        {
            return second.Multiply(first);
        }


        public static ComplexNumber operator /(ComplexNumber first, ComplexNumber second)
        {

            return first.Divide(second);
        }


        public static ComplexNumber operator ^(ComplexNumber complexNumber, double power)
        {
            ComplexNumber retValue = new ComplexNumber(complexNumber.Real, complexNumber.Imaginary);

            // Power: If a is a complex numbers and n is a real number then 
            // a^n = (length^n, n*angle) (in polar coordinates)

            retValue.SetPolarCoordinates(Math.Pow(complexNumber.Length, power), complexNumber.Angle * power);

            return retValue;
        }


        public static bool operator ==(ComplexNumber first, ComplexNumber second)
        {
            if (first.Equals(second))
                return true;
            else
                return false;
        }


        public static bool operator !=(ComplexNumber first, ComplexNumber second)
        {
            return !(first == second);
        }


        // implicit cast operator for complex numbers from doubles
        public static implicit operator ComplexNumber(double value)
        {
            return (new ComplexNumber(value));
        }



        #endregion



        #region staticvalues

        private static ComplexNumber _i = new ComplexNumber(0.0, 1.0);
        private static ComplexNumber _zero = new ComplexNumber(0.0, 0.0);
        private static ComplexNumber _one = new ComplexNumber(1.0, 0.0);
        private static ComplexNumber _negOne = new ComplexNumber(-1.0, 0.0);
        private static ComplexNumber _negI = new ComplexNumber(0.0, -1.0);

        public static ComplexNumber I { get { return _i; }  }
        public static ComplexNumber Zero { get { return _zero; } }
        public static ComplexNumber One { get { return _one; } }
        public static ComplexNumber NegativeI { get { return _negI; } }
        public static ComplexNumber NegativeOne { get { return _negOne; } }

        #endregion



        #region overrides

        public override string ToString()
        {
            return Real.ToString() + " + " + Imaginary.ToString() + "i";
        }

        #endregion


        public override bool Equals(object obj)
        {
            if (obj == null) return base.Equals(obj);

            if (!(obj is ComplexNumber))
                throw new InvalidCastException("The 'obj' argument is not a ComplexNumber object.");
            else
                return Equals((ComplexNumber)obj);
        }


        public override int GetHashCode()
        {
            // my hash code will be an integer in this format: Real, Imaginary
            // This is not guarenteed to be unique
            // TODO: Does this hash code really work?
            return (int)((Real / 0.0001) * 1000.0 + Imaginary / 0.0001);
        }


        #region methods

        private bool Equals(ComplexNumber complexNumber)
        {
            double diffReal = Math.Abs(Real - complexNumber.Real);
            double diffImaginary = Math.Abs(Imaginary - complexNumber.Imaginary);
            double largerReal = Math.Abs(Real) > Math.Abs(complexNumber.Real) ? Math.Abs(Real) : Math.Abs(complexNumber.Real);
            largerReal = largerReal > 0.000001 ? largerReal : 0.0000001;
            double largerImaginary = Math.Abs(Imaginary) > Math.Abs(complexNumber.Imaginary) ? Math.Abs(Imaginary) : Math.Abs(complexNumber.Imaginary);
            largerImaginary = largerImaginary > 0.000001 ? largerImaginary : 0.0000001;
            
            double toleranceReal = Math.Abs(largerReal * 0.001);
            double toleranceImaginary = Math.Abs(largerImaginary * 0.001);

            if (diffReal <= toleranceReal && diffImaginary <= toleranceImaginary)
                return true;
            else
                return false;
        }


        // Returns the modulus which is really just the length of the complex number.
        // Needed for polar representation
        public double Modulus()
        {
            double a, b;

            // Modulus: |c| = |a + bi| = Sqrt(a^2 + b^2)
            a = Real * Real;
            b = Imaginary * Imaginary;
            return Math.Sqrt(a + b);
        }


        // Returns the Conjugate, which is a way of sort of inversing the complex number
        public ComplexNumber Conjugate()
        {
            return new ComplexNumber(Real, -Imaginary);
        }



        // Returns the k=0th square root of this complex number
        public void Root(double root, int k)
        {

            ComplexNumber newValue = new ComplexNumber(Real, Imaginary);

            // Power: If a is a complex numbers and n is a real number then 
            // a^n = (length^n, n*angle) (in polar coordinates)

            newValue.SetPolarCoordinates(Math.Pow(Length, 1.0/root), (Angle * (1.0/root) + (((double)k/root) * 2.0*Math.PI )) );

            this.Real = newValue.Real;
            this.Imaginary = newValue.Imaginary;
        }


        // Returns the angle in radians of the complex number for use with polar representation
        public double GetAngle()
        {
            // % (2.0*Math.PI) constrains the angle such that it's always equivalent to it's equivalent angle within 2*PI
            // This allows us to add angles and still get the correct answer. See Quantum Computing p. 19
            double radians = Math.Atan2(Imaginary, Real) % (2.0 * Math.PI);

            // We don't want negative angles because they give the wrong result
            if (radians < 0)
                radians = radians + 2.0 * Math.PI;

            return radians;
        }


        // Returns the angle in degrees of the complex number for use with polar representation
        public double GetAngleDegrees()
        {
            double radians = Math.Atan2(Real, Imaginary);

            // % (2.0*Math.PI) constrains the angle such that it's always equivalent to it's equivalent angle within 2*PI
            // This allows us to add angles and still get the correct answer. See Quantum Computing p. 19
            radians = radians % (2.0 * Math.PI);

            // We don't want negative angles because they give the wrong result
            if (radians < 0)
                radians = radians + 2.0 * Math.PI;

            double oneDegree = (2.0*Math.PI)/360.0; // a circles radius is 2*PI*Radius. On unit circle radius = 1, so just 2*PI

            return radians / oneDegree; 
        }



        public ComplexNumber Multiply(ComplexNumber multiplier)
        {
            double part1, part2, part3, part4, realTot, imaginaryTot;

            // (first.Real * second.Real) + (first.Real * second.Imaginary) + (first.Imaginary * second.Real) + (first.Imaginary * second.Imaginary)

            // (first.Real * second.Real)
            part1 = this.Real * multiplier.Real;

            // (first.Real * second.Imaginary)
            part2 = this.Real * multiplier.Imaginary;

            // (first.Imaginary * second.Real)
            part3 = this.Imaginary * multiplier.Real;

            // (first.Imaginary * second.Imaginary)
            part4 = -(this.Imaginary * multiplier.Imaginary);

            realTot = part1 + part4;
            imaginaryTot = part2 + part3;

            return new ComplexNumber(realTot, imaginaryTot);

        }



        public ComplexNumber Multiply(double scalarMultiplier)
        {
            return new ComplexNumber(Real * scalarMultiplier, Imaginary * scalarMultiplier);
        }


        
        public ComplexNumber Divide(ComplexNumber divisor)
        {
            double div, realTot, imaginaryTot;

            // Division: (a1 + b1i) / (a2 + b2i) == (a1*a2 + b1*b2) / (a2*a2 + b2*b2)  +  (a2*b1 - a1b2)i / (a2*a2 + b2*b2)i

            div = ((divisor.Real * divisor.Real) + (divisor.Imaginary * divisor.Imaginary));
            realTot = ((this.Real * divisor.Real) + (this.Imaginary * divisor.Imaginary)) / div;
            imaginaryTot = ((divisor.Real * this.Imaginary) - (this.Real * divisor.Imaginary)) / div;

            return new ComplexNumber(realTot, imaginaryTot);
        }



        public ComplexNumber Add(ComplexNumber value)
        {
            return new ComplexNumber(this.Real + value.Real, this.Imaginary + value.Imaginary);
        }


        public ComplexNumber Subtract(ComplexNumber value)
        {
            return new ComplexNumber(this.Real - value.Real, this.Imaginary - value.Imaginary);
        }



        // Divide using polar representation short cut
        public void DividePolar(ComplexNumber divisor)
        {
            double p1 = Length;
            double angle1 = Angle;
            double p2 = divisor.Length;
            double angle2 = divisor.Angle;

            SetPolarCoordinates(p1 / p2, angle1 - angle2);
        }


        // create a complex number using polar representation
        public void SetPolarCoordinates(double length, double angle)
        {
            double x, y;

            // % (2.0*Math.PI) constrains the angle such that it's always equivalent to it's equivalent angle within 2*PI
            // This allows us to add angles and still get the correct answer. See Quantum Computing p. 19
            angle = angle % (2.0*Math.PI);

            // Find the result by using a triangle
            // length is the hypotenuse
            // Cos(angle) = base/hypotenuse
            // Sin(angle) = perpendicular/hypotenuse
            // base is x and perpendicular is y

            x = length * Math.Cos(angle);
            y = length * Math.Sin(angle);

            Real = x;
            Imaginary = y;
        }


        // Create a complex number using cartesian coordinates
        public void SetCartesianCoordinates(double real, double imaginary)
        {
            Real = real;
            Imaginary = imaginary;
        }

        // Output as string in polar coordinates
        public string PolarCoordinatesString()
        {
            return "p=" + Length.ToString() + " angle=" + Angle.ToString();
        }


        // Output as string in polar coordinates
        public string CartesianCoordinatesString()
        {
            return "Real=" + Real.ToString() + " Imaginary=" + Imaginary.ToString();
        }


        // Output as string in polar coordinates
        public string ExponentialFormString()
        {
            return Length.ToString() + " * e^(i*" + Angle.ToString()+")";
        }


        // InnerProduct: Treat a complex number as a vector and take its inner product with another ComplexNumber
        public ComplexNumber InnerProduct(ComplexNumber number)
        {
            // An "Inner Product" is the same as a Dot Product
            // For two vectors (1-dimensional matrix) you multiply them together and then add the results to get 
            // a final scalar. i.e. {2, -2} dot {2, -2} = 2*2 + -2*-2 = 8
            // See Quantum Computing for Computer Scientists p. 54-55

            return (this.Real * number.Real) + (this.Imaginary * number.Imaginary);
        }


        #endregion

    }
}
