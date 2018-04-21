using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMovement : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
	    if (PlayerPrefs.GetInt("position_save") == 1)
        {
            PlayerPrefs.DeleteKey("position_save");
            transform.position = new Vector2(PlayerPrefs.GetInt("x"), PlayerPrefs.GetInt("y"));
        }

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
