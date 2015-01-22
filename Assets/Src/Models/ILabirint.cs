using UnityEngine;
using System.Collections;

public interface ILabirint {

	void Generate(int Size);
	int[,] GetLab();
}
