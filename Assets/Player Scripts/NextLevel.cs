using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Manages the level transitions.
 * Detect collisions and do the unity-backend scene loading
 */
public class NextLevel : MonoBehaviour {

	/* CONFIGURATIONS*/
	//player reference
	public Rigidbody2D player;
	//name of level to load
	public string levelToLoad;

	/* INTERNAL */
	//collider of the level target game object
	private Collider2D diamondCollider;
	//light source
	private Light spot;

	// Use this for initialization
	void Start () {
		//setup referendes
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
            Inventory script = GameObject.Find("Player").GetComponent<Inventory>();
            script.saveInventory();
            SceneManager.LoadScene(levelToLoad);
        }
	}
}
