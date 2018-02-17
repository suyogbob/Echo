using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPickups : MonoBehaviour, Pickups
{
    private String name;
    public PowerPickups(String name) {
        this.name = name;
    }
    public string getName()
    {
        return name; 
    }

    public bool isUnique()
    {
        return true;
    }

    public void onPickup()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.tag == "Player") {
            Debug.Log("Picking up " + getName());
            onPickup();
        }
    }
}
