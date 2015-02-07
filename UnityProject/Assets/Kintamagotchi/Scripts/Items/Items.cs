//******************************************************************************
// Authors: Frederic SETTAMA  
//******************************************************************************

using UnityEngine;
using System.Collections;

//******************************************************************************
public class Items : MonoBehaviour 
{
#region Script Parameters
	public ItemDesc		ItemDesc;
#endregion

#region Static
#endregion

#region Properties
#endregion

#region Fields
	// Const -------------------------------------------------------------------

	// Private -----------------------------------------------------------------
#endregion

#region Unity Methods
	void Start () 
	{
	}
	
	void Update () 
	{
	}
#endregion

#region Methods
	public bool Use()
	{
		Debug.Log(ItemDesc.Name);
		return true;
	}
#endregion

#region Implementation
#endregion
}
