using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour {

	private GameObject player;
	private Transform playerPosition;
	private Transform cameraPosition;
    public int speed = 2;
	public float cameraWidth;
	public float cameraHeight;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player");
		playerPosition = player.transform;
		cameraPosition = this.transform;
	}

	// Update is called once per frame
	void Update () {
		MoveDirection();
	}

	void MoveDirection() {
		// move camera to the right , elseif - move camera to the left
        if (playerPosition.position.x > cameraPosition.position.x + cameraWidth / 2 + 1.5f) {
			cameraPosition.position = new Vector3(cameraPosition.position.x + cameraWidth + 1.5f,
			cameraPosition.position.y, cameraPosition.position.z);
			player.GetComponent<LevelManager>().x_position += 1;
		} else if (playerPosition.position.x < cameraPosition.position.x - cameraWidth / 2 - 1.5f) {
			cameraPosition.position = new Vector3(cameraPosition.position.x - cameraWidth - 1.5f,
			cameraPosition.position.y, cameraPosition.position.z);
			player.GetComponent<LevelManager>().x_position -= 1;
		}

		// move camera up , elseif - move camera down
		if (playerPosition.position.y > cameraPosition.position.y + cameraHeight / 2 + 1.5f) {
			cameraPosition.position = new Vector3(cameraPosition.position.x,
				cameraPosition.position.y + cameraHeight + 1.5f, cameraPosition.position.z);
			player.GetComponent<LevelManager>().y_position += 1;
		} else if (playerPosition.position.y < cameraPosition.position.y - cameraHeight / 2 - 1.5f) {
			cameraPosition.position = new Vector3(cameraPosition.position.x,
				cameraPosition.position.y - cameraHeight - 1.5f, cameraPosition.position.z);
			player.GetComponent<LevelManager>().y_position -= 1;
		}


	}
}
