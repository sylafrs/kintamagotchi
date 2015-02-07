﻿using UnityEngine;
using System.Collections;

public class Speaker : cObject 
{
	// Use this for initialization
	void Start()
	{
		base.Start();
		_pType = eObjectType._SPEAKER_;
	}
	
	public override void OnTapped()
	{
		if (!hasActivatedObject)
			return;
		base.OnTapped();
	}
}