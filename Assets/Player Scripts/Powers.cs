using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powers : MonoBehaviour {
	//GENERAL POWER VARIABLES
	public enum Power { Circular, Movement, Flashlight, Ray };
	public Power activePower;
	//CIRCULAR VARIABLES
	public Object ball;
	public int numberOfBalls;
	public float velocityScalar;
	public GameObject firstBall;
	public float cooldown = 0;
	//MOVEMENT VARIABLES
	private Rigidbody2D rb2d;
	private CircleCollider2D c2d;
	private Collider2D groundSensor;
	public float speedInitial;//8
	public float jumpStrength;//150
	public bool isGrounded;
	private float speed;
    private int platformsIndex;
    private GameObject platform;
	//INVISIBLE BLOCKS VARIABLES
	public GameObject[] invis;
	public GameObject player;
	public Light playerLight;
	private float timeSinceUse;
	private SpriteRenderer[] ren;
	public Sprite rusty;
	public Sprite none;
	//RAY VARIABLES
	public Object rayBall;
	private Transform pointerTransform;
	private SpriteRenderer pointerRenderer;
	public Sprite pointer_sprite;
	public float angle = 0;
	//GENERAL POWER SCRIPTS
	void Start() {
		//General Power Start Up
		activePower = Power.Movement;
		//Movement Start Up
		rb2d = GetComponent<Rigidbody2D>();
		c2d = GetComponent<CircleCollider2D>();
		groundSensor = GameObject.Find("Ground_Sensor").GetComponent<Collider2D>();
		speed = speedInitial;
		isGrounded = true;
        // Find platforms layer
        platformsIndex = LayerMask.NameToLayer("Platforms");
		//Invisible Start Up
		playerLight = GameObject.Find("Point light").GetComponent<Light>();
		player = GameObject.Find("Player");
		timeSinceUse = 10f;
		invis = GameObject.FindGameObjectsWithTag("Invisible");
		ren = new SpriteRenderer[invis.Length];
		for (int i = 0; i < invis.Length; i++)
		{
			ren[i] = invis[i].GetComponent<SpriteRenderer>();
		}
		//Ray startup
		pointerRenderer = GameObject.Find("pointer").GetComponent<SpriteRenderer>();
		pointerTransform = GameObject.Find("pointer").GetComponent<Transform>();

	}
    void OnGUI()
    {
        int offset = 0;
        int numOfPowers = System.Enum.GetNames(typeof(Power)).Length;
        GUIStyle activeButton = new GUIStyle(GUI.skin.box);
        activeButton.normal.textColor = Color.red;
        activeButton.fontStyle = FontStyle.Bold;
        foreach (string powers in System.Enum.GetNames(typeof(Power)))
        {
            int x = Screen.width / 2 - 100 * numOfPowers / 2;
            Rect r = new Rect(x + offset * 100, Screen.height - 30, 100, 20);
            if (powers.Equals(activePower.ToString()))
            {
                GUI.Box(r, powers, activeButton);
            }
            else
            {
                GUI.Box(r, powers);
            }
            offset++;
        }
    }
    void Update() {
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E) || (Input.GetKeyDown(KeyCode.LeftShift) && (activePower != Power.Movement)))
        {
            //TODO negate current active power
            switch (activePower)
            {
                case Power.Circular:
                    //DESTROY ALL THE BALLS
                    while (firstBall != null)
                    {
                        GameObject nextBall = firstBall.GetComponent<EchoBallConstructor>().otherNeighbor;
                        Destroy(firstBall);
                        firstBall = nextBall;
                    }
                    break;
                case Power.Flashlight:
                    //RESET FLASHLIGH
                    playerLight.intensity = 0.0f;
                    for (int i = 0; i < invis.Length; i++)
                    {
                        ren[i].sprite = none;
                    }
                    //pointerRenderer.sprite = pointer_sprite;
                    break;
                case Power.Ray:
                    //DESTROY ALL THE BALLS
                    while (firstBall != null)
                    {
                        GameObject nextBall = firstBall.GetComponent<RayBallConstructor>().otherNeighbor;
                        Destroy(firstBall);
                        firstBall = nextBall;
                    }
                    pointerRenderer.sprite = none;
                    break;
                case Power.Movement:
                    //Stop Horizontal Movement
                    Vector2 movement = new Vector2(0, rb2d.velocity.y);
                    rb2d.velocity = (movement);
                    break;
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                switch (activePower)
                {
                    case Power.Circular:
                        Debug.Log("Swapping to flashlight");
                        pointerRenderer.sprite = pointer_sprite;
                        activePower = Power.Ray;
                        break;
                    case Power.Movement:
                        Debug.Log("Swapping to circular");
                        activePower = Power.Circular;
                        break;
                    case Power.Flashlight:
                        Debug.Log("Swapping to ray");
                        activePower = Power.Movement;
                        break;
                    case Power.Ray:
                        Debug.Log("Swapping to movement");
                        pointerRenderer.sprite = none;
                        activePower = Power.Flashlight;
                        break;
                }
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                switch (activePower)
                {
                    case Power.Circular:
                        Debug.Log("Swapping to movement");
                        activePower = Power.Movement;
                        break;
                    case Power.Movement:
                        Debug.Log("Swapping to ray");
                        activePower = Power.Flashlight;
                        break;
                    case Power.Flashlight:
                        Debug.Log("Swapping to circular");
                        pointerRenderer.sprite = pointer_sprite;
                        activePower = Power.Ray;
                        break;
                    case Power.Ray:
                        Debug.Log("Swapping to flashlight");
                        pointerRenderer.sprite = none;
                        activePower = Power.Circular;
                        break;
                }
            }
            else if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                Debug.Log("Swapping back to movement");
                pointerRenderer.sprite = none;
                activePower = Power.Movement;
            }
        }
        else
        {
            switch (activePower)
            {
                case Power.Circular:
                    circularEcho();
                    break;
                case Power.Flashlight:
                    invisible();
                    break;
                case Power.Ray:
                    rayPower();
                    break;
                case Power.Movement:
                    movement();
                    break;
            }
        }
    }

	//MOVEMENT SCRIPTS
	void movement()
	{
		if (Input.GetKey(KeyCode.LeftShift))
		{
			speed = 2 * speedInitial;
		}
		else
		{
			speed = speedInitial;
		}

		float moveHorizontal = Input.GetAxis("Horizontal");
		Vector2 movement = new Vector2(moveHorizontal * speed, rb2d.velocity.y);
		rb2d.velocity = (movement);
		//rb2d.velocity = movement * speed;
		if (Input.GetKey(KeyCode.S))
		{
			Vector2 move = new Vector2(0, -3) * speed;
			rb2d.AddForce(move);
		}
		if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
		{
			Vector2 move = new Vector2(0, 1) * jumpStrength;
			rb2d.AddForce(move);
			isGrounded = false;
		}

	}

    void OnTriggerEnter2D(Collider2D col)
    {
        platform = col.gameObject;
        if (platform.layer == platformsIndex)
        {
            isGrounded = true;
        }
    }

	void OnTriggerStay2D(Collider2D col)
	{
		Rigidbody2D box = col.attachedRigidbody;
		//Debug.Log ("platform right edge: " + (col.bounds.center.x + col.bounds.extents.x));
		//Debug.Log ("player left edge: " + (c2d.bounds.center.x - c2d.bounds.extents.x));
		//Debug.Log("player bottom edge: " + (c2d.bounds.center.y - c2d.bounds.extents.y));
		//Debug.Log("platform top edge: " + (col.bounds.center.y + col.bounds.extents.y));
		//Debug.Log ("\n");
		/*if (col.bounds.center.x + col.bounds.extents.x > c2d.bounds.center.x - c2d.bounds.extents.x &&
            col.bounds.center.x - col.bounds.extents.x < c2d.bounds.center.x + c2d.bounds.extents.x &&
            (col.bounds.center.y + col.bounds.extents.y / 2) < c2d.bounds.center.y - c2d.bounds.extents.y) {
			isGrounded = true;
        } else {
            Vector2 move = new Vector2(0, -10);
            rb2d.velocity = (move);
            isGrounded = false;
        }*/
	}

    void OnTriggerExit2D(Collider2D col)
    {
        platform = col.gameObject;
        if (platform.layer == platformsIndex)
        {
            isGrounded = false;
        }
    }

	//CIRCULAR SCRIPTS
	void circularEcho () {
		if (cooldown > 0)
		{
			cooldown -= Time.deltaTime;
			return;
		}
		if (Input.GetKeyDown(KeyCode.F))
		{
			cooldown = 2;
			while (firstBall != null) 
			{
				GameObject nextBall = firstBall.GetComponent<EchoBallConstructor> ().otherNeighbor;
				Destroy (firstBall);
				firstBall = nextBall;
			}

			Transform parent = GetComponent<Transform>();
			Quaternion none = new Quaternion();
			GameObject previousBall = null;
			for (float rad = 0; rad < 2 * Mathf.PI; rad += 2 * Mathf.PI / numberOfBalls)
			{
				Vector2 offset = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
				GameObject theBall = (GameObject)Instantiate(ball, parent.position, none);
				Rigidbody2D rgbd = theBall.GetComponent<Rigidbody2D>();
				if (rad == 0) {
					firstBall = theBall;
				}
				else
				{
					if (rad == (2*Mathf.PI)*(numberOfBalls - 1)/numberOfBalls)
					{
						firstBall.GetComponent<EchoBallConstructor>().neighbor = theBall;
						theBall.GetComponent<EchoBallConstructor>().otherNeighbor = firstBall;
					}
					theBall.GetComponent<EchoBallConstructor>().neighbor = previousBall;
					previousBall.GetComponent<EchoBallConstructor>().otherNeighbor = theBall;
				}
				rgbd.velocity = offset*velocityScalar;
				previousBall = theBall;
			}
		}
	}
	//INVISIBLE SCRIPTS
	void invisible()
	{

		for (int i = 0; i < invis.Length; i++)
		{
			if (timeSinceUse < 1f && playerLight.intensity == 5f && Vector3.Distance(invis[i].transform.position, player.transform.position) < 5)
				ren[i].sprite = rusty;
			else
				ren[i].sprite = none;
		}

		if (timeSinceUse > 1f && playerLight.intensity == 5f)
		{
			playerLight.intensity = 0.0f;
		}
	}

	void FixedUpdate()
	{
		if (activePower.Equals(Power.Flashlight))
		{
			if (Input.GetKey(KeyCode.F))
			{
				LightFlash();
			}
			timeSinceUse += Time.deltaTime;
		}
	}

	void LightFlash()
	{
		playerLight.intensity = 5f;

		timeSinceUse = 0f;
	}

	//RAY SCRIPTS
	void rayPower()
	{
		if (Input.GetKeyDown (KeyCode.A)) 
		{
			angle += Mathf.PI / 12;
			if (angle > Mathf.PI * 2)
				angle -= (Mathf.PI * 2);
			Vector3 v = pointerTransform.position;
			v.x = Mathf.Cos (angle) * 3 + player.transform.position.x;
			v.y = Mathf.Sin (angle) * 3 + player.transform.position.y;
			pointerTransform.position = v;
			pointerTransform.Rotate (new Vector3 (0, 0, 7.5f));
		}
		if (Input.GetKeyDown (KeyCode.D)) 
		{
			angle -= Mathf.PI / 12;
			if (angle < 0)
				angle += (Mathf.PI * 2);

			Vector3 v = pointerTransform.position;
			v.x = Mathf.Cos (angle) * 3 + player.transform.position.x;
			v.y = Mathf.Sin (angle) * 3 + player.transform.position.y;
			pointerTransform.position = v;
			pointerTransform.Rotate (new Vector3 (0, 0, -7.5f));

		}
		if (cooldown > 0)
		{
			cooldown -= Time.deltaTime;
			return;
		}
		if (Input.GetKeyDown(KeyCode.F))
		{
			cooldown = 5;
			while (firstBall != null) 
			{
				GameObject nextBall = firstBall.GetComponent<EchoBallConstructor> ().otherNeighbor;
				Destroy (firstBall);
				firstBall = nextBall;
			}

			Transform parent = GetComponent<Transform>();
			Quaternion none = new Quaternion();
			GameObject previousBall = null;

			float radStart = angle - Mathf.PI / 24;
			float radEnd = angle + Mathf.PI / 24;
			float c = numberOfBalls / (2*Mathf.PI) * (radEnd-radStart);

			for (int i = 0; i < c; i++)
			{
				float rad = radStart + (radEnd - radStart) / c * i;

				Vector2 offset = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
				GameObject theBall = (GameObject)Instantiate(rayBall, parent.position, none);
				Rigidbody2D rgbd = theBall.GetComponent<Rigidbody2D>();
				if (rad == radStart) {
					firstBall = theBall;
				}
				else
				{
					if (rad >= radEnd)
					{
						firstBall.GetComponent<RayBallConstructor>().neighbor = theBall;
						theBall.GetComponent<RayBallConstructor>().otherNeighbor = firstBall;
					}
					theBall.GetComponent<RayBallConstructor>().neighbor = previousBall;
					previousBall.GetComponent<RayBallConstructor>().otherNeighbor = theBall;
				}
				rgbd.velocity = offset*velocityScalar;
				previousBall = theBall;
				//break;
			}
		}
	}
}


