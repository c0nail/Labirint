using UnityEngine;
using System.Collections;

public interface IDraw  {

	void Draw(ILabirint lab);
	GameObject[,] GetLab();
}
