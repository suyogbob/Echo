using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ray : Echo
{


	public GameObject player;
	private Transform pointerTransform;
	private SpriteRenderer pointerRenderer;
	public Sprite pointer_sprite;
	public Sprite none;
	private float angle = 0;

	public override string getName()
	{
		return "Ray";
	}

	public override void init()
	{
		pointerRenderer = GameObject.Find("pointer").GetComponent<SpriteRenderer>();
		pointerTransform = GameObject.Find("pointer").GetComponent<Transform>();
		pointerTransform.position = new Vector3 (player.transform.position.x + 3, player.transform.position.y, 0);
	}

	public override float tick(bool onCd)
	{
		if (Input.GetKeyDown (KeyCode.A)) 
		{
			angle += Mathf.PI / 12;
			if (angle > Mathf.PI * 2)
				angle -= (Mathf.PI * 2);
			Vector3 v = pointerTransform.position;
			v.x = Mathf.Cos (angle) * 3 + player.transform.position.x;
			v.y = Mathf.Sin (angle) * 3 + player.transform.position.y;
			pointerTransform.position = v;
			pointerTransform.Rotate (new Vector3 (0, 0, 15f));
		}
		if (Input.GetKeyDown (KeyCode.D)) 
		{
			angle -= Mathf.PI / 12;
			if (angle < 0)
				angle += (Mathf.PI * 2);

			Vector3 v = pointerTransform.position;
			v.x = Mathf.Cos (angle) * 3 + player.transform.position.x;
			v.y = Mathf.Sin (angle) * 3 + player.transform.position.y;
			pointerTransform.position = v;
			pointerTransform.Rotate (new Vector3 (0, 0, -15f));

		}
		if (onCd)
			return 0;
		
		if (base.createBalls (angle - Mathf.PI / 24, angle + Mathf.PI / 24))
			return 4;
		else
			return 0;
	}

	public override void switchTo()
	{
		pointerRenderer.sprite = pointer_sprite;
	}

	public override void switchFrom()
	{
		base.switchFrom ();
		pointerRenderer.sprite = none;
	}

}
