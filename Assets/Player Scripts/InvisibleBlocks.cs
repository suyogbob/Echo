using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InvisibleBlocks : MonoBehaviour {
    public GameObject[] invis;
    public GameObject player;
    public Light playerLight;
    private float timeSinceUse;
    private SpriteRenderer[] ren;
    public Sprite rusty;
    public Sprite none;
    // Use this for initialization
    void Start () {
        playerLight = GameObject.Find("Point light").GetComponent<Light> ();
        player = GameObject.Find ("Player");
        timeSinceUse = 10f;
        invis = GameObject.FindGameObjectsWithTag ("Invisible");
        ren = new SpriteRenderer[invis.Length];
        for (int i = 0; i < invis.Length; i++) {
            ren [i] = invis [i].GetComponent<SpriteRenderer> ();
        }
    }
    
    // Update is called once per frame
    void Update () {
        for (int i = 0; i < invis.Length; i++) {
            if(timeSinceUse < 1f && playerLight.intensity == 5f && Vector3.Distance(invis[i].transform.position, player.transform.position) < 5)
                ren[i].sprite = rusty;
            else
                ren[i].sprite = none;
        }
        if (timeSinceUse > 1f && playerLight.intensity == 5f) {
            playerLight.intensity = 0.4f;
        }
    }
    void FixedUpdate () {
        if (Input.GetKey (KeyCode.F)) {
            playerLight.intensity = 5f;
            timeSinceUse = 0f;
        }
        timeSinceUse += Time.deltaTime;
    }
            
}