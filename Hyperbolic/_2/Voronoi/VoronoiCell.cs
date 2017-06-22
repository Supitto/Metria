using System.Collections.Generic;

namespace Metria.Hyperbolic._2.Voronoi
{
    public class VoronoiCell : ConvexPoligon
	{
	#region Variables

		protected Point _center;

		public Point Center
		{
			get
			{
				return _center;
			}
		}

		private new List<VoronoiLine> _sides;

		public new List<VoronoiLine> Sides
		{
			get
			{
				return _sides;
			}
			set
			{
				_sides = value;
			}
		}
	#endregion
	#region Constructors
		//must have to make lists
		public VoronoiCell() : base() 
		{
			_sides = new List<VoronoiLine>();
		}

		public VoronoiCell( Point center) : base ()
		{
			_center = center;
			_sides = new List<VoronoiLine>();
		}

		public VoronoiCell( Point center, List<VoronoiLine> sides)
		{
			_sides = sides;
			_center = center;
		}

		public VoronoiCell(VoronoiCell C) : base()
		{
			_sides = new List<VoronoiLine>(C._sides);
			_center = new Point(C._center);
		}
	#endregion
	#region Methods
		//working good
		public override bool IsInside (Point P)
		{
			bool Inside = true; //Variable used to return the value
			for(int i = 0; i < _sides.Count; i++)
			{
                bool Ponto;
                bool Centro;
                if (Sides[i]._line.Beta.Y == -1)
                {
                    Ponto = Sides[i].A.X < P.X;
                    Centro = Sides[i].A.X < Center.X;
                }
                else
                {
                    Ponto = _sides[i]._line.EuclidianDistanceFromCenter(P) >= _sides[i].Radius;
                    Centro = _sides[i]._line.EuclidianDistanceFromCenter(_center) >= _sides[i].Radius;
                }
				if(Ponto != Centro)
				{
					Inside = false;
					break;
				}
			}
			return Inside;
		}
		
        public bool IsInsideSpecial(Point P)
        {
            return IsInside(P) && P != Center;
        }

		/// <summary>
		/// Cut the voronoi cells in two, and discard the one that does not have the center of the cell included
		/// </summary>
		/// <param name="L"></param>
		/// <returns></returns>
		public List<int> CutPoligon(Line L, ref bool haveCut) //TODO Impelementar uma solução mais elegante para havecut
		{
            List<int> nextCut = new List<int>();
            //security check
            foreach (VoronoiLine l in _sides)
            {
                if (L.EuclidianDistanceFromCenter(l.A) == L.Radius && L.EuclidianDistanceFromCenter(l.B) == L.Radius )
                    return nextCut;
            }
            List<Point> intersecta = new List<Point>();
            /*{
                List<Point> DieFast = new List<Point>();
                Point AuxGonnaDie = null;
                for (int i = Sides.Count - 1; i >= 0; i--)
                {
                    AuxGonnaDie = Sides[i].IntersectionPoint(L);
                    if (AuxGonnaDie!=null)
                    {
                        if(DieFast.Contains(AuxGonnaDie))
                        {
                            if()
                            return nextCut;
                        }
                        else
                        {
                            DieFast.Add(AuxGonnaDie);
                        }
                    }
                }
            }
            //end of security check*/
            for (int i = Sides.Count-1; i >= 0; i--)
            {
                Point auxp = Sides[i]._line.IntersectionPoint(L);
                if(auxp !=  null)
                {
                    bool contem = false;
                    foreach(Point p in intersecta)
                    {
                        if (p == auxp)
                            contem = true;
                    }
                    if(!contem)
                    {
                        intersecta.Add(new Point(auxp));
                        nextCut.Add(Sides[i].IndexNeigbor);
                    }
                    /*if (!intersecta.Contains(auxp))
                    {
                        intersecta.Add(new Point(auxp));
                        nextCut.Add(Sides[i].IndexNeigbor);
                    }*/
                }

                Line auxl = Sides[i]._line.Cut(L,Center);
                
                if (auxl!=null)
                {
                    Sides[i]._line = auxl;
                }
                else
                {
                    if (L.Beta.Y == 0)//L é um arco
                    {
                        if (this.Sides[i].B.Y == -1)
                        {
                            if ((L.EuclidianDistanceFromCenter(this.Center) < L.Radius) == (this.Sides[i].B == this.Sides[i]._line.Beta))
                            {
                                Sides.RemoveAt(i);
                            }
                        }
                        else
                        {
                            
                            bool paraA = (L.EuclidianDistanceFromCenter(this.Sides[i]._line.A) < L.Radius)
                            , paraB = (L.EuclidianDistanceFromCenter(this.Sides[i]._line.B) < L.Radius)
                            , paraCentro = (L.EuclidianDistanceFromCenter(this.Center) < L.Radius);
                            if (auxp == this.Sides[i]._line.A)
                                paraA = false;
                            else
                                if (auxp == this.Sides[i]._line.B)
                                    paraB = false;
                            //(
                            if (paraCentro)
                            {

                                if(!(paraB||paraA))
                                {
                                    Sides.RemoveAt(i);
                                }
                            }
                            else
                            {
                                if(paraB||paraA)
                                {
                                    Sides.RemoveAt(i);
                                }
                            }
                            /*if((paraA!=paraCentro)&&(paraB!=paraCentro))
                            {
                                Sides.RemoveAt(i);
                            }
                            /*
                            bool paraA = (L.EuclidianDistanceFromCenter(this.Sides[i]._line.A) <= L.Radius)
                            , paraB = (L.EuclidianDistanceFromCenter(this.Sides[i]._line.B) <= L.Radius)
                            , paraCentro = (L.EuclidianDistanceFromCenter(this.Center) < L.Radius);
                            if (paraA != paraCentro || paraB != paraCentro)
                            {
                                Sides.RemoveAt(i);
                            }
                            */
                            /*else
                            {
                                if(L.EuclidianDistanceFromCenter(this.Sides[i]._line.A) == L.Radius)
                                {
                                    if(paraB != paraCentro)
                                    {
                                        Sides.RemoveAt(i);
                                    }
                                }
                                else if(L.EuclidianDistanceFromCenter(this.Sides[i]._line.B) == L.Radius)
                                {
                                    if(paraB != paraCentro)
                                    {
                                        Sides.RemoveAt(i);
                                    }
                                }
                            }*/
                            /*if ( paraA != paraCentro)
                            {
                                Sides.RemoveAt(i);
                            }
                            else if (paraB != paraCentro)
                            {
                                Sides.RemoveAt(i);
                            }*/
                        }
                    }
                    else //L ´´e vertical
                    {
                        if ((L.Center.X < Sides[i]._line.A.X) != (L.Center.X < Center.X))
                        {
                            Sides.RemoveAt(i);
                        }
                        else if ((L.Center.X < Sides[i]._line.B.X) != (L.Center.X < Center.X))//FIX ME
                        {
                            Sides.RemoveAt(i);
                        }
                    }

                }
            }//Fim do loop
            //Daqui pra frente é onde inserimos a nova aresta
            if(intersecta.Count == 2)//Se intersectarmos duas, criamos um segmento
            {
                /*if(L.Beta.Y==-1)
                {
                    int X = 0;
                    for(int j = 0; j<_sides.Count;j++)
                    {
                        if(intersecta[0] == _sides[j])
                    }
                }*/
                this.Sides.Add(new VoronoiLine(new LineSegment(intersecta[0], intersecta[1])));
            }
            else if(intersecta.Count == 1)//Se intersectarmos uma criamos uma semireta
            {
                bool estagio1 = false, estagio2 = false;//verificar com o paiva
                foreach(VoronoiLine l in _sides)
                {
                    if(intersecta[0]==l.A||intersecta[0]==l.B)
                    {
                        if (estagio1) estagio2 = true;
                        else estagio1 = true;
                    }
                }
                if (!estagio2)
                {
                    if (this.IsInside(L.Alfa))//Se alfa esta dentro da celula entao o segmento vai de p a alfa
                    {
                        this.Sides.Add(new VoronoiLine(new Ray(intersecta[0], L.Alfa)));
                    }
                    else//caso contrario a semireta vai de p a beta
                    {
                        this.Sides.Add(new VoronoiLine(new Ray(intersecta[0], L.Beta)));
                    }
                }
            }
            else//Caso a reta não intersecta 
            {
                if (L.Beta.Y == -1)
                {
                    if (this.IsInsideSpecial(L.Center))
                    {
                        this.Sides.Add(new VoronoiLine(new Line(L)));//adiciona a reta
                    }
                }
                else
                {
                    bool alfadentro = this.IsInside(L.Alfa);
                    bool betadentro = this.IsInside(L.Beta);
                    if (alfadentro && betadentro)
                    {//e caso a L esteja contida na celula
                        this.Sides.Add(new VoronoiLine(new Line(L)));//adiciona a reta
                    }
                }
            }
        
            return nextCut;
			
            
       }
        #endregion
        public override string ToString()
        {
            string wow = "";
            foreach (VoronoiLine l in _sides)
            {
                wow += l._line.ToString();
            }
            return wow;
        }
    }
}