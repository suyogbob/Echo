using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryView : MonoBehaviour {
    public bool showInventoryScreen = false;
    string description = "Please select a document to look at";
    // Use this for initialization
    void Start () {
        showInventoryScreen = false;
	}
    //Screen
    void OnGUI()
    {
        if (showInventoryScreen) {
            Rect r;
            Inventory inventoryObject = GameObject.Find("Player").GetComponent<Inventory>();
            int boxWidth = 500;
            int titleWidth = 200;
            int titleHeight = 30;
            int y = 0;
            r = new Rect((Screen.width - boxWidth) / 2 - titleWidth, 40 + titleHeight * y, titleWidth, titleHeight);
            GUI.Box(r, "Inventory");
            y++;
            foreach (Pickups p in inventoryObject.inventoryList)
            {
                r = new Rect((Screen.width - boxWidth) / 2 - titleWidth, 40 + titleHeight * y, titleWidth, titleHeight);
                if (GUI.Button(r, p.getName()))
                {
                    AudioSource source = GameObject.Find("Player").GetComponent<AudioSource>();
                    source.Stop();
                    description = ((DocPickup)p).getText();
                    ((DocPickup)p).playAudio();
                }
                y++;
            }
            GUI.skin.box.wordWrap = true;
            r = new Rect((Screen.width - boxWidth) / 2, 40, boxWidth, (Screen.height - 200));
            GUI.Box(r, description);
        }
    }
    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.I)) {
            showInventoryScreen = !showInventoryScreen;
            if (showInventoryScreen == false)
            {
                Inventory inventoryObject = GameObject.Find("Player").GetComponent<Inventory>();
                AudioSource source = GameObject.Find("Player").GetComponent<AudioSource>();
                source.Stop();
            }
        }
	}
}
