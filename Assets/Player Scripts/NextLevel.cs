using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Manages the level transitions.
 * Detect collisions and do the unity-backend scene loading
 */
public class NextLevel : MonoBehaviour {

	/* CONFIGURATIONS*/
	//player reference
    public GameObject player;
	//name of level to load
	public string levelToLoad;

    public bool customTarget = false;
	public float targetX = 0;
    public float targetY = 0;

	/* INTERNAL */
	//light source
	private Light spot;

	// Use this for initialization
	void Start () {
		//setup referendes
		//spot = GameObject.Find("Spotlight").GetComponent<Light> ();
		//spot.transform.localPosition = new Vector2 (0f, 1.5f);
        player = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D(Collider2D diamondCollider) {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (diamondCollider.gameObject.tag == player.tag)
        {
            //If the GameObject's name matches the one you suggest, output this message in the console
            Inventory script = GameObject.Find("Player").GetComponent<Inventory>();
            script.saveInventory();
            if(customTarget)
            {
                PlayerPrefs.SetInt("position_save", 1);
                PlayerPrefs.SetFloat("x", targetX);
                PlayerPrefs.SetFloat("y", targetY);
            }
            SceneManager.LoadScene(levelToLoad);
        }
	}
}
