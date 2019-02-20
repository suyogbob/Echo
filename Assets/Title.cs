using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour {

    public static Save g_save = null;


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {


	}
    void OnGUI()
    {

            if(GUI.Button(new Rect(Screen.width / 2 - 50,Screen.height/2 - 50,100,50),  "New Game"))
            {
                SceneManager.LoadScene("Demo Level/INTRO");

            }

            GUI.enabled = File.Exists(Path.Combine(Application.persistentDataPath,"test.sav"));

            if(GUI.Button(new Rect(Screen.width / 2 - 50,Screen.height/2 + 10,100,50),  "Load Game"))
            {

                BinaryFormatter f = new BinaryFormatter();

                using (FileStream stream = new FileStream(Path.Combine(Application.persistentDataPath,"test.sav"), FileMode.Open))
                {
                    try
                    {
                        g_save =  f.Deserialize(stream) as Save;
                        Debug.Log("load");
                        SceneManager.LoadScene(g_save.level);


                    }
                    catch (Exception e)
                    {
                        Debug.Log(e);
                    }
                }

            }

            GUI.enabled = true;

            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 70, 100, 50), "Controls"))
            {
            SceneManager.LoadScene("options");
            }
    }

    public static void save(string level,float x,float y, LinkedList<string> p)
    {
        Debug.Log("hi");
        Save save = new Save();
        save.level = level;
        save.x = x;
        save.y = y;
        save.powers = p;

        foreach(string po in p)
            Debug.Log("power " + po);

            Debug.Log(Application.persistentDataPath);
        BinaryFormatter f = new BinaryFormatter();
        using (FileStream stream = new FileStream(Path.Combine(Application.persistentDataPath,"test.sav"), FileMode.Create))
        {
            try
            {
                f.Serialize(stream, save);
                Debug.Log("done save");
            }
            catch (Exception)
            {
                Debug.Log("bad");
            }
        }
    }
}

[Serializable]
public class Save
{
    public string level;

    public float x,y;

    public LinkedList<string> powers;
}
