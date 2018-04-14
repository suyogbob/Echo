using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Manages the power system itself,
 * including GUI. References
 * the other power objects.
 */
public class Powers : MonoBehaviour {

	/* CONFIGURATIONS */
	//player reference
	public GameObject player;

	/* INTERNAL VARIABLES */
	//array of power objects
	private IPower[] powers;
    public LinkedList<IPower> powersList = new LinkedList<IPower>();
	//pointer to current power
	private IPower currentPower;
	//current accumulated cooldown
	private float cooldown;

    private InventoryView inv;
    /// <summary>
    /// Used by the inventory script to add PowerPickups' 
    /// powerScript attributes to the list of powers that can be used.
    /// </summary>
    /// <author>Chris Foley</author>
    /// <param name="set">The set of PowerPickups to be considered for the weapon wheel.</param>
    public void addPower(IPower newPower) {
        Debug.Log("Updating Powers List");
        newPower.init();
        powersList.AddLast(newPower);
        Debug.Log("Finished Adding " + newPower.ToString());
        inv = player.GetComponent<InventoryView>();
        
    }
    void Start()
    {
        Debug.Log("Starting");
        //Power are defined here rather than in the Inspector (Configurations)
        //because we haven't figured out how to put purely abstract C# objects
        //(i.e. not gameObjects) in the inspector
        //base definition

        //init powers
        /*
        powersList = new LinkedList<IPower>();
        currentPower = player.GetComponent<Movement>();
        currentPower.init();
        powersList.AddLast(currentPower);
        */
        player = gameObject;
        powersList = player.GetComponent<Inventory>().getPowerSet();
        if (powersList.Count == 0)
        {
            powersList = new LinkedList<IPower>();
            currentPower = player.GetComponent<Movement>();
            currentPower.init();
            powersList.AddLast(currentPower);
        }
        else {
            currentPower = player.GetComponent<Movement>();
            foreach (IPower p in powersList) p.init();
        }
		cooldown = 0;
	}
    
	//draw the power gui
    void OnGUI()
    {
        //Debug.Log("GUI Drawing list of "+powersList.Count);
		//setup gui style for current power
		int offset = 0;
        GUIStyle activeButton = new GUIStyle(GUI.skin.box);
        activeButton.normal.textColor = Color.red;
        activeButton.fontStyle = FontStyle.Bold;
        //handle powers indicators
		foreach(IPower p in powersList)
        {
			//bounding rectangle
			int x = Screen.width / 2 - 100 * powersList.Count / 2;
            Rect r = new Rect(x + offset * 100, Screen.height - 30, 100, 20);
			//draw active power
			if (p.getName() == currentPower.getName())
            {
				GUI.Box(r, p.getName(), activeButton);
            }
            else //draw other powers
            {
				GUI.Box(r, p.getName());
            }

            offset++;
        }
    }
	//hanlde swapping
    void Update() {
        if (inv == null)
            inv = player.GetComponent<InventoryView>();
        if (inv.showInventoryScreen)
            return;
        //movement is always power 0. only switch to movement if not already there
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E) || (Input.GetKeyDown(KeyCode.LeftShift) && (!currentPower.getName().Equals("Movement"))))
        {

            //deselect callback
            currentPower.switchFrom();
            IPower nextPower = null;
            IPower prevPower = null;
            IPower firstPower = null;
            IPower lastPower = null;
            bool foundCenter = false;
            foreach (IPower p in powersList)
            {

                if (firstPower == null) firstPower = p;
                if (p.getName().Equals(currentPower.getName()))
                {
                    foundCenter = true;
                }
                else if (foundCenter != true)
                {
                    prevPower = p;
                }
                else if (foundCenter && nextPower == null)
                {
                    nextPower = p;
                }
                lastPower = p;
            }
            //switch left
            if (Input.GetKeyDown(KeyCode.Q))
            {
                currentPower = prevPower;
                if (currentPower == null)
                    currentPower = lastPower;
            }
            //switch right
            else if (Input.GetKeyDown(KeyCode.E))
            {
                currentPower = nextPower;
                if (currentPower == null)
                    currentPower = firstPower;
            }
            else if (Input.GetKeyDown(KeyCode.LeftShift)) {//leftshift - go to movement (0)
                foreach (IPower p in powersList) {
                    if (p.getName().Equals("Movement")) currentPower = p;
                }
            }          

			//select callback
			currentPower.switchTo ();

			cooldown = 0;

        }
        else
        {
			//tick the power, and tell it if it is on cooldown.
			//update the cooldowns
			cooldown += currentPower.tick (cooldown > 0); 
			//tick the cooldown
			if (cooldown > 0)
				cooldown -= Time.deltaTime;
			if (cooldown < 0) //avoid negative cooldown
				cooldown = 0;
        }

        // Activate powers by pushing number keys, so player can go out of order. Assumes Movement is 1, Echo is 2, etc.

        // Loop for all ints between 1 and the number of powers
        int index = 1;
		foreach(IPower p in powersList) 
		{
			// If a number between 1 and the number of powers is pushed on the keyboard... 
			if (Input.GetKeyDown (index.ToString ())) {
				// Switch from last power
				currentPower.switchFrom ();
                currentPower = p;
				// Switch to next power
				currentPower.switchTo ();
				// And reset cooldown
				cooldown = 0;
			}
            index++;	
		}
    }

}


