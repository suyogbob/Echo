using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPickups : MonoBehaviour, Pickups
{
    public String name;
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
        Inventory script = GameObject.Find("Player").GetComponent<Inventory>();
        script.HandlePickup(this);
        //Find inventory and execute handlePickup
    }

    public override bool Equals(object other)
    {
        PowerPickups otherPickup = (PowerPickups)other;
        return getName().Equals(otherPickup.getName());
    }

    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.tag == "Player") {
            Debug.Log("Picking up " + getName());
            onPickup();
        }
    }
}
