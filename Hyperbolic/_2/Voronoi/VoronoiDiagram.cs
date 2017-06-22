using System.Collections.Generic;

namespace Metria.Hyperbolic._2.Voronoi
{
    public class VoronoiDiagram
	{
	#region Variables

		static List<VoronoiCell> _cells;
		public List<VoronoiCell> Cells
		{
			get
			{
				return _cells;
			}
		}

	#endregion
	#region Constructors

		public VoronoiDiagram ()
		{
			_cells = new List<VoronoiCell> ();
		}

	#endregion
	#region Methods

		public bool AddPoint (Point P)
		{
			Line cut = null;

			//----------------------------------------first part-------------------------
			if (Cells.Count == 0)
			{
				_cells.Add(new VoronoiCell(P));
				return true;
			}
			_cells.Add(new VoronoiCell(P));

			//Get the medium bissector
			LineSegment aux = new LineSegment(_cells[0].Center, _cells[1].Center);
			//Agora a gambiarra fica seria
			cut = aux.MediumBissector();
			if (Cells.Count == 1)
			{
				_cells[0].Sides.Add(new VoronoiLine(cut,_cells[1],1));
				_cells[1].Sides.Add(new VoronoiLine(cut,_cells[0],0));
				return true;
			}

			//--------------------------------------second part-----------------------

			Queue<int> indexToCut = new Queue<int>(); //Queue with the index of the voroni cells to cut

			//Finds which cell contains P
			int initialIndex;
			bool worked = false;
			for (initialIndex = 0; initialIndex < Cells.Count; initialIndex++)
			{
				if(Cells[initialIndex].IsInsideSpecial(P))
				{
					worked = true;
					break;
				}
			}
			if(!worked) return false;
			
            //Start the algorithm
			bool [] visited = new bool [Cells.Count];
            for (int i = 0;  i < Cells.Count; i++)
            {
                visited[i] = false;
            }
            visited[initialIndex] = true;
			visited[Cells.Count-1] = true;
            indexToCut.Enqueue(initialIndex);
			List<int> nextIndexes; //aux
			while(indexToCut.Count>0)
			{

				int currentIndex = indexToCut.Dequeue();//gets the next index
				bool haveCut = true; //POG
                cut = new LineSegment(P, Cells[currentIndex].Center).MediumBissector();
				nextIndexes = Cells[currentIndex].CutPoligon(cut, ref haveCut);
				if(haveCut)
				{
					Cells[currentIndex].Sides[Cells[currentIndex].Sides.Count - 1].IndexNeigbor = Cells.Count-1;
					Cells[currentIndex].Sides[Cells[currentIndex].Sides.Count - 1].Neigbor = Cells[Cells.Count - 1];

					VoronoiLine support = Cells[currentIndex].Sides[Cells[currentIndex].Sides.Count-1];

					Cells[Cells.Count-1].Sides.Add(new VoronoiLine(support._line));
					Cells[Cells.Count - 1].Sides[Cells[Cells.Count - 1].Sides.Count - 1].IndexNeigbor = currentIndex;
					Cells[Cells.Count - 1].Sides[Cells[Cells.Count - 1].Sides.Count - 1].Neigbor = Cells[currentIndex];
				}
                for (int i = 0; i < nextIndexes.Count; i++)
				{
					if(!visited[nextIndexes[i]])
					{
						visited[nextIndexes[i]] = true;
						indexToCut.Enqueue(nextIndexes[i]);
					}
				}
			}
			return true;
			
		}


	#endregion
	}
}