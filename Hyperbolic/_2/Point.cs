using System;
using Numerics;

namespace Metria.Hyperbolic._2
{
    /// <summary>
    /// This class represents a point in the R² plane
    /// </summary>
	public class Point : Base
	{

        #region Attributes

        private BigRational _x;
        private BigRational _y;
		protected String _id;
        /// <summary>
        /// X coordinate
        /// </summary>
		public BigRational X
		{
			get
			{
				return _x;
			}
			set
			{
				_x = value;
			}
		}
        /// <summary>
        /// y coordinate
        /// </summary>
		public BigRational Y
	{
		get
		{
			return _y;
		}
		set
		{
			_y = value;
		}
	}

        #endregion
        #region Constructors
        /// <summary>
        /// Construtor com numeros racionais
        /// </summary>
        /// <param name="x">Coordenada X</param>
        /// <param name="y">Coordenada Y</param>
        public Point(BigRational x, BigRational y)
        {
            _x = x;
            _y = y;
        }

        /// <summary>
        /// Creates a point in the orign
        /// </summary>
        public Point()
		{
			X = 0;
			Y = 0;
		}

		/// <summary>
		/// Creates a point with floats
		/// </summary>
		/// <param name="x">x coordinate</param>
		/// <param name="y">y coordinate</param>
		public Point(float x, float y)
		{

			_x = x;
			_y = y;
            //Console.WriteLine("Created the (" + X + ", " + Y + ") point");
		}	

		public Point(Point P)
		{
			X = P.X;
			Y = P.Y;

		}

		public Point(Euclidian._2.Point P)
		{
			X = P.X;
			Y = P.Y;
		}
	#endregion
	#region Methods

		/// <summary>
		/// Calculate and returns Euclidean distance from this point to a point P
		/// </summary>
		/// <param name="P">Point P</param>
		/// <returns>distance from this point to poitn P</returns>
		public double EuclidianDistance (Point P)
		{
			return Math.Sqrt(EuclidianPoweredDistance(P));
		}
	
		/// <summary>
		/// Calculate and returns Euclidean distance powered by 2 from this point to a point P
		/// </summary>
		/// <param name="P">Point P</param>
		/// <returns>distance powered</returns>
		public double EuclidianPoweredDistance(Point P)
		{
            BigRational aux = (this.X - P.X) * (this.X - P.X) + (this.Y - P.Y) * (this.Y - P.Y);
            return (double)(aux.Numerator/aux.Denominator);
		}

		
	
	#endregion
	#region Operators

		public static Point operator +(Point P,Vector V)
		{
			return new Point (P.X+V.X,P.Y+V.Y);
		}

        public static bool operator ==(Point T, Point P)
        {
            if (object.ReferenceEquals(T, null))
                return object.ReferenceEquals(P, null);
            if (object.ReferenceEquals(P, null))
                return object.ReferenceEquals(T, null);
            return (T.X==P.X)&&(T.Y==P.Y);
        }

        public override bool Equals(System.Object obj)
        {
            return this == obj as Point;
        }

        public static bool operator !=(Point T, Point P)
        {
            return !(T==P);
        }

        public override string ToString()
        {
            return "X : "+X+", Y : "+Y;
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
        #endregion
    }
}