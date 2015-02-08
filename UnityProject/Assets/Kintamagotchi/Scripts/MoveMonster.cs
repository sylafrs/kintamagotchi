using UnityEngine;
using System.Collections;

public class MoveMonster : MonoBehaviour 
{
	void OnTapped(Vector3 point)
	{
		Monster.instance.MoveTo(point);
	}
}
