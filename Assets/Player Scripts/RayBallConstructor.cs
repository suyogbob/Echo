using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Behavior of Ray Balls.
 * Change color and leave a marker when it passes out of a block.
 * Inherits from EchoBallConstructor for mostly code organization reasons
 */
public class RayBallConstructor : EchoBallConstructor {
	//counter used to give each marker a unique id (for debug)
	private static int count = 0;
	//this ball's id (debug)
	private int id;
	//elapsed time this ball has existed
	private float time = 0;
	//ball's collider
	private CircleCollider2D collider; 
	//ball's position
	private Transform transform;
	//ball's physics body
	private Rigidbody2D rb2d;
	//ball's sprite handler
	private SpriteRenderer sr;
	//buffer array for detecting collisions
	private Collider2D[] buffer;
	//if true, this ray ball is a marker and shouldn't spawn markers
	private bool mark = false;
	//if true, this ray ball just encountered a platform (used to detect exiting a platform)
	private bool last = false;
	//marker balls spawned by this object
	private GameObject[] marks;
	//how many markers we have already created
	private int mi = 0;

    // Use this for initialization
    void Start () {
		//generate id
		id = count;
		count++;

		//despawn timer
        Destroy(gameObject, timeTilDeath);

		//references
		collider = GetComponent<CircleCollider2D> ();
		transform = GetComponent<Transform> ();
		rb2d = GetComponent<Rigidbody2D> ();
		sr = GetComponent<SpriteRenderer> ();
		buffer = new Collider2D[5];

		//store up to 5 markers
		marks = new GameObject[5];

    }
	void Update()
	{
		//marks don't do anything, they are for show
		if (mark)
			return;

		//update timer
		time += Time.deltaTime;
		//do bitshifting math to create a layer mask for platforms and invis platforms
		LayerMask plm = LayerMask.NameToLayer ("Platforms");
		LayerMask iplm = LayerMask.NameToLayer ("InvisiblePlatforms");
		LayerMask cplm = ((1 << plm.value) | (1 << iplm.value));
		//create a contact filter
		ContactFilter2D cf2d = new ContactFilter2D ();
		cf2d.layerMask = plm;
		cf2d.useLayerMask = true;

		//detect all overlapping platforms and put them in the buffer
		buffer = Physics2D.OverlapCircleAll (transform.position, collider.radius, cplm.value);

		//count the platforms interacted
		int x = buffer.Length;
		int c = 0;
		for (int i = 0; i < x; i++) 
		{
			int l = buffer [i].gameObject.layer;
			if (l == LayerMask.NameToLayer ("Platforms") || l == LayerMask.NameToLayer ("InvisiblePlatforms")) 
			{
				c++;
			}
		}
		//if we are in a platform, remember for the next tick
		if (!last && c > 0)
			last = true;
		//if we WERE just in a platform (last == true) but now we are not (c == 0)
		else if (last && c == 0) 
		{
			//lighten by 50%
			Color o = sr.color;
			o.r = (o.r + 1) / 2;
			o.g = (o.g + 1) / 2;
			o.b = (o.b + 1) / 2;
			sr.color = o;

			last = false; //no longer in a platform

			if (mi >= 5) //if we have hit the max number of marks, turn into a mark and stop moving
			{
				mark = true;
				rb2d.velocity = new Vector2 (0, 0);
			} 
			else 
			{
				//otherwise, duplicate me, make the duplicate a mark, and increase the mark count
				marks [mi] = (GameObject)(Instantiate (gameObject, transform.position, new Quaternion ()));
				marks [mi].GetComponent<RayBallConstructor> ().mark = true;
				mi++;
			}

		}


	}

	//when I am destroyed, destroy all my markers.
	void OnDestroy()
	{
		//avoid array index out of bounds
		if (mi >= 5)
			mi = 4;
		//destroy the marks
		for (int i = 0; i < mi; i++)
			Destroy (marks [i], 0);
	}
}
