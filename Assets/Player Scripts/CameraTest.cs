using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This class is a test behavior to control the camera size.
 * I was attempting to avoid the fact that the viewport resizes when
 * the game window resizes. I was mostly unsuccessful. -Alex
 */
public class CameraTest : MonoBehaviour {

	private Camera cam;

	public int width;
	public int height;

	void Start()
	{
		cam = Camera.main;
		//try setting the size fixed by pixel.
		cam.pixelRect = new Rect ((Screen.width - width) / 2, (Screen.height - height) / 2, width, height);
	}

	void Update()
	{ 
		//what follows is some code I took from the unity forums to mess with cameras.
		if (Input.GetKey("u"))
		{
			Debug.Log ("u");
			// choose the margin randomly
			float margin = Random.Range(0.0f, 0.3f);
			// setup the rectangle
			cam.rect = new Rect(margin, 0.0f, 1.0f - margin * 2.0f, 1.0f);
		}
	}
}
