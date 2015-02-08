﻿//******************************************************************************
// Author: Frederic SETTAMA
//******************************************************************************

using UnityEngine;
using System.Collections.Generic;

public enum FX
{
	Diamonds,
	NewChester,
	UseItem,
	LevelUP,
	Sickness,
	TouchedChester,
	Fire,
	Xp,
	Moral
}

public class FxManager : MonoBehaviour
{
#region Static
	private static FxManager mInstance;
	public static FxManager Get { get{ return mInstance; } }
#endregion
	
#region Script Parameters
	public List<GameObject>	FX;
#endregion
	
#region Unity Methods
	void Awake()
	{
		if(mInstance != null && mInstance != this) 
		{
			UnityEngine.Debug.Log("FxManager - we were instantiating a second copy of TextManager, so destroying this instance");
			DestroyImmediate(this.gameObject, true);
			return;
		}
		
		// Keep this object alive for the duration of the game
		DontDestroyOnLoad(this);
		mInstance = this;
	}
#endregion
	
#region Methods
	public void Play(FX fx, Transform target)
	{
		var instance = Instantiate(FX[(int)fx]) as GameObject;
		instance.transform.parent = target;
		instance.transform.localPosition = Vector3.zero;
		instance.transform.localRotation = Quaternion.identity;
		instance.SetActive(true);
	}

	public void Play(FX fx, Component target)
	{
		Play(fx, target.transform);
	}

	public GameObject Play(FX fx, Vector3 position, Quaternion rotation)
	{
		var instance = Instantiate(FX[(int)fx]) as GameObject;
		instance.transform.parent = transform.parent;
		instance.transform.localPosition = position;
		instance.transform.localRotation = rotation;
		instance.SetActive(true);
		return instance;
	}
#endregion
}
