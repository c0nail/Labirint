using UnityEngine;
using System.Collections;

public class DrawLabirint : IDraw {

	
	GameObject[,] Cells = null;
	int sizeFieldX;
	int sizeFieldY;
	float centerX;
	float centerY;
	
	Vector3 centerField = Vector3.zero;

	float scale = 1;
	float distance = 0.95f;
	float maxElement = 10;

	string pathToPrefabFruit = "Prefabs/BackLab";

	public GameObject[,] GetLab()
	{
		return Cells;
	}

	public void Draw(ILabirint labirint)
	{
		if (Cells != null) {
			for (int i =0; i < sizeFieldX; i++) {
				for (int j =0; j < sizeFieldY; j++) {
					GameObject.Destroy(Cells[i,j]);
				}
			}
		}

		int[,] lab = labirint.GetLab ();
		int maxSize = (int)Mathf.Sqrt (lab.Length);

		if (maxSize > maxElement) {
			distance = distance * (maxElement/maxSize);	
			scale = maxElement/maxSize * 0.4f;

		}

		sizeFieldX = (int)Mathf.Sqrt (lab.Length);
		sizeFieldY = (int)Mathf.Sqrt (lab.Length);

		Cells = new GameObject[sizeFieldX, sizeFieldY];
		
		if (sizeFieldX % 2 == 0) {
			centerX = sizeFieldX/2 + 0.5f;	
		}
		else{
			centerX = sizeFieldX/2 + 1;	
		}
		
		if (sizeFieldY % 2 == 0) {
			centerY = sizeFieldY/2 + 0.5f;	
		}
		else{
			centerY = sizeFieldY/2 + 1;	
		}

		for (int i =1; i <= sizeFieldX; i++) {
			for (int j =1; j <= sizeFieldY; j++) {
				Vector3 positonFruit = centerField;
				if(i < centerX)
				{
					positonFruit.x -= (centerX - i)* distance;
				}
				else if(i > centerX){
					positonFruit.x += (i - centerX)* distance;
				}
				
				if(j < centerY)
				{
					positonFruit.y += (centerY - j)* distance;
				}
				else if(j > centerY)
				{
					positonFruit.y -= (j - centerY)* distance;
				}


				Cells[i-1,j-1] = (GameObject)GameObject.Instantiate(Resources.Load(pathToPrefabFruit),positonFruit, Quaternion.identity);
				Cells[i-1,j-1].name = (i-1) +"x"+ (j-1);
				Cells[i-1,j-1].GetComponent<PartLab>().SetWall(lab[i-1,j-1]);
				Cells[i-1,j-1].GetComponent<PartLab>().X = i-1;
				Cells[i-1,j-1].GetComponent<PartLab>().Y = j-1;
				if (maxSize > maxElement) {
					Cells[i-1,j-1].transform.localScale = new Vector3(scale,scale,1);
				}
			}	
		}
	}
}
