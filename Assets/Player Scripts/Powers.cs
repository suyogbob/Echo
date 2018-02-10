using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powers : MonoBehaviour {

	public GameObject player;
	private IPower[] powers;
	private int power;
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

        }
        else
        {
			powers [power].tick ();
        }
    }

}


