namespace Metria.Hyperbolic._2.Voronoi
{
    public class VoronoiLine
	{
	#region Variables

		protected VoronoiCell _neigbor;
		public VoronoiCell Neigbor
		{
			get
			{
				return _neigbor;
			}
			set
			{
				_neigbor = value;
			}
		}
		protected int _indexNeigbor;
		public int IndexNeigbor
		{
			get
			{
				return _indexNeigbor;
			}
			set
			{
				_indexNeigbor = value;
			}
		}

        public Line _line;

        public Point A
        {
            get
            {
                return _line.A;
            }
            set
            {
                _line.A = value;
            }
        }

		public Point B
		{
			get
			{
				return _line.B;
			}
			set
			{
				_line.B = value;
			}
		}

		public float Radius
		{
			get
			{
				return _line.Radius;
			}
		}

        public Vector Director
        {
            get
            {
                return _line.Director;
            }
        }

	#endregion
	#region Constructors
		public VoronoiLine(Line L)
		{
			IndexNeigbor = -1;
			Neigbor = null;
			_line = ParseLine(L);
		}
		public VoronoiLine(Line L, VoronoiCell neigbor)
		{
			IndexNeigbor = -1;
			Neigbor = neigbor;
			_line = ParseLine(L);
           
		}
		public VoronoiLine(Line L, int indexNeigbor)
		{
			IndexNeigbor = indexNeigbor;
			Neigbor = null;
			_line = ParseLine(L);
		}
		public VoronoiLine(Line L, VoronoiCell neigbor, int indexNeigbor)
		{
			IndexNeigbor = indexNeigbor;
			Neigbor = neigbor;
			_line = ParseLine(L);
		} 
		#region suporte

		static Line ParseLine(Line L)
		{
			if (L is Ray)  return new Ray(L as Ray);
			else if (L is LineSegment) return new LineSegment(L as LineSegment);
			else return new Line(L);
		}

		#endregion

	#endregion
    #region methods

       	public Point IntersectionPoint (Line L)
        {
            return _line.IntersectionPoint(L);
        }

        public Line Cut (Line L, Point Referencia)
        {
            return _line.Cut(L, Referencia);
        }

        public bool IsInside(Point P)
        {
            return _line.IsInside(P);
        }

		public float EuclidianDistanceFromCenter(Point P)
		{
			return _line.EuclidianDistanceFromCenter(P);
		}
    #endregion
    }
}
