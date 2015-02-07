using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour 
{
    public Transform target;

    void Start()
    {
        target.position = this.transform.position;
    }

	public void OnTapped()
	{
		this.transform.FindChild("Cube").renderer.material.color = Utils.RandomColor();
	}

    public void OnMoved(Vector3 pPosition)
    {
        transform.position = pPosition;
    }

	public void MoveTo(Vector3 pPosition)
	{
        target.position = pPosition;
	}
}
