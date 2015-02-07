using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour 
{
    public Transform target;

    void Start()
    {
        target.position = this.transform.position;
    }

	public void OnInteraction()
	{
		this.transform.FindChild("Cube").renderer.material.color = Utils.RandomColor();
	}

	public void MoveTo(Vector3 position)
	{
        target.position = position;
	}
}
