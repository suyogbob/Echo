using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Echo : MonoBehaviour, IPower 
{
	public Object ball;
	public int numberOfBalls;
	public float velocityScalar;
	protected GameObject firstBall;

	public virtual string getName()
	{
		return "Circular";
	}

	public virtual void init()
	{
	}

	public virtual float tick(bool onCd)
	{
		if (onCd)
			return 0;
		if (createBalls (0, Mathf.PI * 2))
			return 2;
		else
			return 0;
	}

	protected virtual bool createBalls(float radStart, float radEnd)
	{
		if (Input.GetKeyDown(KeyCode.F))
		{
			for(int i = 0; i < numberOfBalls; i++)
			{
				if (firstBall == null)
					break;

				GameObject nextBall = firstBall.GetComponent<EchoBallConstructor>().otherNeighbor;
				Destroy(firstBall);
				firstBall = nextBall;
			}

			Transform parent = GetComponent<Transform>();
			Quaternion none = new Quaternion();
			GameObject previousBall = null;

			for (int i = 0; i < numberOfBalls; i++)
			{
				float rad = radStart + (radEnd - radStart) * i / numberOfBalls;
				Vector2 offset = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
				GameObject theBall = (GameObject)Instantiate(ball, parent.position, none);
				Rigidbody2D rgbd = theBall.GetComponent<Rigidbody2D>();
				if (i == 0) {
					firstBall = theBall;
				}
				else
				{
					if (i == numberOfBalls - 1)
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
			return true;
		}
		return false;
	}

	public virtual void switchFrom()
	{
		for(int i = 0; i < numberOfBalls; i++)
		{
			if (firstBall == null)
				break;
			
			GameObject nextBall = firstBall.GetComponent<EchoBallConstructor>().otherNeighbor;
			Destroy(firstBall);
			firstBall = nextBall;
		}
			
	}

	public virtual void switchTo()
	{
	}	
}
