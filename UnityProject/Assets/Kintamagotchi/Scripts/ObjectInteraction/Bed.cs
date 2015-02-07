using UnityEngine;
using System.Collections;

public class Bed : Object 
{
    // Use this for initialization
    void Start()
    {
        base.Start();
        _pType = eObjectType._BED_;
    }

    public override void OnInteraction()
    {
        base.OnInteraction();
    }
}
