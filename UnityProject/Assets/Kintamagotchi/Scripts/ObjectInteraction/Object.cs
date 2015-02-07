using UnityEngine;
using System.Collections;

public class Object : MonoBehaviour 
{
    protected enum eObjectType
    {
        _NO_TYPE_ = 0,
        _BED_,
        _TV_
    };

    protected eObjectType _pType;
    protected Monster _monsterScript;

    public Transform defaultMonsterMosition;

	// Use this for initialization
	protected void Start () 
    {
        _pType = eObjectType._NO_TYPE_;
        _monsterScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Monster>();
	}

    public virtual void OnTapped()
    {
        Debug.Log(_pType);
        _monsterScript.MoveTo(defaultMonsterMosition.position);
    }
}
