using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameCore : MonoBehaviour {
	
	bool generateLab = false; 
	LabirintFactory labFact = new LabirintFactory();
	ILabirint labirint;
	IDraw drawLabirint;
	IFinder finder;

	bool show = false;
	List<Vector3> path;

	public CreateBlock pool;

	private static GameCore INSTANCE = null;

	public static GameCore GetInstance () {
		return INSTANCE;
	}

	// Use this for initialization
	void Awake () {
		labirint = labFact.buildLab ();
		drawLabirint = labFact.buildDraw ();
		finder = labFact.buildFind ();

		if (GameCore.INSTANCE == null) {
			GameCore.INSTANCE = this;
			GameObject.DontDestroyOnLoad (this);
		} else {
			GameObject.Destroy (this.gameObject);
		}
	}

	public void GenerateLabirint(int size)
	{
		if(size * size < pool.SizePool)
		{
			show = false;
			labirint.Generate (size);
			drawLabirint.Draw (labirint, pool);
			generateLab = true;
		}
	}

	public void ShowWay()
	{
		if (generateLab) {
			show = true;
			path = finder.Find (labirint,drawLabirint);	
		}
	}

	void OnGUI()
	{
		if(show)
		{
			for(int i = 0; i < path.Count -1;i++)
			{
				Drawing.DrawLine(path[i],path[i+1],Color.green);
			}
		}
	}



}
