using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
	private Rigidbody2D rb2d;
	private CircleCollider2D c2d;
	private Collider2D groundSensor;
	public float speedInitial;
	public float jumpStrength;
	public bool isGrounded;
	private float speed;
	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		c2d = GetComponent<CircleCollider2D> ();
		groundSensor = GameObject.Find("Ground_Sensor").GetComponent<Collider2D>();
		speed = speedInitial;
		isGrounded = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.LeftShift)) {
			speed = 2 * speedInitial;
		} else {
			speed = speedInitial;
		}
		float moveHorizontal = Input.GetAxis ("Horizontal");

		Vector2 movement = new Vector2 (moveHorizontal*speed, rb2d.velocity.y);
		rb2d.velocity = (movement);
		//rb2d.velocity = movement * speed;
		if (Input.GetKey (KeyCode.S)) {
			Vector2 move = new Vector2 (0, -3)*speed;
			rb2d.AddForce (move);
		}
		if (Input.GetKeyDown (KeyCode.Space) && isGrounded) {
			Vector2 move = new Vector2 (0, 1)*jumpStrength;
			rb2d.AddForce (move);
			isGrounded = false;
		}

	}

	void OnTriggerEnter2D(Collider2D col) {
		/*if (col.bounds.center.x + col.bounds.extents.x > rb2d.position.x &&
		   col.bounds.center.x - col.bounds.extents.x < rb2d.position.x) {
			isGrounded = true;
		}*/
	}
	void OnTriggerStay2D(Collider2D col) {
        Rigidbody2D box = col.attachedRigidbody;
		//Debug.Log ("platform right edge: " + (col.bounds.center.x + col.bounds.extents.x));
		//Debug.Log ("player left edge: " + (c2d.bounds.center.x - c2d.bounds.extents.x));
        //Debug.Log("player bottom edge: " + (c2d.bounds.center.y - c2d.bounds.extents.y));
        //Debug.Log("platform top edge: " + (col.bounds.center.y + col.bounds.extents.y));
		//Debug.Log ("\n");
        if (col.bounds.center.x + col.bounds.extents.x > c2d.bounds.center.x - c2d.bounds.extents.x &&
            col.bounds.center.x - col.bounds.extents.x < c2d.bounds.center.x + c2d.bounds.extents.x &&
            (col.bounds.center.y + col.bounds.extents.y / 2) < c2d.bounds.center.y - c2d.bounds.extents.y) {
			isGrounded = true;
        }/* else {
            Vector2 move = new Vector2(0, -10);
            rb2d.velocity = (move);
            isGrounded = false;
        }*/
	}
	void OnTriggerExit2D(Collider2D col) {
		isGrounded = false;
	}


}
