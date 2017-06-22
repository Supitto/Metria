using System;

namespace Metria.Hyperbolic._2
{
    /// <summary>
    /// A ray going from point A and passing throught point B
    /// </summary>
	public class Ray : Line
	{
	#region Variables

		

	#endregion
	#region Constructors

		public Ray(Ray r) : base(r)
		{
			 this._a = r._a;
			 this._b = r._b;
		}

		public Ray(Point P, Point Q) : base (P, Q) 
		{
            if (P.Y == -1)
                throw new Exception("A ray may never begin on infinty");
            _a = new Point(P);

			if(Beta.Y == -1) // Se for vertical
            {
                if(Q.Y > P.Y)
                {
                    _b = new Point(Beta);
                    _director = new Vector(0, 1);

                }
                else
                {
                    _b = new Point(Alfa);
                    _director = new Vector(0, -1);

                }
            }
            else
            {
                if(P.X < Q.X)
                {
                    _b = new Point(Beta);
                    _director = new Vector(_center, B).Normal;

                }
                else
                {
                    _b = new Point(Alfa);
                    _director = new Vector(_center, B).Normal;

                }
            }

		}

	#endregion
	#region Methods
        //FIX ME
		public override Point IntersectionPoint(Line L)
		{
            Point P = base.IntersectionPoint(L);
		    if(P==null) return null;
            if (P == A) return A;
            if (this.Beta.Y != -1)//reta torta
            {
                if (P.X < this.A.X == P.X > this.B.X) //XOR
                {
                    return P;
                }
            }
            else
            {
                if (this.B.Y == -1)
                {
                    if (P.Y > this.A.Y) return P;
                }
                else
                {
                    if (P.Y < this.A.Y) return P;
                }
            }
            return null;
		}

		public override bool IsInside(Point P)
		{
		    throw new NotImplementedException("Ainda não implementado");
		}

        public override Line Cut (Line l, Point reference)
        {
            if (this == l) return null;
            Point P = this.IntersectionPoint(l);
            if (P == null) //No cut
            {
                /*if (l.Center.EuclidianDistance(this.A) < l.Radius == l.Center.EuclidianDistance(reference) < l.Radius)
                {
                    return this;
                }
                else
                {
                    return null;
                }*/
                return null;
            }
            if (P == A)
            {
                /*if((B.Y == 0 )==( l.Center.EuclidianDistance(reference) < l.Radius))
                {
                    return this;
                }*/
                return null;
            }
            if(this.Beta.X == this.Alfa.X)//this is a vertical ray
            {
                if (l.Center.EuclidianDistance(reference)<l.Radius)
                {
                    if(l.Center.EuclidianDistance(this.A) < l.Radius)
                    {
                        return new LineSegment(this.A, P);
                    }
                    return new Ray(P, this.Beta);
                }
                if (l.Center.EuclidianDistance(this.A) < l.Radius)
                {
                    return new Ray(P,this.Beta);
                }
                return new Ray(P, this.A);
            }
            if(l.Beta.X == l.Alfa.X)//l is vertical
            {
                if(reference.X<l.Center.X)//Ponto de referencia esta a direita
                {
                    if(this.Beta.X<l.Center.X)
                    {
                        return new Ray(P, this.Beta);
                    }
                    return new LineSegment(P, this.A);
                }
                if (this.Beta.X < l.Center.X)
                {
                    return new LineSegment(P, this.A);
                }
                return new Ray(P, this.Beta);
            }
            //Se chegamos aqui significa que tanto a this quanto a l são arcos

            if(l.Center.EuclidianDistance(reference)<l.Radius)
            {
                if(l.Center.EuclidianDistance(this.A) < l.Radius)
                {
                    return new LineSegment(this.A, P);
                }
                return new Ray(P, this.B);
            }
            if (l.Center.EuclidianDistance(this.A) < l.Radius)
            {
                return new Ray(P, this.B);
            }
            return new LineSegment(this.A, P);
        }

        #endregion
        public static bool operator ==(Ray R1, Ray R2)
        {
            if (object.ReferenceEquals(R1, null))
                return object.ReferenceEquals(R2, null);
            if (object.ReferenceEquals(R2, null))
                return object.ReferenceEquals(R1, null);
            return (R1.A == R2.A && R1.B == R2.B);
        }

        public static bool operator !=(Ray R1, Ray R2)
        {
            return !(R1 == R2);
        }
    }
}
