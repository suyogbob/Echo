using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    public LevelManager levelManager;

    // Use this for initialization
    void Start() {
        levelManager = FindObjectOfType<LevelManager>();
    }

	// Update is called once per frame
	void Update () {

	}

    void OnTriggerEnter2D(Collider2D other) {
      //if player goes through the checkpoint, update current Checkpoint
        if (other.name == "Player") {
            levelManager.currentCheckpoint = gameObject;
        }
    }
}
