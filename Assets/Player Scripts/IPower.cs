using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Defines the interface for powers.
 * All power instances must implement these functions
 */
public interface IPower 
{
	//power initial setup
	void init();
	//power tick processing (onCd = whether the power is on cooldown) (return = new cooldown to add)
	float tick(bool onCd);
	//setup the power when it is selected
	void switchTo();
	//when the power is deselected, turn it off
	void switchFrom();
	//power display name
	string getName();
}
