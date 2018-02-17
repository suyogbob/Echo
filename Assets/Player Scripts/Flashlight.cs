using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * IPower instance for the flashlight power.
 * Show the light when the power is on and
 * change the sprite of all invis platforms within range
 * ("within range" is a little weird and needs changing)
 */
public class Flashlight : MonoBehaviour, IPower 
{
	/* CONFIGURATIONS */

	//sprite to show for illuminated platforms
	public Sprite rusty;
	//sprite to put back for platforms when the light turns off
	public Sprite none;

	/* INTERNAL VARIABLES */

	//reference to the player
	private GameObject player;
	//reference to the light object
	private Light playerLight;
	//list of all invisible platforms
	private GameObject[] invis;
	//light display time (this is an outdated precursor to the cooldown system and needs to be refactored)
	private float timeSinceUse;
	//sprite renderers for all the platforms
	private SpriteRenderer[] ren;

	//power name
	public string getName()
	{
		return "Flashlight";
	}

	//setup references
	public void init()
	{
		playerLight = GameObject.Find("Point light").GetComponent<Light>();
		player = GameObject.Find("Player");
		timeSinceUse = 10f;

		//setup sprite renderers
		invis = GameObject.FindGameObjectsWithTag("Invisible");
		ren = new SpriteRenderer[invis.Length];
		for (int i = 0; i < invis.Length; i++)
		{
			ren[i] = invis[i].GetComponent<SpriteRenderer>();
		}
	}

	//process 1 frame
	public float tick(bool onCd)
	{
		for (int i = 0; i < invis.Length; i++)
		{
			//if platform is in range and the light is on (5f) show it
			if (timeSinceUse < 1f && playerLight.intensity == 5f && Vector3.Distance(invis[i].transform.position, player.transform.position) < 5)
				ren[i].sprite = rusty;
			//otherwise hide it
			else
				ren[i].sprite = none;
		}

		//if the light has been on without a press, turn it off (0.0f)
		if (timeSinceUse > 1f && playerLight.intensity == 5f)
		{
			playerLight.intensity = 0.0f;
		}

		//handle key presses
		if (Input.GetKey(KeyCode.F))
		{
			playerLight.intensity = 5f;

			//reset the use timer
			timeSinceUse = 0f;
		}

		//update time
		timeSinceUse += Time.deltaTime;
		return 0;
	}

	//power selection
	public void switchTo()
	{
	}

	//when power is deselected, hide all the sprites and turn the light off
	public void switchFrom()
	{
		playerLight.intensity = 0.0f;
		for (int i = 0; i < invis.Length; i++)
		{
			ren[i].sprite = none;
		}
	}
}
