using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// Evenement générique
/// </summary>
public abstract class TimeEvent : MonoBehaviour 
{
	public List<AudioClip> clipList;
	protected Queue<AudioClip> toListen = new Queue<AudioClip>();
	public float fadeSpeed;

	/// <summary>
	/// Nom de l'event
	/// </summary>
	public string Name;

	/// <summary>
	/// Combien de fois doit-on lancer le check d'event ?
	/// </summary>
	/// <param name="dt">Temps passé depuis le dernier check</param>
	/// <returns>Le nombre de checks à faire</returns>
	public abstract int MustCheck(TimeSpan dt);

	/// <summary>
	/// Chance (ratio 0 - 1) pour lancer l'event
	/// </summary>
	public abstract float Chance { get; }

	/// <summary>
	/// Lance l'event
	/// </summary>
	public abstract void Launch();

	/// <summary>
	/// A quand correspondrait le dernier check ?
	/// </summary>
	/// <param name="i">Itération de check depuis last</param>
	/// <param name="last">Dernier check testé</param>
	/// <returns>Le temps depuis le dernier check</returns>
	public abstract DateTime GetLastCheck(int i, DateTime lastCheck);

	#region Sound

	protected bool fade()
	{
		audio.volume -= Time.deltaTime * fadeSpeed;
		if (audio.volume <= 0.0f)
		{
			audio.Stop();
			return true;
		}
		return false;
	}

	protected void playSound()
	{
		if (audio.loop == false && audio.isPlaying)
		{
			fade();
		}

		if(audio.loop == true && toListen.Count > 0)
		{
			AudioClip c = toListen.Dequeue();
			audio.volume = 0.1f;
			audio.clip = c;
			this.OnAudioChanged(c);
			audio.Play();
		}

		if (audio.volume <= 0.0f || !audio.isPlaying)
		{
			if (toListen.Count > 0)
			{
				AudioClip c = toListen.Dequeue();
				audio.volume = 0.1f;
				audio.clip = c;
				this.OnAudioChanged(c);
				audio.Play();
			}
		}
	}

	protected virtual void OnAudioChanged(AudioClip audioClip)
	{

	}

	private void Update()
	{
		this.playSound();
	}

	#endregion
}
