using UnityEngine;
using System.Collections;

public class ArmChair : cObject 
{
	// Use this for initialization
	void Start()
	{
		base.Start();
		_pType = eObjectType._ARMCHAIR_;

	}
	
	public override void OnTapped()
	{		
		if (!hasActivatedObject)
			return;
		base.OnTapped();
	}
}
