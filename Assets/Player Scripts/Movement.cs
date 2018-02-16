using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour, IPower 
{
	private Transform transform;
	private Rigidbody2D rb2d;
	public float speedInitial;//8
	public float jumpStrength;//150
	private float speed;
	private int platformsIndex;
	private int defaultIndex;
	private GameObject platform;

	public string getName()
	{
		return "Movement";
	}

	public void init()
	{
		transform = GetComponent<Transform> ();
		rb2d = GetComponent<Rigidbody2D>();
		speed = speedInitial;
		platformsIndex = LayerMask.NameToLayer("Platforms");
		defaultIndex = LayerMask.NameToLayer ("Default");
		platform = null;
	}

	public float tick(bool onCd)
	{
		if (Input.GetKey(KeyCode.LeftShift))
		{
			speed = 2 * speedInitial;
		}
		else
		{
			speed = speedInitial;
		}


		float moveHorizontal = Input.GetAxis("Horizontal");
		Vector2 movement = rb2d.velocity + moveHorizontal * speed * Vector2.right; 
		rb2d.velocity = movement;
		//rb2d.velocity = movement * speed;
		if (Input.GetKey(KeyCode.S))
		{
			Vector2 move = new Vector2(0, -3) * speed;
			rb2d.AddForce(move);
		}
		if (Input.GetKeyDown(KeyCode.Space) && (platform != null))
		{
			Vector2 move = new Vector2(0, 1) * jumpStrength;
			rb2d.AddForce(move);
		}


		return 0;
	}

	void Update()
	{
		RaycastHit2D hit = Physics2D.Linecast (transform.position, transform.position + (0.5f*Vector3.down), (1 << platformsIndex) | (1 << defaultIndex));
		platform = (hit.transform == null) ? null : hit.transform.gameObject;
		float platSpeed = 0;
		if (platform != null) {
			Rigidbody2D platRB2D = platform.GetComponent<Rigidbody2D> ();
			if (platRB2D != null) {
				platSpeed = platRB2D.velocity.x;
			}

		}
		rb2d.velocity = new Vector2(platSpeed, rb2d.velocity.y);

	}

	public void switchTo()
	{
	}

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
