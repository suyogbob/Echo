using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atium : MonoBehaviour, IPower {

	public GameObject player;
	//sprite for aiming pointer
	public Sprite shadow_sprite;
	//sprite for hiding pointer
	public Sprite none;

	private Transform shadowTransform;
	//pointer's renderer
	private SpriteRenderer shadowRenderer;

	private Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		
	}


	public virtual void init()
	{
		shadowRenderer = GameObject.Find("shadow").GetComponent<SpriteRenderer>();
		shadowRenderer.sprite = null;
		shadowTransform = GameObject.Find("shadow").GetComponent<Transform>();
		shadowTransform.position = new Vector3 (player.transform.position.x, player.transform.position.y, -1);
		rb2d = GameObject.Find("shadow").GetComponent<Rigidbody2D>();

	}

	public virtual float tick(bool onCd)
	{
		float moveHorizontal = Input.GetAxis("Horizontal");
		Vector2 movement = rb2d.velocity + moveHorizontal * 4 * Vector2.right;
		rb2d.velocity = movement;

		return 0;
	}

	//on switching to this power, show the shadow
	public virtual void switchTo()
	{		
		shadowTransform.position = new Vector3 (player.transform.position.x, player.transform.position.y, -1);
		shadowRenderer.sprite = shadow_sprite;
	}

	//hide the shadow
	public virtual void switchFrom()
	{
		shadowRenderer.sprite = none;		

	}

	public virtual string getName()
	{
		return "Atium";
	}

}
