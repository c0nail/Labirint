using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Finder : IFinder {
		
	int sizeLab;

	IDraw drawLab;

	List<MazeIndex> path;
	int len;                       // длина пути
	int[,] grid;
	int[,] originalGrid;
		

	bool Find(int ax, int ay, int bx, int by)  
	{
		path = new List<MazeIndex>();
		int d, x, y;
		bool stop;
		grid = new int[sizeLab, sizeLab];
		for (int i = 0; i < sizeLab; i++)
		{	for (int j = 0; j < sizeLab; j++ )
			{
				grid[i,j] = originalGrid[i,j];
			}
		}

		// распространение волны
		d = 1000;
		grid[ay,ax] = 1000;            
		do {
			stop = true;               // предполагаем, что все свободные клетки уже помечены
			for (int i = 0; i < sizeLab; i++)
				for (int j = 0; j < sizeLab; j++ )
				if ( grid[i,j] == d )                        
				{
         
					if (i-1 >= 0 && grid[i-1,j] < 999 && (grid[i-1,j] == 0 || grid[i-1,j] == 1 || grid[i-1,j] == 4 || grid[i-1,j] == 5 || grid[i-1,j] == 8 ||
					                                   grid[i-1,j] == 9 || grid[i-1,j] == 12 || grid[i-1,j] == 13))
					{
						stop = false;                           
						grid[i-1,j] = d + 1; 
					}

					if (i+1 < sizeLab && grid[i+1,j] < 8)
					{
						stop = false;                                                     
						grid[i+1,j] = d + 1;
					}

					if (j-1 >= 0 && grid[i,j -1] < 999 && (grid[i,j -1] < 4 || (grid[i,j -1] > 7 && grid[i,j -1] < 12)))
					{
						stop = false;                                                 
						grid[i,j -1] = d + 1;
					}

					if (j+1 < sizeLab && grid[i,j +1] < 999 && (grid[i,j+1] == 0 || grid[i,j+1] == 2 || grid[i,j +1] == 4 || grid[i,j +1] == 6 ||
					                                     grid[i,j +1] == 8 || grid[i,j +1] == 10 || grid[i,j +1] == 12 || grid[i,j +1] == 14))
					{
						stop = false;                           
						grid[i,j+1] = d + 1;
					}


				}
			d++;
		} while ( !stop && grid[by,bx] < 999);


		if (grid[by,bx] < 999) return false;  // путь не найден

		// восстановление пути
		len = grid[by,bx];            // длина кратчайшего пути из (ax, ay) в (bx, by)
		x = bx;
		y = by;
		d = len;
		while ( d > 1000 )
		{
			path.Add(new MazeIndex(x,y));
			d--;

			if (x-1 >= 0 && grid[x-1,y] == d && (originalGrid[x-1,y] == 0 || originalGrid[x-1,y] == 1 || originalGrid[x-1,y] == 4 || originalGrid[x-1,y] == 5 || originalGrid[x-1,y] == 8 ||
			                                                                 originalGrid[x-1,y] == 9 || originalGrid[x-1,y] == 12 || originalGrid[x-1,y] == 13))
			{
				x -= 1;
			}else if (x+1 < sizeLab && grid[x+1,y] == d && originalGrid[x+1,y] < 8)
			{
				x += 1;
			}else if(y-1 >= 0 && grid[x,y-1] == d && (originalGrid[x,y -1] < 4 || (originalGrid[x,y -1] > 7 && originalGrid[x,y -1] < 12)))
			{
				y -= 1;
			}else if(y+1 < sizeLab && grid[x,y+1] == d && (originalGrid[x,y+1] == 0 || originalGrid[x,y+1] == 2 || originalGrid[x,y +1] == 4 || originalGrid[x,y +1] == 6 ||
			                                                              originalGrid[x,y +1] == 8 || originalGrid[x,y +1] == 10 || originalGrid[x,y +1] == 12 || originalGrid[x,y +1] == 14))
			{
				y += 1;
			}

		}

		path.Add(new MazeIndex(ax,ay));
		path.Reverse ();


		return true;
	}

	public List<Vector3> Find(ILabirint lab, IDraw drawLabirint)
	{
		originalGrid = lab.GetLab ();
		drawLab = drawLabirint;
		sizeLab = (int)Mathf.Sqrt (originalGrid.Length);
		Debug.Log(Find ((int)sizeLab/2, (int)sizeLab/2, sizeLab - 1, sizeLab - 1));

		List<Vector3> positionPath = new List<Vector3> ();
		for (int i = 0; i < path.Count; i++) {
			Vector3 pos = drawLab.GetLab()[path[i].x,path[i].y].transform.position;
			drawLab.GetLab()[path[i].x,path[i].y].GetComponent<SpriteRenderer>().color = Color.red;
			pos = Camera.main.WorldToScreenPoint(pos);
			pos.y = Screen.height - pos.y;
			positionPath.Add(pos);
		}

		return positionPath;
	}
	
}
