using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Manages the motion of moving platforms
 * so they follow a predefined path and interact
 * correctly with player deaths.
 * 
 * Each Moving Platform has a list of "targets": each is a location.
 * The moving platform will move on a straight-line path towards each target
 * until it reaches the target. Then it will go to the next target, and
 * proceed indefinitely.
 */
public class MovingPlatform : MonoBehaviour {

	/* INTERNAL VARIABLES */

	//platform rigidbody
	private Rigidbody2D rb2d;
	//platform position
	private Transform transform;
	//index of the list of positions we are currently pursuing
	private int currentTarget;
	//initial position (for respawns)
	private Vector3 start;
	//how close to the target you have to be to count as "arriving"
	private float epsilon = 0.1f;

	/* CONFIGURATIONS */

	//points on the path to visit (in order)
	public Vector3[] positions;
	// how fast to move
	public float velocityScalar;

	void Start () 
	{
		//initialize components
		rb2d = GetComponent<Rigidbody2D> ();
		transform = GetComponent<Transform> ();
		//start by purusing the first target (index 0)
		currentTarget = 0;
		//save the initial position for later
		start = transform.position;
		//if the targets array is empty, create a single target and just stay there
		if (positions.Length == 0) {
			positions = new Vector3[1];
			positions [0] = new Vector3(0,0,0);
		}

		//tell the level manager about this mover
		FindObjectOfType<LevelManager> ().registerMover (this);
	}
	
	void Update () 
	{
		//if we are close enough (epsilon) to the target, go to next target (and wrap around)
		if ((positions [currentTarget] + start - transform.position).magnitude <= epsilon)
			currentTarget = (currentTarget + 1) % positions.Length;

		//set our velocity appropriately
		rb2d.velocity = (positions [currentTarget] + start - transform.position).normalized * velocityScalar;

	}

	//public-facing method to go back to start position (called by level manager)
	public void reset()
	{
		transform.position = start;
		currentTarget = 0;
	}
}
