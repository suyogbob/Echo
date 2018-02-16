using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public GameObject currentCheckpoint;
    private GameObject player;
	private List<MovingPlatform> movers;

	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player");

	}

	public void registerMover(MovingPlatform m)
	{
		if (movers == null)
			movers = new List<MovingPlatform> ();
		movers.Add (m);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RespawnPlayer() {
        Debug.Log("Player Respawn");
        player.transform.position = currentCheckpoint.transform.position;
		foreach (MovingPlatform m in movers) {
			m.reset ();
		}
			
    }
}
