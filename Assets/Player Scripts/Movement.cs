using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log ("Starting Ground Sensor");
	}
	
	// Update is called once per frame
	void Update () {
	}
	void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.tag == "Platforms") {
			Debug.Log ("Touched Platform");
		}
	}

}
