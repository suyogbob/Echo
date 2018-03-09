using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryView : MonoBehaviour {
    bool showInventoryScreen = false;
	// Use this for initialization
	void Start () {
        showInventoryScreen = false;
	}
    //Screen
    void OnGUI()
    {
        if (showInventoryScreen) { 
            int y = 1;
            Rect r;
            r = new Rect(0, Screen.height - 20 * y, 150, 20);
            GUI.Box(r, "Inventory");
            y++;
            Inventory inventoryObject = GameObject.Find("Player").GetComponent<Inventory>();
            foreach (Pickups p in inventoryObject.inventoryList)
            {
                r = new Rect(0, Screen.height - 20 * y, 100, 20);
                GUI.Box(r, p.getName());
                y++;
            }
        }
    }
    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.I)) {
            showInventoryScreen = !showInventoryScreen;
        }
	}
}
