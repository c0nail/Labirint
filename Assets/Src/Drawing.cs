using System;
using UnityEngine;

public class Drawing
{
	//****************************************************************************************************
	//  static function DrawLine(rect : Rect) : void
	//  static function DrawLine(rect : Rect, color : Color) : void
	//  static function DrawLine(rect : Rect, width : float) : void
	//  static function DrawLine(rect : Rect, color : Color, width : float) : void
	//  static function DrawLine(Vector2 pointA, Vector2 pointB) : void
	//  static function DrawLine(Vector2 pointA, Vector2 pointB, color : Color) : void
	//  static function DrawLine(Vector2 pointA, Vector2 pointB, width : float) : void
	//  static function DrawLine(Vector2 pointA, Vector2 pointB, color : Color, width : float) : void
	//  
	//  Draws a GUI line on the screen.
	//  
	//  DrawLine makes up for the severe lack of 2D line rendering in the Unity runtime GUI system.
	//  This function works by drawing a 1x1 texture filled with a color, which is then scaled
	//   and rotated by altering the GUI matrix.  The matrix is restored afterwards.
	//****************************************************************************************************
	
	public static Texture2D lineTex;
	
	public static void DrawLine(Rect rect) { DrawLine(rect, GUI.contentColor, 1.0f); }
	public static void DrawLine(Rect rect, Color color) { DrawLine(rect, color, 1.0f); }
	public static void DrawLine(Rect rect, float width) { DrawLine(rect, GUI.contentColor, width); }
	public static void DrawLine(Rect rect, Color color, float width) { DrawLine(new Vector2(rect.x, rect.y), new Vector2(rect.x + rect.width, rect.y + rect.height), color, width); }
	public static void DrawLine(Vector2 pointA, Vector2 pointB) { DrawLine(pointA, pointB, GUI.contentColor, 1.0f); }
	public static void DrawLine(Vector2 pointA, Vector2 pointB, Color color) { DrawLine(pointA, pointB, color, 1.0f); }
	public static void DrawLine(Vector2 pointA, Vector2 pointB, float width) { DrawLine(pointA, pointB, GUI.contentColor, width); }
	public static void DrawLine(Vector2 pointA, Vector2 pointB, Color color, float width)
	{
		// Save the current GUI matrix, since we're going to make changes to it.
		pointA.x = (int)pointA.x;    pointA.y = (int)pointA.y;
		pointB.x = (int)pointB.x;    pointB.y = (int)pointB.y;
		
		if (!lineTex) { lineTex = new Texture2D(1, 1); }
		Color savedColor = GUI.color;
		GUI.color = color;
		
		Matrix4x4 matrixBackup = GUI.matrix;
		
		float angle = Mathf.Atan2(pointB.y-pointA.y, pointB.x-pointA.x)*180f/Mathf.PI;
		float length = (pointA-pointB).magnitude;
		GUIUtility.RotateAroundPivot(angle, pointA);
		GUI.DrawTexture(new Rect(pointA.x, pointA.y, length, width), lineTex);
		
		GUI.matrix = matrixBackup;
		GUI.color = savedColor;
	}
}