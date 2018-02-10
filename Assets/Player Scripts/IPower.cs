using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPower 
{
	void init();
	float tick(bool onCd);
	void switchTo();
	void switchFrom();
	string getName();
}
