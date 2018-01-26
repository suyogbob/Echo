using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour {


	public Rigidbody2D player;
	public string levelToLoad;
	private Collider2D diamondCollider;
	private Light spot;

	// Use this for initialization
	void Start () {
		diamondCollider = player.GetComponent<Collider2D> ();
		spot = GameObject.Find("Spotlight").GetComponent<Light> ();
		spot.transform.localPosition = new Vector2 (0f, 1.5f);
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D(Collider2D diamondCollider) {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (diamondCollider.gameObject.name == "Player")
        {
            //If the GameObject's name matches the one you suggest, output this message in the console
            SceneManager.LoadScene(levelToLoad);
        }
	}
	void Update () {

	}
}
