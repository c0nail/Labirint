using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MazeMatrix : ILabirint {

	private int _width;
	private int _height;
	private int _startX;
	private int _startY;
	private int _finishX;
	private int _finishY;
	private int[,] matrix;
	public List<MazeIndex> activeRoute = new List<MazeIndex>();
	private int currentStep;
	
	private const int WALL_AROUND = 15;
	public const int WALL_TOP  = 1;
	public const int WALL_RIGHT  = 2;
	public const int WALL_BOTTOM = 4;
	public const int WALL_LEFT  = 8;
	public const int CELL_VISITED  = 128;

	public void Generate(int SizeX)
	{
		_width    = SizeX;
		_height    = SizeX;
		_startX = 0;
		_startY = 0;
		_finishX    = SizeX - 1;
		_finishY    = SizeX - 1;
		matrix = new int[SizeX,SizeX];
		
		for (int i = 0; i < SizeX; ++i)
		{
			for(int j = 0; j < SizeX; j++)
			{
				matrix[i,j] = WALL_AROUND;
			}
		}
		
		activeRoute.Add(new MazeIndex(_startX, _startY));
	//	matrix[_finishX,_finishY] = CELL_VISITED;
		currentStep = 0;

		createRoute ();

		for (int i = 0; i < SizeX; ++i)
		{
			for(int j = 0; j < SizeX; j++)
			{
				matrix[i,j] -=128;
			}
		}
	}

	public int[,] GetLab()
	{
		return matrix;
	}

			
	public int getFinishX()
	{
		return _finishX;
	}
	
	public int getFinishY()
	{
		return _finishY;
	}
	
	public int getCell(int x, int y)
	{
		return matrix[x,y];
	}
	
	public bool isCompleted()
	{
		return (activeRoute.Count == 0);
	}
	

	
	// Building maze routes
	void createRoute()
	{
		while(doStep()) {}
	}
	
	
	bool doStep()
	{
		// If now way (maze is completed)
		if (activeRoute.Count == 0) return false;
		
		MazeIndex p = activeRoute[currentStep];
		bool wayExist = canMove(p, WALL_TOP) || canMove(p, WALL_RIGHT) ||
			canMove(p, WALL_BOTTOM) || canMove(p, WALL_LEFT);
		if (wayExist)
		{
			switch (Random.Range(0,4))
			{
			case 0:
				if (canMove(p, WALL_TOP)) { addRoute(p, WALL_TOP); }
				break;
			case 1:
				if (canMove(p, WALL_RIGHT)) { addRoute(p, WALL_RIGHT); }
				break;
			case 2:
				if (canMove(p, WALL_BOTTOM)) { addRoute(p, WALL_BOTTOM);  }
				break;
			case 3:
				if (canMove(p, WALL_LEFT)) { addRoute(p, WALL_LEFT); }
				break;
			}                
			// Continue route
			currentStep = activeRoute.Count - 1;
		}
		else
		{
			// Remove deadlock route point
			activeRoute.RemoveRange(currentStep, 1);
			
			// Start a new route
			currentStep = 0;
			
			// Return false, if maze is completed
			return (activeRoute.Count > 0);
		}
		return true;
	}
	
	
	private void addRoute(MazeIndex pos,int direct)
	{
		matrix[pos.x,pos.y] = matrix[pos.x,pos.y] & (0xFFFF ^ direct);
		MazeIndex newIndex = new MazeIndex(
			pos.x + (direct == WALL_LEFT ? -1 : (direct == WALL_RIGHT ? 1 : 0)),
			pos.y + (direct == WALL_TOP ? -1 : (direct == WALL_BOTTOM ? 1 : 0))
			);
		matrix[newIndex.x,newIndex.y] = (matrix[newIndex.x,newIndex.y] | CELL_VISITED) & (0xFFFF ^ backDirect(direct));
		if ((newIndex.x == _finishX) && (newIndex.y == _finishY)) { return; }
		activeRoute.Add(newIndex);
	}
	
	
	// Returns inverse direction
	private int backDirect(int direct)
	{
		switch (direct)
		{
		case WALL_TOP: return WALL_BOTTOM;
		case WALL_RIGHT: return WALL_LEFT;
		case WALL_BOTTOM: return WALL_TOP;
		case WALL_LEFT: return WALL_RIGHT;
		}
		return 0;
	}
	
	
	// Check a movement
	private bool canMove(MazeIndex pos,int direct)
	{
		switch (direct)
		{
		case (WALL_TOP):
			return (pos.y > 0) && ((matrix[pos.x,pos.y - 1] & CELL_VISITED) == 0);
		case (WALL_RIGHT):
			return (pos.x < _width - 1) && ((matrix[pos.x + 1,pos.y] & CELL_VISITED) == 0);
		case (WALL_BOTTOM):
			return (pos.y < _height - 1) && ((matrix[pos.x,pos.y + 1] & CELL_VISITED) == 0);
		case (WALL_LEFT):
			return (pos.x > 0) && ((matrix[pos.x - 1,pos.y] & CELL_VISITED) == 0);
		}
		return false;
	}

}























