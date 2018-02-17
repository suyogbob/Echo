using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    HashSet<PowerPickups> activePowers = new HashSet<PowerPickups>();
    int getPowerSetSize() {
        return activePowers.Count;
    }
    PowerPickups getPowerPickupAt(int index) {
        if( index >= getPowerSetSize()) return null;
        int count = 0;
        foreach (PowerPickups p in activePowers) {
            if (count == index) {
                return p;
            }
            count++;
        }
        return null;
    }
    void addToPowerSet(PowerPickups powers) {
        bool found = false;
        foreach (PowerPickups p in activePowers) {
            if (p.getName().Equals(powers.getName())) found = true;
        }
        if (!found) {
            if (powers.getName().Equals("Echo"))
            {
                powers.powerScript = GameObject.Find("Player").GetComponent<Echo>();
            }
            else if (powers.getName().Equals("Flashligh"))
            {
                powers.powerScript = GameObject.Find("Player").GetComponent<Flashlight>();
            }
            else if (powers.getName().Equals("Ray"))
            {
                powers.powerScript = GameObject.Find("Player").GetComponent<Ray>();
            }
            else {
                Debug.Log("Not changing powerScript");
            }
            activePowers.Add(powers);
        }
    }
    void clearPowerSet() {
        activePowers.Clear();
    }
    void mergePowerSet(HashSet<PowerPickups> set) {
        foreach (PowerPickups p in set) {
            activePowers.Add(p);
        }
    }
    public void HandlePickup(Pickups p) {
        if (p is PowerPickups)
        {
            Debug.Log("Handling: " + p.getName()); 
            addToPowerSet((PowerPickups) p);
        }
        else {
            Debug.Log("What is " + p.getName() + "?");
            throw new System.Exception();
        }
    }
    void OnGUI()
    {
        int y = 1;
        Rect r;
        foreach (PowerPickups p in activePowers) {
            r = new Rect(0, Screen.height - 20 * y, 100, 20);
            GUI.Box(r, p.getName());
            y++;
        }
        r = new Rect(0, Screen.height - 20 * y, 150, 20);
        GUI.Box(r, "Power List Inventory");
    }
    //Loads the temporary inventory into inventory
    void Start () {
        mergePowerSet(tempInventory.powerPickup);
        IPower movementScript = GameObject.Find("Player").GetComponent<Movement>();
        PowerPickups movementPickup = new PowerPickups("Movement", movementScript);
        addToPowerSet(movementPickup);
	}
	// Update is called once per frame
	public void saveInventory () {
        tempInventory.powerPickup = activePowers;
	}
}
