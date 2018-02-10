using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPower 
{
	void init();
	void tick();
	void switchTo();
	void switchFrom();
	string getName();
}
