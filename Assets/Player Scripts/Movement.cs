using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour, IPower 
{
	private Rigidbody2D rb2d;
	private CircleCollider2D c2d;
	private Collider2D groundSensor;
	public float speedInitial;//8
	public float jumpStrength;//150
	private bool isGrounded;
	private float speed;
	private int platformsIndex;
	private GameObject platform;

	public string getName()
	{
		return "Movement";
	}

	public void init()
	{
		rb2d = GetComponent<Rigidbody2D>();
		c2d = GetComponent<CircleCollider2D>();
		groundSensor = GameObject.Find("Ground_Sensor").GetComponent<Collider2D>();
		speed = speedInitial;
		isGrounded = true;
		platformsIndex = LayerMask.NameToLayer("Platforms");
	}

	public void tick()
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
		Vector2 movement = new Vector2(moveHorizontal * speed, rb2d.velocity.y);
		rb2d.velocity = (movement);
		//rb2d.velocity = movement * speed;
		if (Input.GetKey(KeyCode.S))
		{
			Vector2 move = new Vector2(0, -3) * speed;
			rb2d.AddForce(move);
		}
		if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
		{
			Vector2 move = new Vector2(0, 1) * jumpStrength;
			rb2d.AddForce(move);
			isGrounded = false;
		}
	}

	public void switchTo()
	{
	}

	public void switchFrom()
	{
		Vector2 movement = new Vector2(0, rb2d.velocity.y);
		rb2d.velocity = (movement);
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		platform = col.gameObject;
		if (platform.layer == platformsIndex)
		{
			isGrounded = true;
		}
	}
		
	void OnTriggerExit2D(Collider2D col)
	{
		platform = col.gameObject;
		if (platform.layer == platformsIndex)
		{
			isGrounded = false;
		}
	}

}
