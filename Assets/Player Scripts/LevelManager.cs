using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * The Level manager is an abstraction
 * that manages level-wide behaviors,
 * such as respawning.
 * There is only ever one, so it can be found
 * with FindObjectOfType.
 */
public class LevelManager : MonoBehaviour {

	//which (if any) is the currently activated checkpoint.
	public GameObject currentCheckpoint;

	//player reference
	private GameObject player;
	//list of all moving platforms (so they can reset on player death)
	private List<MovingPlatform> movers;
	public int x_position = 0;
	public int y_position = 0;

	void Start () {
        //create reference
		player = GameObject.FindWithTag("Player");
		// make platforms invisibile
		makeInvis();
		//make sure the movers list is defined if there are no movers
		if (movers == null)
			movers = new List<MovingPlatform> ();

	}

	void setupMapDict () {
		Dictionary<string, int> xcoords = new Dictionary<string, int>();
		Dictionary<string, int> ycoords = new Dictionary<string, int>();

        xcoords.Add("00", 0);
        xcoords.Add("11", 1);
        xcoords.Add("20", 2);
        xcoords.Add("10", 1);
        xcoords.Add("2-1", 2);
        xcoords.Add("1-1", 1);
        xcoords.Add("3-1", 3);

        ycoords.Add("00", 0);
        ycoords.Add("11", 1);
        ycoords.Add("20", 0);
        ycoords.Add("10", 0);
        ycoords.Add("2-1", -1);
        ycoords.Add("1-1", -1);
        ycoords.Add("3-1", -1);
	}

	//public-facing method to add a moving platform to the list
	public void registerMover(MovingPlatform m)
	{
		//nullcheck in case this occurs before Start() is called
		if (movers == null)
			movers = new List<MovingPlatform> ();
		//add to list
		movers.Add (m);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.M))
		{
			// show coords text
			Debug.Log(x_position + "," + y_position);
		}

	}

	//public-facing method to trigger respawn
    public void RespawnPlayer() {
		//move player
		player.transform.position = currentCheckpoint.transform.position;
		//reset platforms
		foreach (MovingPlatform m in movers) {
			m.reset ();
		}

    }

		//method to make invisibile platforms invisibile
		private void makeInvis() {
			GameObject[] invis = GameObject.FindGameObjectsWithTag("Invisible");
			foreach(GameObject g in invis) {
				g.GetComponent<SpriteRenderer>().sprite = null;
				g.GetComponent<SpriteRenderer>().color = Color.white;
			}
		}
}
