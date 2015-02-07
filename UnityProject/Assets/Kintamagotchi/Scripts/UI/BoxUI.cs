//******************************************************************************
// Authors: Frederic SETTAMA  
//******************************************************************************

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//******************************************************************************
public class BoxUI : MonoBehaviour 
{
#region Script Parameters
	public Text		Message;
#endregion

#region Properties
#endregion

#region Fields
	// Const -------------------------------------------------------------------

	// Private -----------------------------------------------------------------
#endregion

#region Methods

	public void SetTextAndShow(string text)
	{
		Message.text = text;
		this.gameObject.SetActive(true);
	}

	public void Validate()
	{
		MenuManager.Get.ValidatedBuy();
		this.gameObject.SetActive(false);
	}

	public void Cancel()
	{
		this.gameObject.SetActive(false);
	}
#endregion

#region Implementation
#endregion
}
