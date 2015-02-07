using UnityEngine;
using System.Collections.Generic;

public static class ArrayUtils 
{
	public static bool Contains<T>(this IList<T> array, T item)
	{
		foreach(T o in array)
		{
			if(o.Equals(item))
				return true;
		}
		
		return false;
	}	
}
