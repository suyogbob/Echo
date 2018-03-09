﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DocPickup : MonoBehaviour, Pickups {
    public String name;
    public String text;
    public IPower powerScript;
    public DocPickup(String name, String text, IPower powerScript)
    {
        this.text = text;
        this.name = name;
        this.powerScript = powerScript;
    }
    public string getName()
    {
        return "[Document: "+name+"]";
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
        DocPickup otherPickup = (DocPickup)other;
        return getName().Equals(otherPickup.getName());
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            Debug.Log("Picking up " + getName());
            onPickup();
        }
    }
}