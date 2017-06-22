using System;

namespace Metria.Hyperbolic._2
{
    public class LineSegment : Line
    {
        #region Variables
        public Line MediumBissector()
        {
            //Get the medium bissector
            //Agora a gambiarra fica seria
            if (A.Y == B.Y)
            {
                return new Line(new Point((A.X + B.X) / 2, A.Y), new Point((A.X + B.X) / 2, 0));
            }
            if (A.X != B.X)//reta torta
            {
                //manda A
                float P, Q, Ya, Yb;
                P = A.X - Alfa.X;
                Q = Beta.X - A.X;
                Ya = (A.Y * (P + Q)) / ((A.Y * A.Y) + (Q * Q));
                //Manda B
                P = B.X - Alfa.X;
                Q = Beta.X - B.X;
                Yb = (B.Y * (P + Q)) / ((B.Y * B.Y) + (Q * Q));
                //Sendo M o ponto medio da reta reta
                float M = Ya * (float)Math.Sqrt(Yb / Ya);
                //float M = (float)Math.Log(Yb/Ya);
                //Aplico a volta
                float NovoAlfa = (Beta.X * M * -1 + Alfa.X) / (1 - M);
                float NovoBeta = (Beta.X * M + Alfa.X) / (M + 1);
                //Retorno a reta
                return new Line(new Point(NovoAlfa, 0), new Point(NovoBeta, 0));
            }
            float _M = A.Y * (float)Math.Sqrt(B.Y / A.Y);
            return new Line(new Point(-_M + Center.X, 0), new Point(_M + Center.X, 0));
        }

        #endregion
        #region Constructor
        public LineSegment(LineSegment L) : base(L)
        {

        }

        public LineSegment(Point P, Point Q) : base(P, Q)
        {
            if (Beta.Y == -1)//Eh vertical
            {
                _a = (P.Y < Q.Y) ? new Point(P) : new Point(Q);
                _b = (P.Y > Q.Y) ? new Point(P) : new Point(Q);
                _director = new Vector(_center, P).Normal;
            }
            else //eh torta
            {
                _a = (P.X < Q.X) ? new Point(P) : new Point(Q);
                _b = (P.X > Q.X) ? new Point(P) : new Point(Q);
                _director = new Vector(_center, P).Normal;
                _radius = ((float)(P.EuclidianDistance(this._center)));
            }
        }

        //public LineSegment(LineSegment L) : base(L) { }

        //public LineSegment(Line L) : base(L) { }
        #endregion
        #region Methods


        public override Point IntersectionPoint(Line L)
        {
            Point P = base.IntersectionPoint(L);
            if (P == null) return null;
            if (P == A)
            {
                if (L.Beta.Y == -1)
                {
                    A = new Point(P);
                    return P;
                }
                return A;
            }
            if (P == B)
            {
                if (L.Beta.Y == -1)
                {
                    B = new Point(P);
                    return P;
                }
                return B;
            }
            //Erro aqui - incluir segmento na vertical
            if (this.A.X == this.B.X)
            {
                if (P.Y < A.Y || P.Y > B.Y) return null;
            }
            else
            {
                if (P.X < A.X || P.X > B.X) return null;
            }
            return P;
        }


        public override Line Cut(Line l, Point reference)
        {
            Point cutP = IntersectionPoint(l);
            if (cutP == null || cutP == A || cutP == B)
            {
                /*if (l.Beta.Y < 0)
                {
                    if (this.A.X < l.Center.X == reference.X < l.Center.X)
                        return this;
                    return null;
                }
                if (cutP == A)
                {
                    if (l.Center.EuclidianDistance(this.B) < l.Radius == l.Center.EuclidianDistance(reference) < l.Radius)
                        return this;
                }
                else
                {
                    if (l.Center.EuclidianDistance(this.A) < l.Radius == l.Center.EuclidianDistance(reference) < l.Radius)
                        return this;
                }*/
                return null;
            }
            if (this.Beta.Y < 0)//This é vertical
            {
                if (l.Center.EuclidianDistance(reference) < l.Radius)
                {
                    return new LineSegment(A, cutP);
                }
                return new LineSegment(B, cutP);
            }
            if (l.Beta.Y < 0)//l é vertical
            {
                if (reference.X < l.Center.X)
                    return new LineSegment(A, cutP);
                return new LineSegment(cutP, B);
            }
            //Se chegamos até aqui é porque nenhuma é vertical
            if (l.Center.EuclidianDistance(reference) < l.Radius == l.Center.EuclidianDistance(A) < l.Radius)
            {
                if (l.Center.EuclidianDistance(reference) < l.Radius == l.Center.EuclidianDistance(B) < l.Radius)
                {
                    return null;
                    //return this;
                }
                return new LineSegment(A, cutP);

            }
            return new LineSegment(B, cutP);


        }


        public static bool operator ==(LineSegment L1, LineSegment L2)
        {
            if (object.ReferenceEquals(L1, null))
                return object.ReferenceEquals(L2, null);
            if (object.ReferenceEquals(L2, null))
                return object.ReferenceEquals(L1, null);
            return (L1.A == L2.A && L1.B == L2.B);
        }

        public static bool operator !=(LineSegment L1, LineSegment L2)
        {
            return !(L1 == L2);
        }
        #endregion

    }
}