using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Monster : MonoBehaviour
{
	private Transform target;
	private PlayMakerFSM __fsm;
	public List<AudioClip>	clipList;
	private AudioSource	__actualSound;

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
		__actualSound = GetComponent<AudioSource>();
	}

	public void OnTapped()
	{
		//this.transform.FindChild("Cube").renderer.material.color = Utils.RandomColor();
		playSound(clipList[0]);
	}

	public void OnMoved(Vector3 pPosition)
	{
		target.position = pPosition;
		transform.position = pPosition;
		if (__fsm)
			__fsm.Fsm.Event("selected");
		playSound(clipList[1]);
	}

	public void MoveTo(Vector3 pPosition)
	{
		target.position = pPosition;
		if (__fsm)
			__fsm.Fsm.Event("startsMoving");
		playSound(clipList[2]);
	}

	public void	OnObjectDropped()
	{
		playSound(clipList[Random.Range(3, 4)]);
	}

	public void	OnLvlUp()
	{
		playSound(clipList[5]);
	}

	public void	OnIsHappy()
	{
		playSound(clipList[6]);
	}

	public void	OnHappyBecauseNewMeubleAdded()
	{
		playSound(clipList[6]);
	}

	public void	OnNeutral()
	{
		playSound(clipList[7]);
	}

	public void	OnSad()
	{
		playSound(clipList[8]);
	}

	private void OnInteraction(InteractionType obj)
	{
		GameData.Get.Data.MoralLastInteraction = GameData.Get.Data.Moral;
	}

	#region Sound

	bool	fade()
	{
		return true;
	}

	void	playSound(AudioClip pClip)
	{
		if (__actualSound.isPlaying)
			__actualSound.Stop ();
		__actualSound.clip = pClip;
		__actualSound.Play();
	}

	#endregion
}
