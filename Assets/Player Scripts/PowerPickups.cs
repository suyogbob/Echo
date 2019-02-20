using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPickups : MonoBehaviour, Pickups
{
    public String name;
    public IPower powerScript;
    public AudioClip audioLog;
    public String text;
    public PowerPickups(String name, IPower powerScript, AudioClip audioLog, String text) {
        this.name = name;
        this.powerScript = powerScript;
        this.text = text;
        this.audioLog = audioLog;
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

    public string getText()
    {
        return text;
    }

    public void playAudio()
    {
        Debug.Log("[" + getName() + "] " + "Starting Audio");
        AudioSource source = GameObject.Find("Player").GetComponent<AudioSource>();
        source.PlayOneShot(audioLog);
    }
}
