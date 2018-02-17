using UnityEngine;
using System.Collections;

/*
 * Manages all movement capabilities
 * (running, jumping, downward push).
 * Also updates the player on moving platforms.
 * Counts as a power because of reasons.
 */
public class Movement : MonoBehaviour, IPower 
{
	//player's location
	private Transform transform;
	//player's rigidbody
	private Rigidbody2D rb2d;
	//current movement speed
	private float speed;
	//layer number for platforms
	private int platformsIndex;
	//layer number for the floor
	private int defaultIndex;
	//the platform the player is currently standing on (if any)
	private GameObject platform;

	/* CONFIGURATIONS */

	//base running speed
	public float speedInitial;
	//jump force
	public float jumpStrength;

	//power name
	public string getName()
	{
		return "Movement";
	}

	//setup references and indices
	public void init()
	{
		transform = GetComponent<Transform> ();
		rb2d = GetComponent<Rigidbody2D>();
		speed = speedInitial;
		platformsIndex = LayerMask.NameToLayer("Platforms");
		defaultIndex = LayerMask.NameToLayer ("Default");
		platform = null;
	}

	//do power tick (only if movement is currently seleted power)
	public float tick(bool onCd)
	{
		//double motion on shift press
		if (Input.GetKey(KeyCode.LeftShift))
		{
			speed = 2 * speedInitial;
		}
		else //otherwise normal
		{
			speed = speedInitial;
		}

		//add the moving velocity to the existing velocity.
		//Note: this is because the velocity is reset every frame.
		float moveHorizontal = Input.GetAxis("Horizontal");
		Vector2 movement = rb2d.velocity + moveHorizontal * speed * Vector2.right; 
		rb2d.velocity = movement;
		//on S press, do downwards push
		if (Input.GetKey(KeyCode.S))
		{
			//push
			Vector2 move = new Vector2(0, -3) * speed;
			rb2d.AddForce(move);
		}
		//on space press, do jump. only if standing on a platform
		if (Input.GetKeyDown(KeyCode.Space) && (platform != null))
		{
			//jump
			Vector2 move = new Vector2(0, 1) * jumpStrength;
			rb2d.AddForce(move);
		}

		//movement has no cooldown
		return 0;
	}

	//update on every frame, regardless of power status
	//handles moving platforms and ground detection
	void Update()
	{
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
		rb2d.velocity = new Vector2(platSpeed, rb2d.velocity.y);

	}

	//power selection callback
	public void switchTo()
	{
	}

	//power deselection: default to platform's speed (x only) if there is one.
	public void switchFrom()
	{
		float platSpeed = 0;

		if (platform != null) {
			Rigidbody2D platRB2D = platform.GetComponent<Rigidbody2D> ();
			if (platRB2D != null) {
				platSpeed = platRB2D.velocity.x;
			}

		}
		rb2d.velocity = new Vector2(platSpeed, rb2d.velocity.y);
	}

}
