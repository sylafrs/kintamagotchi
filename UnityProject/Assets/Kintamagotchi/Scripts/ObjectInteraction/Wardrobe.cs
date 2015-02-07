using UnityEngine;
using System.Collections;

public class Wardrobe : cObject
{
	// Use this for initialization
	void Start()
	{
		base.Start();
		_pType = eObjectType._WARDROBE_;
	}
	
	public override void OnTapped()
	{
		if (!hasActivatedObject)
			return;
		base.OnTapped();
	}
}
