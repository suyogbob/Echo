using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * IPower instance for the echo power.
 * Manages the power action of creating the little
 * particle balls (they manage their own behavior once created).
 * Note: Ray power inherits from this so some behavior is generalized
 */
public class Echo : MonoBehaviour, IPower 
{
	/* CONFIGURATIONS */

	//prefab reference to spawn the balls from
	public Object ball;
	//how many balls to spawn
	public int numberOfBalls;
	//how fast the balls go
	public float velocityScalar;

	/* INTERNAL VARIABLES */

	//reference to the first ball in the list (for destruction purposes)
	protected GameObject firstBall;

	//power name
	public virtual string getName()
	{
		return "Circular";
	}

	//power setup
	public virtual void init()
	{
	}

	//process a single frame's elapsed time
	public virtual float tick(bool onCd)
	{
		if (onCd)
			return 0; //no cooldown if already on cooldown
		
		if (createBalls (0, Mathf.PI * 2)) //try to spawn new balls
			return 2; //2s cooldown
		else
			return 0; //if fail, no cooldown
	}

	//internal method for doing the actual ball spawning
	//inputs: arc begin and end points
	protected virtual bool createBalls(float radStart, float radEnd)
	{
		//detect keypress
		if (Input.GetKeyDown(KeyCode.F))
		{
			//the balls implement a doubly linked list
			//so we an remove them by looping through the
			//.otherNeighbor field until they are all gone
			//note: if you call Destroy() on something,
			//even though the object still exists, 
			//if you compare using ==null it will come out to true.
			//this is because of some C# magic in the core of Unity.
			for(int i = 0; i < numberOfBalls; i++)
			{
				if (firstBall == null)
					break;

				GameObject nextBall = firstBall.GetComponent<EchoBallConstructor>().otherNeighbor;
				Destroy(firstBall);
				firstBall = nextBall;
			}

			//parameters for ball spawning
			Transform parent = GetComponent<Transform>(); //parent is the player
			Quaternion none = new Quaternion();
			GameObject previousBall = null;

			//spawn balls
			for (int i = 0; i < numberOfBalls; i++)
			{
				//get angle from horizontal
				float rad = radStart + (radEnd - radStart) * i / numberOfBalls;
				//get the offset vector from the player
				Vector2 offset = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
				//spawn the ball
				GameObject theBall = (GameObject)Instantiate(ball, parent.position, none);
				//get the ball's rigidbody
				Rigidbody2D rgbd = theBall.GetComponent<Rigidbody2D>();
				if (i == 0) {
					//handle i=0 edgecase
					firstBall = theBall;
				}
				else
				{
					//handle i=n-1 edgecase
					if (i == numberOfBalls - 1)
					{
						firstBall.GetComponent<EchoBallConstructor>().neighbor = theBall;
						theBall.GetComponent<EchoBallConstructor>().otherNeighbor = firstBall;
					}
					//linked list
					theBall.GetComponent<EchoBallConstructor>().neighbor = previousBall;
					previousBall.GetComponent<EchoBallConstructor>().otherNeighbor = theBall;
				}
				//give the ball speed
				rgbd.velocity = offset*velocityScalar;
				//update previous pointer for list purposes
				previousBall = theBall;
			}
			return true;
		}
		return false;
	}

	//remove balls when power is deselected
	public virtual void switchFrom()
	{
		//re-implement the list traversal to clear balls
		for(int i = 0; i < numberOfBalls; i++)
		{
			if (firstBall == null)
				break;
			
			GameObject nextBall = firstBall.GetComponent<EchoBallConstructor>().otherNeighbor;
			Destroy(firstBall);
			firstBall = nextBall;
		}
			
	}

	//power selection initialization
	public virtual void switchTo()
	{
	}	
}
