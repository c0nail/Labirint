using UnityEngine;
using System.Collections;

public class MazeIndex {
	
	public int x;
	public int y;

	public MazeIndex(int x, int y)
	{
		this.x = x;
		this.y = y;
	}
	
	
	public string toString()
	{
		return "[" + x + ", " + y + "]";
	}
}
