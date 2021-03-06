﻿//******************************************************************************
// Authors: Frederic SETTAMA  
//******************************************************************************

using UnityEngine;
using System.Collections;

//******************************************************************************
public class SplashScreen : MonoBehaviour 
{
#region Script Parameters
	public float		Delay = 2;
	public GameObject	LoginPanel;
#endregion

#region Static
#endregion

#region Properties
#endregion

#region Fields
	// Const -------------------------------------------------------------------

	// Private -----------------------------------------------------------------
	private AsyncOperation	mAsync;
	private float			mTime = 0;
#endregion

#region Unity Methods
	void Start() 
	{
		
	}

	void Update () 
	{
		mTime += Time.deltaTime;
		if (mTime > Delay)
			LoginPanel.SetActive(true);
	}
#endregion
}
