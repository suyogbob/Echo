using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayBallConstructor : MonoBehaviour {
	private static int count = 0;
	private int id;
	private float time = 0;
    public float timeTilDeath;
    public GameObject neighbor;
    public GameObject otherNeighbor;
    public float breakingPoint;
    private bool broken;
	private CircleCollider2D collider;
	private Transform transform;
	private Rigidbody2D rb2d;
	private SpriteRenderer sr;
	private Collider2D[] buffer;
	private bool mark = false;
	private bool last = false;
	private GameObject[] marks;
	private int mi = 0;
    // Use this for initialization
    void Start () {
		id = count;
		count++;
		Debug.Log ("initialize id " + id);

        Destroy(gameObject, timeTilDeath);
        //broken = false;
		collider = GetComponent<CircleCollider2D> ();
		transform = GetComponent<Transform> ();
		rb2d = GetComponent<Rigidbody2D> ();
		sr = GetComponent<SpriteRenderer> ();
		buffer = new Collider2D[5];

		marks = new GameObject[5];

    }
	void Update()
	{
		if (mark)
			return;
		
		time += Time.deltaTime;
		LayerMask plm = LayerMask.NameToLayer ("Platforms");
		LayerMask iplm = LayerMask.NameToLayer ("InvisiblePlatforms");
		LayerMask cplm = ((1 << plm.value) | (1 << iplm.value));
		ContactFilter2D cf2d = new ContactFilter2D ();
		cf2d.layerMask = plm;
		cf2d.useLayerMask = true;

		buffer = Physics2D.OverlapCircleAll (transform.position, collider.radius, cplm.value);

		int x = buffer.Length;
		int c = 0;
		for (int i = 0; i < x; i++) 
		{
			int l = buffer [i].gameObject.layer;
			Debug.Log("id " + id + " encountered an object of layer " + LayerMask.LayerToName(buffer[i].gameObject.layer));
			if (l == LayerMask.NameToLayer ("Platforms") && l != LayerMask.NameToLayer ("InvisiblePlatforms")) 
			{
				c++;
			}
		}
		if (!last && c > 0)
			last = true;
		else if (last && c == 0) 
		{
			Color o = sr.color;
			o.r = (o.r + 1) / 2;
			o.g = (o.g + 1) / 2;
			o.b = (o.b + 1) / 2;
			sr.color = o;

			last = false;
			if (mi >= 5) 
			{
				mark = true;
				rb2d.velocity = new Vector2 (0, 0);
			} 
			else 
			{
				marks [mi] = (GameObject)(Instantiate (gameObject, transform.position, new Quaternion ()));
				marks [mi].GetComponent<RayBallConstructor> ().mark = true;
				mi++;
			}

		}


	}

	void OnDestroy()
	{
		if (mi >= 5)
			mi = 4;
		for (int i = 0; i < mi; i++)
			Destroy (marks [i], 0);
	}
    /*void Update() {
        //if (broken) return;
        //LineRenderer LR = GetComponent<LineRenderer>();
        Rigidbody2D MyRGBD = GetComponent<Rigidbody2D>();
        Rigidbody2D TheirRGBD = neighbor.GetComponent<Rigidbody2D>();
        Rigidbody2D TheOtherRGBD = neighbor.GetComponent<Rigidbody2D>();
        
        Vector3 neighborDist = new Vector3(TheirRGBD.position.x-MyRGBD.position.x, TheirRGBD.position.y - MyRGBD.position.y, 0);
        Vector3 otherNeighborDist = new Vector3(TheOtherRGBD.position.x - MyRGBD.position.x, TheOtherRGBD.position.y - MyRGBD.position.y, 0);
        //LR.SetPosition(1, point);
        Debug.Log("---------------");
        Debug.Log(neighborDist.magnitude);
        Debug.Log(otherNeighborDist.magnitude);
        if ((neighborDist.magnitude >= breakingPoint)&&(otherNeighborDist.magnitude >= breakingPoint)) {
            //broken = true;
            //TODO: set invisible
        }
        
    }*/
}
