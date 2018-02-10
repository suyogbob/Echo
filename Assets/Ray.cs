using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ray : MonoBehaviour, IPower 
{

	public Object rayBall;
	public int numberOfBalls;
	public float velocityScalar;
	public GameObject player;
	private Transform pointerTransform;
	private SpriteRenderer pointerRenderer;
	public Sprite pointer_sprite;
	public Sprite none;
	private GameObject firstBall;
	public float angle = 0;

	public string getName()
	{
		return "Ray";
	}

	public void init()
	{
		pointerRenderer = GameObject.Find("pointer").GetComponent<SpriteRenderer>();
		pointerTransform = GameObject.Find("pointer").GetComponent<Transform>();
	}

	public void tick()
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
			pointerTransform.Rotate (new Vector3 (0, 0, 15f));
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
			pointerTransform.Rotate (new Vector3 (0, 0, -15f));

		}
		if (Input.GetKeyDown(KeyCode.F))
		{
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

	public void switchFrom()
	{
		while (firstBall != null)
		{
			GameObject nextBall = firstBall.GetComponent<RayBallConstructor>().otherNeighbor;
			Destroy(firstBall);
			firstBall = nextBall;
		}
		pointerRenderer.sprite = none;
	}

	public void switchTo()
	{
		pointerRenderer.sprite = pointer_sprite;
	}

}
