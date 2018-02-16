using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

	private Rigidbody2D rb2d;
	private Transform transform;
	public Vector3[] positions;
	public float velocityScalar;
	private int currentTarget;
	private Vector3 start;

	private float epsilon = 0.1f;

	// Use this for initialization
	void Start () 
	{
		rb2d = GetComponent<Rigidbody2D> ();
		transform = GetComponent<Transform> ();
		currentTarget = 0;
		start = transform.position;
		if (positions.Length == 0) {
			positions = new Vector3[1];
			positions [0] = new Vector3(0,0,0);
		}

		FindObjectOfType<LevelManager> ().registerMover (this);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if ((positions [currentTarget] + start - transform.position).magnitude <= epsilon)
			currentTarget = (currentTarget + 1) % positions.Length;
		rb2d.velocity = (positions [currentTarget] + start - transform.position).normalized * velocityScalar;

	}

	public void reset()
	{
		transform.position = start;
		currentTarget = 0;
	}
}
