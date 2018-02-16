using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTest : MonoBehaviour {

	private Camera cam;

	public int width;
	public int height;

	void Start()
	{
		cam = Camera.main;
		cam.pixelRect = new Rect ((Screen.width - width) / 2, (Screen.height - height) / 2, width, height);
	}

	void Update()
	{
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
