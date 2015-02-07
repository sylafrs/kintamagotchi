using UnityEngine;
using System.Collections;

public static class Utils
{

	#region Color

	public static Color RandomColor()
	{
		return RandomColor(Color.black, Color.white);
	}

	public static Color RandomColor(Color from, Color to)
	{
		float r = Random.Range(from.r, to.r);
		float g = Random.Range(from.g, to.b);
		float b = Random.Range(from.b, to.b);
		float a = Random.Range(from.a, to.a);

		return new Color(r, g, b, a);
	}

	#endregion

}
