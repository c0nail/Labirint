using UnityEngine;
using System.Collections;

public class PartLab : MonoBehaviour {




	public SpriteRenderer leftWall;
	public SpriteRenderer rightWall;
	public SpriteRenderer topWall;
	public SpriteRenderer botWall;

	public int X;
	public int Y;

	public void SetWall(int x)
	{
		this.GetComponent<SpriteRenderer> ().color = Color.white;
		leftWall.enabled = false;
		rightWall.enabled = false;
		topWall.enabled = false;
		botWall.enabled = false;

		if (x == 15) {
			leftWall.enabled = true;
			rightWall.enabled = true;
			topWall.enabled = true;
			botWall.enabled = true;
		}

		if (x == 1) {
			topWall.enabled = true;
		}

		if (x == 2) {
			rightWall.enabled = true;
		}

		if (x == 3) {
			rightWall.enabled = true;
			topWall.enabled = true;
		}

		if (x == 4) {
			botWall.enabled = true;
		}

		if (x == 5) {
			botWall.enabled = true;
			topWall.enabled = true;
		}

		if (x == 6) {
			rightWall.enabled = true;
			botWall.enabled = true;
		}

		if (x == 7) {
			topWall.enabled = true;
			rightWall.enabled = true;
			botWall.enabled = true;
		}

		if (x == 8) {
			leftWall.enabled = true;
		}

		if (x == 9) {
			leftWall.enabled = true;
			topWall.enabled = true;
		}

		if (x == 10) {
			leftWall.enabled = true;
			rightWall.enabled = true;
		}

		if (x == 11) {
			leftWall.enabled = true;
			rightWall.enabled = true;
			topWall.enabled = true;
		}

		if (x == 12) {
			botWall.enabled = true;
			leftWall.enabled = true;
		}

		if (x == 13) {
			botWall.enabled = true;
			leftWall.enabled = true;
			topWall.enabled = true;
		}

		if (x == 14) {
			botWall.enabled = true;
			leftWall.enabled = true;
			rightWall.enabled = true;
		}

	}


}
