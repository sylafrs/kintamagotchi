using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour
{
	private Transform target;
	private PlayMakerFSM[] __fsm;

	void Awake()
	{
		this.target = GameObject.Find("Target").transform;
	}

	void Start()
	{
		// Ca fait un peu doublon mais bon..
		InteractionManager.OnInteraction += OnInteraction;

		target.position = this.transform.position;
		__fsm = GetComponents<PlayMakerFSM>();
	}

	public void OnTapped()
	{
		//this.transform.FindChild("Cube").renderer.material.color = Utils.RandomColor();
		__fsm[1].Fsm.Event("sfx_raton_cnt_01_event");
	}

	public void OnMoved(Vector3 pPosition)
	{
		target.position = pPosition;
		transform.position = pPosition;

		if (__fsm[0])
			__fsm[0].Fsm.Event("selected");
		if (__fsm[1])
			__fsm[1].Fsm.Event("sfx_raton_cnt_02_event");
	}

	public void MoveTo(Vector3 pPosition)
	{
		target.position = pPosition;
		if (__fsm[0])
			__fsm[0].Fsm.Event("startsMoving");
	}

	private void OnInteraction(InteractionType obj)
	{
		GameData.Get.Data.MoralLastInteraction =
			GameData.Get.Data.Moral;
	}
}
