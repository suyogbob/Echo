using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
	private Rigidbody2D rb2d;
	private Collider2D groundSensor;
	public float speedInitial;
	public float jumpStrength;
	private bool isGrounded;
	private float speed;
	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		groundSensor = GameObject.Find("Ground_Sensor").GetComponent<Collider2D>();
		speed = speedInitial;
	}
	
	// Update is called once per frame
	void Update () {
		isGrounded = (groundSensor.IsTouchingLayers () ? true : false);
		if (Input.GetKey (KeyCode.LeftShift)) {
			speed = 2 * speedInitial;
		} else {
			speed = speedInitial;
		}
		if (Input.GetKey (KeyCode.D)) {
			Vector2 move = new Vector2 (1, 0)*speed;
			rb2d.AddForce (move);
		}
		if (Input.GetKey(KeyCode.A)) {
			Vector2 move = new Vector2 (-1, 0)*speed;
			rb2d.AddForce (move);
		}
		if (Input.GetKey (KeyCode.S)) {
			Vector2 move = new Vector2 (0, -3)*speed;
			rb2d.AddForce (move);
		}
		if (Input.GetKeyDown (KeyCode.Space) && isGrounded) {
			Vector2 move = new Vector2 (0, 1)*jumpStrength;
			rb2d.AddForce (move);
		}

	}


}
