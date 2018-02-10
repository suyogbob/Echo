using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Echo : MonoBehaviour, IPower 
{
	public Object ball;
	public int numberOfBalls;
	public float velocityScalar;
	private GameObject firstBall;

	public string getName()
	{
		return "Circular";
	}

	public void init()
	{
	}

	public void tick()
	{
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

	public void switchFrom()
	{
		while (firstBall != null)
		{
			GameObject nextBall = firstBall.GetComponent<EchoBallConstructor>().otherNeighbor;
			Destroy(firstBall);
			firstBall = nextBall;
		}
	}

	public void switchTo()
	{
	}	
}
