using UnityEngine;
using System.Collections;

public class LabirintFactory {

	public ILabirint buildLab() {
		return new MazeMatrix();
	}
	
	public IFinder buildFind(){
		return new Finder();
	}

	public IDraw buildDraw(){
		return new DrawLabirint();
	}

}
