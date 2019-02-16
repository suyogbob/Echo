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

	private GameObject platform;
	//layer number for platforms
	private int platformsIndex;
	//layer number for the floor
	private int defaultIndex;


	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {

		//linecast straight down to try to hit a platform
		RaycastHit2D hit = Physics2D.Linecast (transform.position, transform.position + (0.5f*Vector3.down), (1 << platformsIndex) | (1 << defaultIndex));
		//save platform if there was one.
		platform = (hit.transform == null) ? null : hit.transform.gameObject;
		//detect the platform's speed (if it is moving, 0 otherwise, or if none exists)
		float platSpeed = 0;
		if (platform != null) {
			Rigidbody2D platRB2D = platform.GetComponent<Rigidbody2D> ();
			if (platRB2D != null) {
				platSpeed = platRB2D.velocity.x;
			}

		}
		//velocity is RESET here to the platforms speed (x dir only)
		//the player motion is ADDED on top of this
		if(rb2d != null)
			rb2d.velocity = new Vector2(platSpeed, rb2d.velocity.y);
		
	}


	public virtual void init()
	{
		shadowRenderer = GameObject.Find("shadow").GetComponent<SpriteRenderer>();
		shadowRenderer.sprite = null;
		shadowTransform = GameObject.Find("shadow").GetComponent<Transform>();
		shadowTransform.position = new Vector3 (player.transform.position.x, player.transform.position.y, -1);
		rb2d = GameObject.Find("shadow").GetComponent<Rigidbody2D>();
		platformsIndex = LayerMask.NameToLayer("Platforms");
		defaultIndex = LayerMask.NameToLayer ("Default");

	}

	public virtual float tick(bool onCd)
	{

		//add the moving velocity to the existing velocity.
		//Note: this is because the velocity is reset every frame.
		float moveHorizontal = Input.GetAxis("Horizontal");
		Vector2 movement = rb2d.velocity + moveHorizontal * 8 * Vector2.right;
		rb2d.velocity = movement;
		//on space press, do jump. only if standing on a platform
		if ( Input.GetKeyDown(KeyCode.Space))
		{
			shadowTransform.position = new Vector3 (player.transform.position.x, player.transform.position.y, -1);

		}

		//movement has no cooldown
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
