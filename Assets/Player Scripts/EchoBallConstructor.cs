using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoBallConstructor : MonoBehaviour {
    public float timeTilDeath;
    public GameObject neighbor;
    public GameObject otherNeighbor;
    public float breakingPoint;
    private bool broken;
    // Use this for initialization
    void Start () {
        Destroy(gameObject, timeTilDeath);
        //broken = false;
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
