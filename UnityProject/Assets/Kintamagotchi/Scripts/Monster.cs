using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Monster : MonoBehaviour
{
	private Transform target;
	private PlayMakerFSM __fsm;
	public List<AudioClip>	clipList;
	private AudioSource	__actualSound;
	private AudioClip __nextSound;
	public float	fadeSpeed = 0.1f;

	public static Monster instance { get; private set; }

	public float	defaultVolume = 1f;

	void Awake()
	{
		instance = this;
		this.target = GameObject.Find("Target").transform;
	}

	void Start()
	{
		// Ca fait un peu doublon mais bon..
		InteractionManager.OnInteraction += OnInteraction;

		target.position = this.transform.position;
		__fsm = GetComponent<PlayMakerFSM>();
		__actualSound = GetComponent<AudioSource>();
		__nextSound = null;
	}

	void	Update()
	{
		playSound();
	}

	public void OnTapped()
	{
		//this.transform.FindChild("Cube").renderer.material.color = Utils.RandomColor();
		__nextSound = clipList[0];
	}

	public void OnMoved(Vector3 pPosition)
	{
		target.position = pPosition;
		transform.position = pPosition;
		if (__fsm)
			__fsm.Fsm.Event("selected");
		if (Random.Range(0, 30) == 15)
			__nextSound = clipList[1];
	}

	public void MoveTo(Vector3 pPosition)
	{
		target.position = pPosition;
		if (__fsm)
			__fsm.Fsm.Event("startsMoving");
		__nextSound = clipList[2];
	}

	public void	OnObjectDropped()
	{
		__nextSound = clipList[Random.Range(3, 4)];
	}

	public void	OnLvlUp()
	{
		__nextSound = clipList[5];
	}

	public void	OnIsHappy()
	{
		__nextSound = clipList[6];
	}

	public void	OnHappyBecauseNewMeubleAdded()
	{
		__nextSound = clipList[6];
	}

	public void	OnNeutral()
	{
		__nextSound = clipList[7];
	}

	public void	OnSad()
	{
		__nextSound = clipList[8];
	}

	private void OnInteraction(InteractionType obj)
	{
		GameData.Get.Data.MoralLastInteraction = GameData.Get.Data.Moral;
	}

	#region Sound

	bool	fade()
	{
		__actualSound.volume -= Time.deltaTime * fadeSpeed;
		if (__actualSound.volume <= 0.0f)
		{
			__actualSound.Stop();
			return true;
		}
		return false;
	}

	void	playSound()
	{
		if (__actualSound.isPlaying)
			fade();
		if (__actualSound.volume <= 0.0f || !__actualSound.isPlaying)
		{
			__actualSound.volume = defaultVolume;
			__actualSound.clip = __nextSound;
			__nextSound = null;
			__actualSound.Play();
		}
	}

	#endregion
}
