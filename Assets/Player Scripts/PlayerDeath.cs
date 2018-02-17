using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour {

    public LevelManager levelManager;

	// Use this for initialization
	void Start () {
        levelManager = FindObjectOfType<LevelManager>();
	}

	// Update is called once per frame
	void Update () {

	}

    void OnTriggerEnter2D(Collider2D other) {
      // if player falls through, trigger respawn
        if(other.name == "Player") {
            levelManager.RespawnPlayer();
        }
    }

	void OnCollisionEnter2D(Collision2D collision)
	{
    // testing
		if (collision.gameObject.name == "Player") {
			levelManager.RespawnPlayer ();
		}
	}
}
