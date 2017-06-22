using System;

namespace Metria.Hyperbolic._2
{
    public class Line
	{
	#region Variables
		protected Point _a;
		public Point A
		{
			get
			{
				return this._a;
			}
			set
			{
				this._a = new Point(value);
			}
		}
		protected Point _b;
		public Point B
		{
			get
			{
				return this._b;
			}
			set
			{
				this._b = new Point(value);
			}
		}
		private Point _alfa;
		public Point Alfa
		{
			get
			{
				return _alfa;
			}
		}
		private Point _beta;
		public Point Beta
		{
			get
			{
				return _beta; 
			}
		}


		protected Point _center;
		public Point Center
		{
			get { return _center;}
		}
		protected float _radius;
		public float Radius
		{
			get
			{
				return _radius;
			}
		}
		protected Vector _director;
		public Vector Director
		{
			get
			{
				return _director;
			}
		}
	#endregion
	#region Constructor

              
		public Line(Point P, Point Q)
		{
            _a = new Point(P);
            _b = new Point(Q);
			if(P.X == Q.X) // se for vertical
			{
				_a = new Point((P.Y < Q.Y) ? P : Q);
				_b = new Point((P.Y < Q.Y) ? Q : P);
                _center = new Point(_a.X, 0);
                _radius = 0;
				_alfa = new Point (Center.X,0);
				_beta = new Point (Center.X,-1);
                _director = new Vector(new Point(0,1));

            }
            else
			{
				_a = new Point((P.X < Q.X) ? P : Q);
				_b = new Point((P.X < Q.X) ? Q : P);
                if (A.Y == B.Y)
                    _center = new Point((A.X + B.X) / 2, 0);
                else
                {
                    Euclidian._2.LineSegment aux = new Euclidian._2.LineSegment(new Euclidian._2.Point(A.X, A.Y), new Euclidian._2.Point(B.X, B.Y));
                    Euclidian._2.Point pointaux = new Euclidian._2.Point((A.X + B.X) / 2, (A.Y + B.Y) / 2);
                    Euclidian._2.Line L = new Euclidian._2.Line(pointaux, aux.Director.Normal);
                    Euclidian._2.Point collision = L.IntersectionPoint(new Euclidian._2.Line(new Euclidian._2.Point(), new Euclidian._2.Point(1, 0)));
                    _center = new Point(collision);
                }
                _radius = (float)A.EuclidianDistance(Center);
                _alfa = new Point(Center.X-Radius, 0);
				_beta = new Point(Center.X+Radius, 0);
                _director = new Vector(Center, A).Normal;

            }

        }

		public Line(Line L)
		{
			_a = L.A;
			_b = L.B;
			_alfa = L.Alfa;
			_beta = L.Beta;
            _center = L.Center;
            _radius = L.Radius;
			_director = new Vector(_center, _a).Normal;
		}

	#endregion
	#region Methods



		public float EuclidianDistanceFromCenter(Point P)
		{
			return (float) P.EuclidianDistance(_center);
		}

		public float EuclidianPoweredDistanceFromCenter(Point P)
		{
			return (float)P.EuclidianPoweredDistance(_center);
		}

		public virtual Point IntersectionPoint (Line L)
		{
            //esse codigo fede um pouquinho
            if (L.Alfa == this.Alfa && L.Beta == this.Beta)
                return null;
			//caso 0 - Eu sou vertical
			if(this.Beta.Y < 0)
			{
				if(L.Beta.Y < 0) //L é vertical;
				{
					return null;
				}
				if(_center.X<=L.Alfa.X||_center.X>=L.Beta.X) return null; //L nao me encontra
				return new Point(A.X,(float)Math.Sqrt(L.Radius*L.Radius-Math.Pow(A.X- L._center.X,2)));
			}
			
			//caso 1 - sou torta

			//caso 1.1 L é vertical

			if(L.Beta.Y == -1)
			{
				if (L.Center.X <= this.Alfa.X || L.Center.X >= this.Beta.X) return null; //L nao me encontra
				return new Point(L.A.X, (float)Math.Sqrt(this.Radius * this.Radius - Math.Pow(Center.X - L.Center.X, 2)));
			}


            //caso 1.2 - ambas tortas
            //POG ---->   if ((this.Alfa.X<L.Alfa.X&&this.Beta.X<L.Beta.X)||(this.Alfa.X>L.Alfa.X&&this.Beta.X>L.Beta.X))
            if(this.EuclidianDistanceFromCenter(L.Alfa)<this.Radius != this.EuclidianDistanceFromCenter(L.Beta) < this.Radius)
            {

                //float Z = (float)(((-(L._center.X+this._center.X)*(L._center.X-this._center.X))+((L._radius+this._radius)*(L._radius-this._radius)))/(2*(-L._center.X + this._center.X)));
                float X = (float)((Math.Pow(this.Center.X, 2) - Math.Pow(L.Center.X, 2) - Math.Pow(this.Radius, 2) + Math.Pow(L.Radius, 2)) / (2 * (this.Center.X - L.Center.X)));
                float Y = (float) Math.Sqrt((this._radius*this._radius)-((X-this._center.X)*(X-this._center.X)));
			
				return new Point(X,Y);
			}

			return null;
		}

        /*public bool fakeIntersectX(Line L)
        {
            //esse codigo fede um pouquinho
            if (L.Alfa == this.Alfa && L.Beta == this.Beta)
                return false;
            //caso 0 - Eu sou vertical
            if (this.Beta.Y < 0)
            {
                if (L.Beta.Y < 0) //L é vertical;
                {
                    return false;
                }
                if (_center.X <= L.Alfa.X || _center.X >= L.Beta.X) return false; //L nao me encontra
                Point P = new Point(A.X, (float)Math.Sqrt(L.Radius * L.Radius - Math.Pow(A.X - L._center.X, 2)));

            }

            //caso 1 - sou torta

            //caso 1.1 L é vertical

            if (L.Beta.Y == -1)
            {
                if (L.Center.X <= this.Alfa.X || L.Center.X >= this.Beta.X) return null; //L nao me encontra
                return new Point(L.A.X, (float)Math.Sqrt(this.Radius * this.Radius - Math.Pow(Center.X - L.Center.X, 2)));
            }


            //caso 1.2 - ambas tortas
            //POG ---->   if ((this.Alfa.X<L.Alfa.X&&this.Beta.X<L.Beta.X)||(this.Alfa.X>L.Alfa.X&&this.Beta.X>L.Beta.X))
            if (this.EuclidianDistanceFromCenter(L.Alfa) < this.Radius != this.EuclidianDistanceFromCenter(L.Beta) < this.Radius)
            {

                //float Z = (float)(((-(L._center.X+this._center.X)*(L._center.X-this._center.X))+((L._radius+this._radius)*(L._radius-this._radius)))/(2*(-L._center.X + this._center.X)));
                float X = (float)((Math.Pow(this.Center.X, 2) - Math.Pow(L.Center.X, 2) - Math.Pow(this.Radius, 2) + Math.Pow(L.Radius, 2)) / (2 * (this.Center.X - L.Center.X)));
                float Y = (float)Math.Sqrt((this._radius * this._radius) - ((X - this._center.X) * (X - this._center.X)));

                return new Point(X, Y);


            }
        }*/

        /// <summary>
        /// Este metodo corta a reta em duas, este metodo não considera retas coincidentes
        /// </summary>
        /// <param name="L">Reta do Corte</param>
        /// <param name="reference">Ponto de Referencia</param>
        /// <returns></returns>
        public virtual Line Cut(Line l, Point reference)
        {
            if(this == l)
            {
                //return this;
                return null;
            }
            Point Aux = l.IntersectionPoint(this);
            if (Aux == null)
            {
                //if(l.Beta.Y==-1)
                //{
                    //if (this.Center.X < l.Center.X == reference.X < l.Center.X)
                    //    return this;
                //    return null;
                //}
                //if (l.Center.EuclidianDistance(this.Center) < l.Radius == l.Center.EuclidianDistance(reference) < l.Radius)
                //    return this;
                //else
                    return null;
            }
            if (this.Beta.Y == -1)
            {//this é vertical
                if(l.Center.EuclidianDistance(reference)<l.Radius)
                {
                    return new Ray(Aux,new Point(Aux.X,Aux.Y/2));
                }
                return new Ray(Aux, new Point(Aux.X,Aux.Y+1));
            }
            if (l.Beta.Y == -1)
            {//l é vertical
                if (reference.X<l.Center.X)
                {
                    return new Ray(this.Alfa, Aux);
                }
                return new Ray(Aux, this.Beta);
            }
            //Ambas são aros
            if(this.Alfa.X<l.Alfa.X)
            {
                if(this.Center.EuclidianDistance(reference)<this.Radius)
                {
                    if(l.Center.EuclidianDistance(reference)<l.Radius)
                    {
                        return new Ray(Aux, this.Beta);
                    }
                    else
                    {
                        return new Ray(Aux, this.Alfa);
                    }
                }
                if (l.Center.EuclidianDistance(reference) < l.Radius)
                {
                    return new Ray(Aux, this.Beta);
                }
                return new Ray(Aux,this.Alfa);
            }
            if (this.Center.EuclidianDistance(reference) < this.Radius)
            {
                if (l.Center.EuclidianDistance(reference) < l.Radius)
                {
                    return new Ray(Aux, this.Alfa);
                }
                return new Ray(this.Beta, Aux);
            }
            if (l.Center.EuclidianDistance(reference) < l.Radius)
            {
                return new Ray(Aux, this.Alfa);
            }
            return new Ray(Aux, this.Beta);
        }
        

		public virtual bool IsInside(Point P)
		{
			return Math.Abs(P.EuclidianPoweredDistance(_center) - _radius) < float.Epsilon;
		}

        public static bool operator == (Line L1, Line L2)
        {
            if (object.ReferenceEquals(L1, null))
                return object.ReferenceEquals(L2, null);
            if (object.ReferenceEquals(L2, null))
                return object.ReferenceEquals(L1, null);
            return L1.Alfa==L2.Alfa&&L1.Center==L2.Center&&L1.Beta==L2.Beta;
        }

        public static bool operator !=(Line L1, Line L2)
        {
            return !(L1 == L2);
        }

        public override string ToString()
        {
            return "Alfa : ( " + Alfa.ToString() + "), Beta : (" + Beta.ToString() + "), Center : (" + Center.ToString() + ")";
        }

        public override bool Equals(object obj)
        {
            return this == obj as Line;
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        #endregion
    }
}
