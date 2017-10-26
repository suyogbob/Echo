using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleBlocks : MonoBehaviour {

	public GameObject[] invis;
	public Light playerLight;
	private float timeSinceUse;
	private SpriteRenderer[] ren;
	public Sprite rusty;
	public Sprite none;
	// Use this for initialization
	void Start () {
		playerLight = GameObject.Find("Point light").GetComponent<Light> ();
		timeSinceUse = 10f;
		invis = GameObject.FindGameObjectsWithTag ("Invisible");
		ren = new SpriteRenderer[invis.Length];
		for (int i = 0; i < invis.Length; i++) {
			ren [i] = invis [i].GetComponent<SpriteRenderer> ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (timeSinceUse > 1f && playerLight.intensity == 5f) {
			playerLight.intensity = 0.4f;
			for (int i = 0; i < invis.Length; i++) {
				ren [i].sprite = none;
			}
		}
	}

	void FixedUpdate () {
		if (Input.GetKey (KeyCode.F)) {
			LightFlash ();
		}
		timeSinceUse += Time.deltaTime;
	}

	void LightFlash () {
		playerLight.intensity = 5f;
		for (int i = 0; i < invis.Length; i++) {
			ren [i].sprite = rusty;
		}
		timeSinceUse = 0f;
	}
			
}
