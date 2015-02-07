using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour 
{
	public float speed;
	private static Vector3 targetPosition;

	public void OnInteraction()
	{
		this.transform.FindChild("Cube").renderer.material.color = Utils.RandomColor();
	}

	public static void MoveTo(Vector3 position)
	{
		targetPosition = position;
	}

	public void Update()
	{
		if(Mathf.Abs(Vector3.Distance(targetPosition, this.transform.position)) > 0.01f)
		{
			this.transform.position += Time.deltaTime * (targetPosition - this.transform.position) * speed;
		}
	}
}
