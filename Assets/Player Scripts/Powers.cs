using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powers : MonoBehaviour {

	public GameObject player;
	private IPower[] powers;
	private int power;
	private float cooldown;
	//GENERAL POWER SCRIPTS
	void Start()
	{
		powers = new IPower[4];
		powers [0] = player.GetComponent<Movement> ();

		powers [1] = player.GetComponent<Echo> ();
		powers [2] = player.GetComponent<Ray> ();
		powers [3] = player.GetComponent<Flashlight> ();

		power = 0;
		for (int i = 0; i < powers.Length; i++) 
		{
			powers [i].init ();
		}

		cooldown = 0;

	}

    void OnGUI()
    {
        int offset = 0;
        GUIStyle activeButton = new GUIStyle(GUI.skin.box);
        activeButton.normal.textColor = Color.red;
        activeButton.fontStyle = FontStyle.Bold;
		for(int i = 0; i < powers.Length; i++)
        {
			
			int x = Screen.width / 2 - 100 * powers.Length / 2;
            Rect r = new Rect(x + offset * 100, Screen.height - 30, 100, 20);
			if (i == power)
            {
				GUI.Box(r, powers[i].getName(), activeButton);
            }
            else
            {
				GUI.Box(r, powers[i].getName());
            }
            offset++;
        }
    }
    void Update() {
		if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E) || (Input.GetKeyDown(KeyCode.LeftShift) && (power != 0)))
        {
			powers[power].switchFrom();

			if (Input.GetKeyDown (KeyCode.Q))
			{
				power--;
				if (power < 0)
					power = powers.Length - 1;
			}
            else if (Input.GetKeyDown(KeyCode.E))
				power = (power + 1) % powers.Length;
            else if (Input.GetKeyDown(KeyCode.LeftShift))
				power = 0;

			powers [power].switchTo ();

			cooldown = 0;

        }
        else
        {
			cooldown += powers [power].tick (cooldown > 0);
			if (cooldown > 0)
				cooldown -= Time.deltaTime;
			if (cooldown < 0)
				cooldown = 0;
        }

		// Activate powers by pushing number keys, so player can go out of order. Assumes Movement is 1, Echo is 2, etc.

		// Loop for all ints between 1 and the number of powers
		for (int i = 1; i <= powers.Length; i++) 
		{
			// If a number between 1 and the number of powers is pushed on the keyboard... 
			if (Input.GetKeyDown (i.ToString ())) {
				// Switch from last power
				powers [power].switchFrom ();

				// Update current power to be the power at the selected number
				power = i - 1;

				// Switch to next power
				powers [power].switchTo ();

				// And reset cooldown
				cooldown = 0;
			} 
				
		}
    }

}


