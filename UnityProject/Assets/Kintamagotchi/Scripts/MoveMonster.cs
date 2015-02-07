using UnityEngine;
using System.Collections;

public class MoveMonster : MonoBehaviour 
{
    private Monster __monsterScript;

    void    Start()
    {
        __monsterScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Monster>();
    }

	void OnInteraction(Vector3 point)
	{
        __monsterScript.MoveTo(point);
	}
}
