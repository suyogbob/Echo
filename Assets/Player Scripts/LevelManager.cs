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


	void Start () {
        //create reference
		player = GameObject.FindWithTag("Player");
		//make sure the movers list is defined if there are no movers
		if (movers == null)
			movers = new List<MovingPlatform> ();

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
}
