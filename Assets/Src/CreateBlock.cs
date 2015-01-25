using UnityEngine;
using System.Collections;

public class CreateBlock : MonoBehaviour, ICreater {

	public GameObject[] blocks;
	public int SizePool;
	Vector3 pos = new Vector3 (0, -200, 0);

	[@ContextMenu ("Generate")]
	void GenerateBlock()
	{
		ClearPool ();
		blocks = new GameObject[SizePool];
		for (int i = 0; i < SizePool; i++) {
			blocks[i] = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/BackLab"),pos, Quaternion.identity);		
			blocks[i].name = "name_"+i;
			blocks[i].transform.parent = this.transform;
		}
	}

	[@ContextMenu ("ClearPool")]
	void ClearPool()
	{
		for (int i = 0; i< blocks.Length; i++) {
			DestroyImmediate(blocks[i]);
		}
		blocks = null;
	}

	public GameObject[,] GetBlock(int size)
	{
		GameObject[,] mass = new GameObject[size, size];
		int b = 0;
		for (int i = 0; i < SizePool; i++) {
			if(blocks[i].transform.position != pos)
			{
				blocks[i].transform.position = pos;
			}
			else{
				break;
			}
		}

		for (int i = 0; i < size; i++) {
			for(int j = 0; j < size; j++)
			{
				mass[i,j] = blocks[b];
				b++;
			}
		}

		return mass;
	}
}
