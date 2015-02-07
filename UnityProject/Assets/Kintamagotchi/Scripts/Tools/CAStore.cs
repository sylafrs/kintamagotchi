using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

public class CAStore : MonoBehaviour 
{
#if USE_OAUTH
	public static System.Random random = new System.Random();
	
	public string oauth_consumer_key;
	public string oauth_consumer_secret;
	public string oauth_signature;
	public string oauth_signature_method;
	public string oauth_version;
	public string oauth_callback;
	public string oauth_request_token_uri;

	IEnumerator Start () 
	{
		string authorization = this.GetOAuth();
		Debug.Log(authorization);

		var headers = new Dictionary<string, string>();
		headers.Add("Authorization", authorization);

		WWW www = new WWW(oauth_request_token_uri, null, headers);

		yield return www;

		if (www.error != null)
			Error(www.error);
		else
			Answer(www.text);
	}

	private long GetTimeStamp()
	{
		DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		DateTime now = DateTime.Now;
		TimeSpan timestamp = now - epoch;
		return (long)timestamp.TotalSeconds;
	}

	public string md5_format_random()
	{
		// Random :D
		byte[] inputBytes = new byte[8];
		random.NextBytes(inputBytes);

		// step 1, calculate MD5 hash from input
		MD5 md5 = System.Security.Cryptography.MD5.Create();
		byte[] hash = md5.ComputeHash(inputBytes);

		// step 2, convert byte array to hex string
		StringBuilder sb = new StringBuilder();
		for (int i = 0; i < hash.Length; i++)
		{
			if (i == 4 || i == 6 || i == 8 || i == 10)
				sb.Append('-');
			sb.Append(hash[i].ToString("x2"));
		}
		return sb.ToString();
	}

	private string GetOAuth()
	{
		Hashtable data = new Hashtable();
		//data.Add("realm",					"")
		data.Add("oauth_consumer_key",		oauth_consumer_key);
		//data.Add("oauth_token",			"");
		data.Add("oauth_signature_method",	oauth_signature_method);
		data.Add("oauth_signature",			oauth_signature);
		data.Add("oauth_timestamp",			this.GetTimeStamp());
		data.Add("oauth_nonce",				md5_format_random());
		data.Add("oauth_version",			oauth_version);
		data.Add("oauth_callback",			oauth_callback);

		string str = "OAuth";
		bool first = true;
		foreach(DictionaryEntry d in data)
		{
			str += (first ? " " : ",\r\n");
			str += d.Key.ToString() + "=\"" + d.Value.ToString() + "\"";
			first = false;
		}

		return str;
	}

	private void Error(string str)
	{
		Debug.LogError(str);
	}

	private void Answer(string str)
	{
		Debug.Log(str);
	}
#endif
}
