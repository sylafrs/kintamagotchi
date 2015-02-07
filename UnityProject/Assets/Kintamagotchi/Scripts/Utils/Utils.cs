using UnityEngine;
using System.Security.Cryptography;
using System.Collections;
using System.Text;

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

	// Encrypt the specified plainText using MD5 hash.
	public static string Encrypt(string plainText)
	{
		// Calculate MD5 hash from input
		MD5.Create();
		MD5 md5 = MD5.Create();
		byte[] inputBytes = Encoding.ASCII.GetBytes(plainText);
		byte[] hash = md5.ComputeHash(inputBytes);

		// Convert byte array to hex string
		var stringBuilder = new StringBuilder();
		for (int i = 0; i < hash.Length; i++)
		{
			stringBuilder.Append(hash[i].ToString("X2"));
		}

		return stringBuilder.ToString();
	}
}
