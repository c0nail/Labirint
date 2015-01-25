using UnityEngine;
using System.Collections;

public interface IDraw  {

	void Draw(ILabirint lab, ICreater creater);
	GameObject[,] GetLab();
}
