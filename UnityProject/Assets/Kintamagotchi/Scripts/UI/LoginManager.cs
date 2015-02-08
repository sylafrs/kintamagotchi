//******************************************************************************
// Authors: Frederic SETTAMA  
//******************************************************************************

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//******************************************************************************
public class LoginManager : MonoBehaviour 
{
#region Script Parameters
	public InputField		Num;
	public InputField		Mdp;
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
#endregion

#region Methods
	public void AddNumero(int num)
	{
		if (Mdp.text.Length < 6)
			Mdp.text = Mdp.text + num.ToString();
	}

	public void Erase()
	{
		Mdp.text = "";
	}

	public void Login()
	{
		Application.LoadLevel("main");
	}

	public void Quit()
	{
		Application.Quit();
	}
#endregion

#region Implementation
#endregion
}
