using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Manages the individual behavior of the echo ball particles
 */
public class EchoBallConstructor : MonoBehaviour {
	/* CONFIGURATIONS */

	//how long the balls last
    public float timeTilDeath;

	//linked list implementation
    public GameObject neighbor;
    public GameObject otherNeighbor;


	//these were part of some code to make the balls die ("break") if they got too far away from
	//their neighbors. they are currently unused.
	/*
    public float breakingPoint;
	private bool broken;
	*/

	void Start () {
		//put the object on a destruction timer
        Destroy(gameObject, timeTilDeath);
        //broken = false;
    }

	//unused particle-breaking code
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
