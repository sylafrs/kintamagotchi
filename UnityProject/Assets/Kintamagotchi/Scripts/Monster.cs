using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour
{
	private Transform target;
	private PlayMakerFSM __fsm;

	void Awake()
	{
		this.target = GameObject.Find("Target").transform;
	}

	void Start()
	{
		// Ca fait un peu doublon mais bon..
		InteractionManager.OnInteraction += OnInteraction;

		target.position = this.transform.position;
		__fsm = GetComponent<PlayMakerFSM>();
	}

	public void OnTapped()
	{
		//this.transform.FindChild("Cube").renderer.material.color = Utils.RandomColor();
	}

	public void OnMoved(Vector3 pPosition)
	{
		target.position = pPosition;
		transform.position = pPosition;

		if (__fsm)
			__fsm.Fsm.Event("selected");
	}

	public void MoveTo(Vector3 pPosition)
	{
		target.position = pPosition;
		if (__fsm)
			__fsm.Fsm.Event("startsMoving");
	}

	private void OnInteraction(InteractionType obj)
	{
		GameData.Get.Data.MoralLastInteraction =
			GameData.Get.Data.Moral;
	}
}
