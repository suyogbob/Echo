using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Manages the checkpoint system for player death.
 * Alerts the level manager when the player touches it
 * so the level manager can do the respawn.
 */

public class Checkpoint : MonoBehaviour {

	//the LevelManager instance
   	private LevelManager levelManager;


    void Start() {
		//find the level manager
        levelManager = FindObjectOfType<LevelManager>();
    }

	void Update () {
	}

	//detect player triggers
    void OnTriggerEnter2D(Collider2D other) {
      //if player goes through the checkpoint, update current Checkpoint
        if (other.name == "Player") {
			//update level manager
            levelManager.currentCheckpoint = gameObject;
        }
    }
}
