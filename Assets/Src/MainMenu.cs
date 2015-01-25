using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	Rect posBtnGeneration;
	Rect posBtnPath;
	Rect posInputField;

	string field = string.Empty;
	// Use this for initialization
	void Start () {
		Calculation ();
	}

	void Calculation()
	{
		posBtnGeneration = new Rect (10, 10, 100, 20);
		posBtnPath = new Rect (10, 40, 100, 20);
		posInputField = new Rect (10, 70, 100, 20);

	}
	
	void OnGUI()
	{
		if (GUI.Button (posBtnGeneration, "Generation") && field != string.Empty) {
			GameCore.GetInstance().GenerateLabirint(int.Parse(field));		
		}

		if (GUI.Button (posBtnPath, "Path")) {
			GameCore.GetInstance().ShowWay();	
		}

		field = GUI.TextField(posInputField, field, 4);

	}
}
