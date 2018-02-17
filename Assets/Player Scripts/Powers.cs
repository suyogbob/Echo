using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Manages the power system itself,
 * including GUI. References
 * the other power objects.
 */
public class Powers : MonoBehaviour {

	/* CONFIGURATIONS */
	//player reference
	public GameObject player;

	/* INTERNAL VARIABLES */
	//array of power objects
	private IPower[] powers;
	//pointer to current power
	private int currentPower;
	//current accumulated cooldown
	private float cooldown;

	void Start()
	{
		//Power are defined here rather than in the Inspector (Configurations)
		//because we haven't figured out how to put purely abstract C# objects
		//(i.e. not gameObjects) in the inspector

		//base definition
		powers = new IPower[4];
		powers [0] = player.GetComponent<Movement> ();

		//power
		powers [1] = player.GetComponent<Echo> ();
		powers [2] = player.GetComponent<Ray> ();
		powers [3] = player.GetComponent<Flashlight> ();

		//init powers
		currentPower = 0;
		for (int i = 0; i < powers.Length; i++) 
		{
			powers [i].init ();
		}

		cooldown = 0;

	}

	//draw the power gui
    void OnGUI()
    {
		//setup gui style for current power
		int offset = 0;
        GUIStyle activeButton = new GUIStyle(GUI.skin.box);
        activeButton.normal.textColor = Color.red;
        activeButton.fontStyle = FontStyle.Bold;
		//handle powers indicators
		for(int i = 0; i < powers.Length; i++)
        {
			//bounding rectangle
			int x = Screen.width / 2 - 100 * powers.Length / 2;
            Rect r = new Rect(x + offset * 100, Screen.height - 30, 100, 20);
			//draw active power
			if (i == currentPower)
            {
				GUI.Box(r, powers[i].getName(), activeButton);
            }
            else //draw other powers
            {
				GUI.Box(r, powers[i].getName());
            }

            offset++;
        }
    }

	//hanlde swapping
    void Update() {
		//movement is always power 0. only switch to movement if not already there
		if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E) || (Input.GetKeyDown(KeyCode.LeftShift) && (currentPower != 0)))
        {
			//deselect callback
			powers[currentPower].switchFrom();

			//switch left
			if (Input.GetKeyDown (KeyCode.Q))
			{
				currentPower--;
				if (currentPower < 0)
					currentPower = powers.Length - 1;
			}
			//switch right
            else if (Input.GetKeyDown(KeyCode.E))
				currentPower = (currentPower + 1) % powers.Length;
			
			else if (Input.GetKeyDown(KeyCode.LeftShift)) //leftshift - go to movement (0)
				currentPower = 0;

			//select callback
			powers [currentPower].switchTo ();

			cooldown = 0;

        }
        else
        {
			//tick the power, and tell it if it is on cooldown.
			//update the cooldowns
			cooldown += powers [currentPower].tick (cooldown > 0); 
			//tick the cooldown
			if (cooldown > 0)
				cooldown -= Time.deltaTime;
			if (cooldown < 0) //avoid negative cooldown
				cooldown = 0;
        }
    }

}


