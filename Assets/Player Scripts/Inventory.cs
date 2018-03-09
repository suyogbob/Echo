using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    HashSet<PowerPickups> activePowers = new HashSet<PowerPickups>();
    public HashSet<Pickups> inventoryList = new HashSet<Pickups>();
    Powers powerManager;
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
            if (powers.getName().Equals("Circular"))
            {
                Debug.Log("Circular");
                powers.powerScript = GameObject.Find("Player").GetComponent<Echo>();
            }
            else if (powers.getName().Equals("Flashlight"))
            {
                Debug.Log("Flashlight");
                powers.powerScript = GameObject.Find("Player").GetComponent<Flashlight>();
            }
            else if (powers.getName().Equals("Ray"))
            {
                Debug.Log("Ray");
                powers.powerScript = GameObject.Find("Player").GetComponent<Ray>();
            }
            else {
                Debug.Log("Not changing powerScript");
            }
            activePowers.Add(powers);
            powerManager.addPower(powers.powerScript);
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
            Debug.Log("Adding Power: " + p.getName()); 
            addToPowerSet((PowerPickups) p);
        }
        else {
            Debug.Log("Adding Inventory Item " + p.getName());
            inventoryList.Add(p);
        }
    }
    /*
    void OnGUI()
    {
        int y = 1;
        Rect r;
        foreach (PowerPickups p in activePowers) {
            r = new Rect(0, Screen.height - 20 * y, 100, 20);
            GUI.Box(r, p.getName());
            r = new Rect(100, Screen.height - 20 * y, 100, 20);
            if (p.powerScript != null)
            {
                GUI.Box(r, p.powerScript.getName());
            }
            else {
                GUI.Box(r, "NULL");
            }
            y++;
        }
        r = new Rect(0, Screen.height - 20 * y, 150, 20);
        GUI.Box(r, "Power List Inventory");
    }
    */
    //Loads the temporary inventory into inventory
    void Start () {
        //Reload non-power inventory
        inventoryList.Clear();
        inventoryList = tempInventory.inventoryPickup;
        //Reload power inventory
        Debug.Log("--------------------------------------------------------");
        powerManager = GameObject.Find("Player").GetComponent<Powers>();
        clearPowerSet();
        HashSet<PowerPickups> oldInventory = tempInventory.powerPickup;
        HashSet<PowerPickups> newInventory = new HashSet<PowerPickups>();
        foreach(PowerPickups p in oldInventory)
        {
            if (p.getName().Equals("Circular"))
            {
                newInventory.Add(new PowerPickups("Circular", GameObject.Find("Player").GetComponent<Echo>()));
            }
            else if (p.getName().Equals("Movement")) {
                newInventory.Add(new PowerPickups("Movement", GameObject.Find("Player").GetComponent<Movement>()));
            }
            else if (p.getName().Equals("Flashlight"))
            {
                newInventory.Add(new PowerPickups("Flashlight", GameObject.Find("Player").GetComponent<Flashlight>()));
            }
            else if (p.getName().Equals("Ray"))
            {
                newInventory.Add(new PowerPickups("Ray", GameObject.Find("Player").GetComponent<Ray>()));
            }
            else
            {
                Debug.Log("Temp Inventory Loading Error");
            }
        }
        Debug.Log("Starting Scene: (" + newInventory.Count + ", " + tempInventory.powerPickup.Count + ")");
        mergePowerSet(newInventory);
        IPower movementScript = GameObject.Find("Player").GetComponent<Movement>();
        Debug.Log("Starting Inventory");
        Debug.Log("Movement scripts name : " + movementScript.getName());
        PowerPickups movementPickup = new PowerPickups("Movement", movementScript);
        addToPowerSet(movementPickup);
	}
    void Update()
    {
        if (powerManager.powersList.Count != activePowers.Count) {
            powerManager.powersList = new LinkedList<IPower>();
            foreach(PowerPickups p in activePowers) {
                powerManager.addPower(p.powerScript);
            }
        }
    }
    public LinkedList<IPower> getPowerSet() {
        LinkedList<IPower> result = new LinkedList<IPower>();
        foreach (PowerPickups p in activePowers) {
            result.AddLast(p.powerScript);
        }
        return result;
    }
	// Update is called once per frame
	public void saveInventory () {
        tempInventory.powerPickup = activePowers;
        tempInventory.inventoryPickup = inventoryList;
	}
}
