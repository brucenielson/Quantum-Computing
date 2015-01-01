using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace MathLib {
	public struct BigReal {

		#region Fields
		// Internal representation
		// ushort = 0 to 65,535
		// 1000 stored as { "1", "0", "0" }
		// As soon as the number gets longer than 5 digits, it is moved!
		private List<byte> _number; //= new List<byte>(); // { element 0
		private List<byte> _decimal; //= new List<byte>();
		private bool _negitive; //= false;
		private long Precision; // = 100;
		#endregion

		#region Properties
		public bool IsNegative {
			get { return _negitive; }
		}

		public long _precision {
			get { return Precision; }
			set { Precision = value; }
		}

		#endregion

		#region Constructors
		// Default constructor
		//public BigReal() { }

		public BigReal( byte num ) : this( (long)num ) { }

		public BigReal( long num ) : this( (decimal)num ) { }

		public BigReal( decimal num ) : this() {
			// Init
			_decimal = new List<byte>();
			_number = new List<byte>();
			_precision = 100;

			decimal number = Math.Abs( num );
			// Get the sign of the num
			if ( num < 0 )
				this.Negate();

			// take the last number (mod it with 10) and divide the number by 10 until we have 0
			while ( number > 0 ) {
				byte r = (byte)( number % 10 );
				_number.Add( r );
				number = number / 10;
			}

			// We have the Mantessa, now we need the modulus
			// Get rid of the "upper" portion of the number so we can just work with the numbers on the right of the decimal point
			number = Math.Abs( num );
			number = (decimal)( (decimal)number - decimal.Truncate( number ) );
			_decimal.Clear();
			while ( number > 0 ) {
				byte r = (byte)( number * 10 );
				_decimal.Add( r );
				number = number * 10;
				number = (decimal)( (decimal)number - (long)number );
			}
			Normalize();
		}

		public BigReal( double num ) : this() {
			// We need to treat decimal and double differently
			// Init
			_decimal = new List<byte>();
			_number = new List<byte>();
			_precision = 100;

			double number = Math.Abs( num );
			// Get the sign of the num
			if ( num < 0 )
				this.Negate();

			// take the last number (mod it with 10) and divide the number by 10 until we have 0
			while ( number > 0 ) {
				byte r = (byte)( number % 10 );
				_number.Add( r );
				number = number / 10;
			}

			// We have the Mantessa, now we need the modulus
			// Get rid of the "upper" portion of the number so we can just work with the numbers on the right of the decimal point
			number = Math.Abs( num );
			number = (double)( (double)number - Math.Truncate( number ) );
			_decimal.Clear();
			while ( number > 0 ) {
				byte r = (byte)( number * 10 );
				_decimal.Add( r );
				number = number * 10;
				number = (double)( (double)number - (long)number );
			}
			Normalize();
		}
		#endregion

		#region Public Methods
		public BigReal Clone() {
			// Copy the field data
			BigReal newNum = new BigReal();

			// Init
			newNum._decimal = new List<byte>();
			newNum._number = new List<byte>();
			newNum._precision = 100;

			newNum._decimal.Clear();
			newNum._number.Clear();
			newNum._number.AddRange( this._number.ToArray() );
			if ( this.IsNegative ) newNum.Negate();
			newNum._decimal.AddRange( this._decimal.ToArray() );
			newNum.Normalize();
			return newNum;
		}
		public void Negate() {
			this._negitive = !this._negitive;
		}
		public void Negate( bool IsNegative ) {
			this._negitive = IsNegative;
		}
		#endregion

		#region Public Static methods
		public static BigReal Parse( string number ) {
			// first split the decimal point (if possible)
			string[] strnum = number.Replace( "-", "" ).Split( new char[] { '.' }, 2 );
			BigReal br = new BigReal();

			// Init
			br._decimal = new List<byte>();
			br._number = new List<byte>();
			br._precision = 100;

			// Check for negative
			if ( number.Trim().StartsWith( "-" ) )
				br.Negate();

			foreach ( char c in strnum[0] ) {
				byte digit;
				if ( byte.TryParse( new string( new char[] { c } ), out digit ) ) {
					br._number.Insert( 0, digit );
				} else
					throw new FormatException();
			}

			if ( strnum.Length > 1 ) {
				foreach ( char c in strnum[1] ) {
					byte digit;
					if ( byte.TryParse( new string( new char[] { c } ), out digit ) ) {
						br._decimal.Add( digit );
					} else
						throw new FormatException();
				}
			}
			br.Normalize();
			return br;
		}
		public static BigReal Abs( BigReal number ) {
			BigReal br = number.Clone();
			br.Negate( false );
			return br;
		}
		#endregion

		#region Override Methods
		public override string ToString() {
			if ( this._decimal.Count == 0 && this._number.Count == 0 )
				return "0";

			StringBuilder result = new StringBuilder();
			foreach ( byte b in _number ) {
				result.Insert( 0, b );
			}
			if ( _decimal.Count > 0 ) {
				// Make sure there is at least a 0. before the decimal
				if ( result.Length == 0 )
					result.Append( "0" );
				result.Append( "." );

				foreach ( byte b in _decimal ) {
					result.Append( b );
				}
			}
			return ( (string)( IsNegative ? "-" : string.Empty ) ) + result.ToString();
		}

		public override bool Equals( object obj ) {
			if ( (obj == null) || (!(obj is BigReal)) )
				return false;
			BigReal num2 = (BigReal)obj;
			if ( num2 == null )
				return false;
			else return ( this == num2 );
		}

		public override int GetHashCode() {
			return base.GetHashCode();
		}
		#endregion

		#region Protected Methods

		private static byte GetByte( List<byte> list, int i ) {
			if ( list.Count > i )
				return list[i];
			else
				return 0;
		}
		private void Normalize() {
			// look for leading or trailing zero's
			while ( ( this._decimal.Count > 0 ) && ( this._decimal[this._decimal.Count - 1] == 0 ) )
				this._decimal.RemoveAt( this._decimal.Count - 1 );

			while ( ( this._number.Count > 0 ) && ( this._number[this._number.Count - 1] == 0 ) )
				this._number.RemoveAt( this._number.Count - 1 );
			// if we are zero, remove negative
			if ( ( this._number.Count == 0 && this._decimal.Count == 0) || ( this._number.Count == 1 && this._number[0] == 0 ) )
				this.Negate( false );
		}

		private static void InitializeList( List<byte> opList, int length ) {
			opList.Clear();
			for ( int i = 0; i <= length; i++ ) {
				opList.Add( 0 );
			}
			if ( opList.Count == 0 )
				opList.Add( 0 ); // Must have at least a single point
		}
		#endregion

		#region Overloaded Operators

		public static implicit operator BigReal( long value ) {
			return ( new BigReal( value ) );
		}

		public static implicit operator BigReal( ulong value ) {
			return ( new BigReal( (double)value ) );
		}

		public static implicit operator BigReal( double value ) {
			return ( new BigReal( value ) );
		}

		public static implicit operator BigReal( int value ) {
			return ( new BigReal( value ) );
		}

		public static implicit operator BigReal( uint value ) {
			return ( new BigReal( value ) );
		}

		public static implicit operator BigReal( float value ) {
			return ( new BigReal( value ) );
		}

		public static implicit operator BigReal( decimal value ) {
			return ( new BigReal( value ) );
		}

		public static BigReal operator -( BigReal num1, BigReal num2 ) {
			// Subtract a BigReal number from another number
			//   23.23345
			// - 10.90000
			//___________
			//   12.33345 (borrow)

			// Sign Truth Table
			// (+) - (+) = (-) 5 - 3 = 2 (positive or negitive)
			// (+) - (-) = (+) 5 - -3 = 8 (positive or negative)
			// (-) - (+) = (+) -5 - 3 = -8 (positive or negative)
			// (-) - (-) = (-) -5 - -3 = -2 (positive or negative)

			// CHECK TO SEE IF NUMBER2 > NUMBER1. IF SO, REVERSE AND NEGATE
			if ( Abs(num1) < Abs(num2) ) {
				BigReal res = num2 - num1;
				res.Negate();
				return res;
			}

			// Only one side is negative
			if ( ( num1.IsNegative && !num2.IsNegative ) || ( !num1.IsNegative && num2.IsNegative ) ) {
				// (+) + (-) = num1 - num2 
				// (-) + (+) = num2 - num1

				// (+) - (-) = (+) 5 - -3 = 8 (positive or negative) // if num2 is bigger, negative
				// (-) - (+) = (+) -5 - 3 = -8 (positive or negative)// if num1 is bigger, negative
				// (+) - (-) = num1 + num2 // drop the negitive on num2
				// (-) - (+) = num1 + num2 // 

				bool neg = num1.IsNegative;
				num1.Negate( false );
				num2.Negate( false );
				BigReal res = num1 + num2;

				res.Negate( neg );
				return res;
			}


			// Start with the decimal portion (we need to see if we have a borrow from the number portion.
			BigReal result = 0;
			int size = num1._decimal.Count > num2._decimal.Count ? num1._decimal.Count - 1 : num2._decimal.Count - 1;
			List<byte> opList = new List<byte>();
			InitializeList( opList, size );
			sbyte carry = 0;

			// Decimal :: Modulus
			for ( int i = size; i >= 0; i-- ) {
				sbyte res = (sbyte)( GetByte( num1._decimal, i ) - GetByte( num2._decimal, i ) - carry );
				if ( res < 0 ) {
					res = (sbyte)Math.Abs( ( GetByte( num1._decimal, i ) + 10 ) - GetByte( num2._decimal, i ) - carry ); // Have to add the previous carry (cascade borrow)
					carry = 1;
				} else
					carry = 0; // zero out if not used
				opList[i] = (byte)res;
			}

			result._decimal.Clear();
			result._decimal.AddRange( opList.ToArray() );

			// Number
			size = num1._number.Count > num2._number.Count ? num1._number.Count - 1 : num2._number.Count - 1;
			opList = new List<byte>();
			InitializeList( opList, size );

			for ( int i = 0; i <= size; i++ ) {
				sbyte res = (sbyte)( GetByte( num1._number, i ) - GetByte( num2._number, i ) - carry ); // Notice that carry is here from the decimal part
				if ( res < 0 ) {
					res = (sbyte)Math.Abs( ( ( GetByte( num1._number, i ) + 10 ) - GetByte( num2._number, i ) - carry ) );
					carry = 1;
				} else
					carry = 0; // zero out if not used
				opList[i] = (byte)res;
			}
			if ( carry != 0 ) // number is negative
				result.Negate( true );

			// TRUTH TABLE :: num1 = H and will always be bigger
			// {H} - {l}   = POSITIVE
			// {-H} - {l}  = NEGATIVE
			// {-H} - {-l} = NEGATIVE
			// {H} - {-l}  = POSITIVE

			result.Negate( num1.IsNegative );

			result._number.Clear();
			result._number.AddRange( opList.ToArray() );
			result.Normalize();
			return result;
		}

		public static BigReal operator +( BigReal num1, BigReal num2 ) {
			// Add a BigReal number against another number
			//   23.23345
			// + 10.9
			//___________
			//   34.13345 

			// Sign Truth Table
			// (+) + (+) = (+) 
			// (+) + (-) = (-) // Use subtraction.
			// (-) + (+) = (-) // Use subtraction.
			// (-) + (-) = (+) // If they are both negitive, just ignore the sign

			// One of them are negative, but not both
			if ( ( num1.IsNegative || num2.IsNegative ) && ( ( !num1.IsNegative || !num2.IsNegative ) ) ) {
				// (+) + (-) = num1 - num2 
				// (-) + (+) = num2 - num1
				if ( num1.IsNegative ) {
					num1.Negate( false );
					num2.Negate( false );
					return num2 - num1;
				} else {
					num1.Negate( false );
					num2.Negate( false );
					return num1 - num2;
				}

			}


			// Start with the decimal portion (we need to see if we have a carry to the number portion.
			BigReal result = 0;
			int size = num1._decimal.Count > num2._decimal.Count ? num1._decimal.Count - 1 : num2._decimal.Count - 1;
			List<byte> opList = new List<byte>();
			InitializeList( opList, size );
			byte carry = 0;

			// Decimal :: Modulus
			for ( int i = size; i >= 0; i-- ) {
				byte res = (byte)( GetByte( num1._decimal, i ) + GetByte( num2._decimal, i ) + carry );
				if ( res >= 10 ) {
					carry = 1;
					res = (byte)( res % 10 );
				} else
					carry = 0; // zero out if not used
				opList[i] = res;
			}

			result._decimal.Clear();
			result._decimal.AddRange( opList.ToArray() );

			// Number
			size = num1._number.Count > num2._number.Count ? num1._number.Count - 1 : num2._number.Count - 1;
			opList = new List<byte>();
			InitializeList( opList, size );

			for ( int i = 0; i <= size; i++ ) {
				byte res = (byte)( GetByte( num1._number, i ) + GetByte( num2._number, i ) + carry ); // Notice that carry is here from the decimal part
				if ( res >= 10 ) {
					carry = 1;
					res = (byte)( res % 10 );
				} else
					carry = 0; // zero out if not used
				opList[i] = res;
			}
			if ( carry != 0 )
				opList.Add( 1 );

			result._number.Clear();
			result._number.AddRange( opList.ToArray() );

			// Assert if the number is negative
			// If both numbers are negative AND the Abs(num1) > Abs(num2); it's negative
			if ( ( num1.IsNegative && num2.IsNegative ) ) {
				result.Negate( true );
			}
			result.Normalize();
			return result;
		}

		public static BigReal operator *( BigReal num1, BigReal num2 ) {
			BigReal result = 0;

			#region Traditional Process
			// Process:
			//   14.25
			// *  8.75 // start from the left and work right;
			//________
			//       5 // 5 * 5 is 25; cary the 2
			//      2  // 5 * 2 = 10 + (carry 2) = 12 (carry the 1)
			//    1.   // 5 * 4 = 20 + (carry 1) = 21 (carry the 2)
			//   7     // 5 * 1 = 5  + (carry 2) = 7 (no carry)
			//   17.25 // First Level (out of three)
			//--------
			//      5  // 7 * 5 = 35   (carry the 3)
			//    7.   // 7 * 2 = 14 + (carry 3) = 17 (carry the 1)
			//   9     // 7 * 4 = 28 + (carry 1) = 29 (carry the 2)
			//  9      // 7 * 1 = 7 +  (carry 2) = 9
			//  997.50 // Second Level (out of three)
			//---------
			//       0 // 8 * 5 = 40   (carry the 4)
			//      0  // 8 * 2 = 16 + (carry 4) = 20 (carry the 2)
			//    4.   // 8 * 4 = 32 + (carry 2) = 34 (carry the 3)
			//  11     // 8 * 1 = 8  + (carry 3) = 11
			//--------- // Third Level (out of three)
			//    17.25
			//    99.75
			// + 114.00
			//---------
			//   231.00

			// 124.6875 // actual value
			#endregion

			#region Method
			//Method 

			//Multiply as though you were multiplying two integers (disregarding the decimal point) 
			//Sum the decimal places in the original numbers (i.e. the numbers to the right of the decimal point), e.g. 

			//2.94×329.625 
			//has a total of 5 decimal places (the 94 from the first number and the 625 from the second number) 

			//Shift the decimal place in the product from Stage 1 by the number of places found from stage 2, e.g. if the product from Stage 1 was 

			//2957867 
			//the final answer would be 29.57867 

			// Example: 7.5 * 94.21
			//  9421
			// *  75
			// ______
			//     5  // 5 * 1 = 5 + (carried 0) = 5 (carry 0)
			//    0   // 5 * 2 = 0 + (carried 0) = 0 (carry 1)
			//   1    // 5 * 4 = 0 + (carried 1) = 1 (carry 2)
			//  7     // 5 * 9 = 5 + (carried 2) = 7 (carry 4)
			// 4      // Carry
			// 47105
			// ------- (level 2)
			//    70  // 7 * 1 = 7 + (carried 0) = 7 (carry 0)
			//   4    // 7 * 2 = 4 + (carried 0) = 4 (carry 1)
			//  9     // 7 * 4 = 8 + (carried 1) = 9 (carry 2)
			// 5      // 7 * 9 = 3 + (carried 2) = 5 (carry 6)
			//6       // carry
			//659470

			//  47105
			//+659470
			//_______
			// 706575
			// Answer: 706.575
			#endregion

			// Multiply by 0
			if ( ( num1 == 0 ) || ( num2 == 0 ) )
				return new BigReal( 0 );

			// Multiply by 1
			if ( ( num1 == 1 ) || ( num2 == 1 ) )
				return ( num1 == 1 ? num2.Clone() : num1.Clone() );

			// Truth table for negitive
			// - * - = +
			// + * - = - // If one or the other is negative BUT NOT BOTH, the result is negative
			// + * + = + 
			bool IsNegative = ( num1.IsNegative != num2.IsNegative );

			// Clone the numbers...we're going to mix'em up a bit by putting the decimal side on the integer side
			BigReal multiplicand = num1.Clone();
			BigReal multiplier = num2.Clone();
			int decimalPoints = 0;

			decimalPoints += multiplicand._decimal.Count;
			multiplicand.Negate( false );
			multiplicand._decimal.Reverse();
			multiplicand._number.InsertRange( 0, multiplicand._decimal );
			multiplicand._decimal.Clear();

			decimalPoints += multiplier._decimal.Count;
			multiplier.Negate( false );
			multiplier._decimal.Reverse();
			multiplier._number.InsertRange( 0, multiplier._decimal );
			multiplier._decimal.Clear();

			// Lets get multiplying!
			BigReal op;
			// outer for loop is for the multiplier
			for ( int i = 0; i < multiplier._number.Count; i++ ) {
				byte carry = 0;
				op = 0;
				byte[] bytes = new byte[i]; // add some padding
				op._number.AddRange( bytes );

				// Inner for loop is for the multiplicand
				for ( int j = 0; j < multiplicand._number.Count; j++ ) {
					byte res = (byte)( ( GetByte( multiplier._number, i ) * GetByte( multiplicand._number, j ) ) + carry );
					carry = (byte) (res / 10);
					res = (byte) (res % 10);
					op._number.Add( res );
				}
				// Add carry to the top (if any)
				if ( carry > 0 )
					op._number.Add( carry );
				result = result + op;
			}

			// Did we end up with a 0.000 number?  
			// if so we need to add some padding to move the decimal back over
			if ( decimalPoints > result._number.Count ) {
				byte[] padding = new byte[decimalPoints];
				result._number.AddRange( padding );
			} 
			result._decimal.AddRange( result._number.GetRange( 0, decimalPoints ) );
			result._decimal.Reverse(); // These digits move outward from the tenths spot
			result._number.RemoveRange( 0, decimalPoints );
		
			result.Negate( IsNegative );

			result.Normalize();

			return result;
		}

		public static BigReal operator /( BigReal num1, BigReal num2 ) {
			BigReal result = new BigReal();

			// a / b = c
			// a -> dividend
			// b -> divisor 
			// c -> quotient

			#region Method
			//http://bdaugherty.tripod.com/KeySkills/basicArithmetic.html#LMULT
			#endregion

			return result;
		}

		//public static BigReal operator %( BigReal num1, BigReal num2 ) {}

		//public static BigReal operator --( BigReal num1 ) { }

		//public static BigReal operator ++( BigReal num1 ) { }

		public static bool operator >( BigReal num1, BigReal num2 ) {
			// Is num1 > num2
			// Shortcut with signs
			if ( !num1.IsNegative && num2.IsNegative )
				return true;
			else if ( num1.IsNegative && !num2.IsNegative )
				return false;
			else if ( num1.IsNegative && num2.IsNegative ) {
				// negate this result! 
				BigReal clone1 = num1.Clone();
				BigReal clone2 = num2.Clone();
				clone1.Negate( false );
				clone2.Negate( false );
				return ( clone2 > clone1 );
			}

			// start at the top
			int length = num1._number.Count > num2._number.Count ? num1._number.Count : num2._number.Count;
			for ( int i = length - 1; i >= 0; i-- ) {
				if ( GetByte( num1._number, i ) < GetByte( num2._number, i ) )
					return false;
				else if ( GetByte( num1._number, i ) > GetByte( num2._number, i ) )
					return true;
				// We only continue if they are equal
			}

			// So it's down to the decimal point, ehh?
			length = num1._decimal.Count > num2._decimal.Count ? num1._decimal.Count : num2._decimal.Count;
			// The decimal goes the opposite direction
			for ( int i = 0; i < length; i++ ) {
				if ( GetByte( num1._decimal, i ) < GetByte( num2._decimal, i ) )
					return false;
				else if ( GetByte( num1._decimal, i ) > GetByte( num2._decimal, i ) )
					return true;
				// We only continue if they are equal
			}
			return false;
		}

		public static bool operator <( BigReal num1, BigReal num2 ) {
			return ( num2 > num1 );
		}

		public static bool operator ==( BigReal num1, BigReal num2 ) {
			// First check the signs.
			if ( num1.IsNegative != num2.IsNegative )
				return false;

			// Next, check the large number portion
			int length = num1._number.Count > num2._number.Count ? num1._number.Count : num2._number.Count;
			for ( int i = length; i >= 0; i-- ) {
				if ( GetByte( num1._number, i ) != GetByte( num2._number, i ) )
					return false;
			}

			// Now, check the decimal number portion
			length = num1._decimal.Count > num2._decimal.Count ? num1._decimal.Count : num2._decimal.Count;
			for ( int i = 0; i < length; i++ ) {
				if ( GetByte( num1._decimal, i ) != GetByte( num2._decimal, i ) )
					return false;
			}
			return true;
		}

		public static bool operator !=( BigReal num1, BigReal num2 ) {
			return !( num1 == num2 );
		}

		public static bool operator ==( BigReal num1, int numb2 ) {

			BigReal num2 = new BigReal( numb2 );

			// First check the signs.
			if ( num1.IsNegative != num2.IsNegative )
				return false;

			// Next, check the large number portion
			int length = num1._number.Count > num2._number.Count ? num1._number.Count : num2._number.Count;
			for ( int i = length; i >= 0; i-- ) {
				if ( GetByte( num1._number, i ) != GetByte( num2._number, i ) )
					return false;
			}

			// Now, check the decimal number portion
			length = num1._decimal.Count > num2._decimal.Count ? num1._decimal.Count : num2._decimal.Count;
			for ( int i = 0; i < length; i++ ) {
				if ( GetByte( num1._decimal, i ) != GetByte( num2._decimal, i ) )
					return false;
			}
			return true;
		}

		public static bool operator !=( BigReal num1, int numb2 ) {
			return !( num1 == numb2 );
		}

		#endregion
	}
}
