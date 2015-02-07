using UnityEngine;
using System.Collections;

public class Tv : Object 
{
	// Use this for initialization
	void Start () 
    {
        base.Start();
        _pType = eObjectType._TV_;
	}

    public override void OnTapped()
    {
 	    base.OnTapped();
    }
}
