using UnityEngine;
using System;

/// <summary>
/// Evenement générique
/// </summary>
public abstract class TimeEvent : MonoBehaviour 
{
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
}
