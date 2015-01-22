using UnityEngine;
using System.Collections;

public class GameCore : MonoBehaviour {

	public UnityEngine.UI.InputField Size;
	bool generateLab = false; 
	LabirintFactory labFact = new LabirintFactory();
	ILabirint labirint;
	IDraw drawLabirint;
	IFinder finder;

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
	
	// Update is called once per frame


	public void GenerateLabirint()
	{
		if(Size.text != string.Empty && int.Parse(Size.text) > 2)
		{
			labirint.Generate (int.Parse(Size.text));
			drawLabirint.Draw (labirint);
			generateLab = true;
			LineRenderer lineRender = GameObject.FindObjectOfType<LineRenderer> ();
			lineRender.SetVertexCount (0);
		}
	}

	public void ShowWay()
	{
		if (generateLab) {
			finder.Find (labirint,drawLabirint);	
		}
	}



}
