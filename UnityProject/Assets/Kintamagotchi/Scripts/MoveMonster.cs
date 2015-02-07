using UnityEngine;
using System.Collections;

public class MoveMonster : MonoBehaviour {

	void OnInteraction(Vector3 point)
	{
		Monster.MoveTo(point);
	}
}
