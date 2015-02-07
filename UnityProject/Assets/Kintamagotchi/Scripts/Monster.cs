using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour
{
	public Transform target;

	void Start()
	{
		// Ca fait un peu doublon mais bon..
		InteractionManager.OnInteraction += OnInteraction;

		target.position = this.transform.position;
	}

	public void OnTapped()
	{
		//this.transform.FindChild("Cube").renderer.material.color = Utils.RandomColor();
	}

	public void OnMoved(Vector3 pPosition)
	{
		target.position = pPosition;
		transform.position = pPosition;
	}

	public void MoveTo(Vector3 pPosition)
	{
		target.position = pPosition;
	}

	private void OnInteraction(InteractionType obj)
	{
		GameData.Get.Data.MoralLastInteraction =
			GameData.Get.Data.Moral;
	}
}
