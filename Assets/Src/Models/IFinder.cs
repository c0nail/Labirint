using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IFinder {

	List<Vector3> Find(ILabirint labirint, IDraw drawLab);
}
