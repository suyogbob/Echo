using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapView : MonoBehaviour {
    public bool showMapScreen = false;
    string description = "Here's your current location";
    // Use this for initialization
    void Start () {
        showMapScreen = false;
    }
    //Screen
    void OnGUI()
    {
        if (showMapScreen) {
            GUI.color = new Color(1f, 1f, 1f, 2f);
            Rect r;
            // Inventory inventoryObject = GameObject.Find("Player").GetComponent<Inventory>();
            int boxWidth = 500;
            int titleWidth = 200;
            int titleHeight = 30;
            int y = 0;
            r = new Rect((Screen.width - boxWidth) / 2 - titleWidth, 40 + titleHeight * y, titleWidth, titleHeight);
            GUI.Box(r, "Map");
            y++;

        }
    }
    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.M)) {
            showMapScreen = !showMapScreen;
        }
    }
}
