using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Power manager for the
 * arc Ray power. Inherits
 * the power functionality
 * and the ball creation mechanic.
 * from its parent, Echo.
 */
public class Ray : Echo
{
	/* CONFIGURATIONS */
	//player reference
	public GameObject player;
	//sprite for aiming pointer
	public Sprite pointer_sprite;
	//sprite for hiding pointer
	public Sprite none;

	/*INTERNAL*/
	//pointer's position
	private Transform pointerTransform;
	//pointer's renderer
	private SpriteRenderer pointerRenderer;
	//aiming angle
	private float angle = 0;

	//power naem
	public override string getName()
	{
		return "Ray";
	}

	//initialize: find references, and put the pointer in the right place.
	public override void init()
	{
		pointerRenderer = GameObject.Find("pointer").GetComponent<SpriteRenderer>();
        pointerRenderer.sprite = null;
		pointerTransform = GameObject.Find("pointer").GetComponent<Transform>();
		pointerTransform.position = new Vector3 (player.transform.position.x + 3, player.transform.position.y, -1);
	}

	//ray tick override echo tick
	public override float tick(bool onCd)
	{
		//do aiming even if on cd

		//aim left
		if (Input.GetKeyDown (KeyCode.A)) 
		{
			//update angle, move pointer, rotate
			angle += Mathf.PI / 12;
			if (angle > Mathf.PI * 2)
				angle -= (Mathf.PI * 2);
			Vector3 v = pointerTransform.position;
			v.x = Mathf.Cos (angle) * 3 + player.transform.position.x;
			v.y = Mathf.Sin (angle) * 3 + player.transform.position.y;
			pointerTransform.position = v;
			pointerTransform.Rotate (new Vector3 (0, 0, 15f));
		}
		//aim right
		if (Input.GetKeyDown (KeyCode.D)) 
		{
			//update angle, move pointer, rotate
			angle -= Mathf.PI / 12;
			if (angle < 0)
				angle += (Mathf.PI * 2);

			Vector3 v = pointerTransform.position;
			v.x = Mathf.Cos (angle) * 3 + player.transform.position.x;
			v.y = Mathf.Sin (angle) * 3 + player.transform.position.y;
			pointerTransform.position = v;
			pointerTransform.Rotate (new Vector3 (0, 0, -15f));

		}
		//handle cooldown state
		if (onCd)
			return 0;

		//use the ball creation method to span a pi/12 arc on either side of the pointer
		if (base.createBalls (angle - Mathf.PI / 24, angle + Mathf.PI / 24))
			return 4; //cooldown only on success
		else
			return 0;
	}

	//on switching to this power, show the pointer
	public override void switchTo()
	{
		pointerRenderer.sprite = pointer_sprite;
	}

	//on switching from this power, use the parent's method (remove all balls)
	//then hide the pointer
	public override void switchFrom()
	{
		base.switchFrom ();
		pointerRenderer.sprite = none;
	}

}
