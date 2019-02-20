using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryView : MonoBehaviour {
    public bool showInventoryScreen = false;
    AudioSource source;
    string description = "Please select a document to look at";
    Pickups p;
    public Vector2 scrollPosition;
    // Use this for initialization
    void Start () {
        showInventoryScreen = false;
	}
    //Screen
    void OnGUI()
    {
        if (showInventoryScreen) {
            GUI.color = new Color(1f, 1f, 1f, 2f);
            Rect r;
            Inventory inventoryObject = GameObject.Find("Player").GetComponent<Inventory>();
            int boxWidth = 500;
            int titleWidth = 200;
            int titleHeight = 30;
            int y = 0;
            r = new Rect((Screen.width - boxWidth) / 2 - titleWidth, 40 + titleHeight * y, titleWidth, titleHeight);
            GUI.Box(r, "Inventory");
            y++;

            //scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(titleWidth), GUILayout.Height((Screen.height - 200)));
            foreach (Pickups p in inventoryObject.inventoryList)
            {
                if(40 + titleHeight * y < 40 + (Screen.height - 200)) {
                    r = new Rect((Screen.width - boxWidth) / 2 - titleWidth, 40 + titleHeight * y, titleWidth, titleHeight);
                    if (GUI.Button(r, p.getName()))
                    {
                        source = GameObject.Find("Player").GetComponent<AudioSource>();
                        source.Stop();
                        description = p.getText();
                        this.p = p;
                    }
                    y++;
                }
            }
            //GUILayout.EndScrollView();
            GUI.skin.box.wordWrap = true;
            r = new Rect((Screen.width - boxWidth) / 2, 40, boxWidth, (Screen.height - 200));
            GUI.Box(r, description);

            //Draw Play Button
            r = new Rect((Screen.width + boxWidth) / 2 - 50, Screen.height - 160 - 30, 50, 30);
            char playChar = '\u25B6';
            if(p != null) {
                if (GUI.Button(r, playChar.ToString())) {
                    source = GameObject.Find("Player").GetComponent<AudioSource>();
                    source.Stop();
                    p.playAudio();
                }
            }

            if(GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height - 100, 100, 50),"Save"))
            {
                Transform t = GameObject.Find("Player").GetComponent<Transform>();
                Inventory inv = GameObject.Find("Player").GetComponent<Inventory>();
                int size = inv.getPowerSetSize();
                LinkedList<string> powerNames = new LinkedList<string>();
                for(int i = 0; i < size; i++)
                {
                    powerNames.AddLast(inv.getPowerPickupAt(i).name);
                }
                Title.save(SceneManager.GetActiveScene().path,t.position.x,t.position.y,powerNames);
            }
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
