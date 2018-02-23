using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script is modular and can be loaded onto any component.
 * It will cause that component to count as deadly and
 * kill the player if the player touches it.
 * This script manages both collisions (for objects)
 * and triggers (for trigger colliders). The other
 * will be redundant. Use the properties of the
 * object itself to control which it behaves as.
 */
public class PlayerDeath : MonoBehaviour {

	//level manager reference
    private LevelManager levelManager;
    //player reference
    private GameObject player;

	void Start () {
        levelManager = FindObjectOfType<LevelManager>();
        player = GameObject.FindWithTag("Player");
	}

	void Update () {

	}

	//respond to triggers
    void OnTriggerEnter2D(Collider2D other) {
      // if player falls through, trigger respawn
        if(other.tag == player.tag) {
            levelManager.RespawnPlayer();
        }
    }

}
