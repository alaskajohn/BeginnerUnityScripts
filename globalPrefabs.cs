//John Pile Jr, 2014 | www.johnpile.com
//For use in Unity3D, tested with version 4.x
//Demonstration of global prefabs
// http://prof.johnpile.com/2014/07/20/globalprefabs

using UnityEngine;
using System.Collections.Generic;

public class globalPrefabs : ScriptableObject
{
	public static Dictionary<int, GameObject> objectList = new Dictionary<int, GameObject>();

	public static GameObject getPrefab(string objName)
	{
		GameObject obj = objectList[ objName.GetHashCode() ];

		if (obj)
			return obj;
		else 
		{
			Debug.Log("Object not found");
			return null;
			// Better yet, create a default game object (like a hot pink cube) 
			// to return when this fails
		}
	}

	public static void LoadAll(string pPath) 
	{
		Object[] ObjectArray = Resources.LoadAll(pPath);

		foreach (Object o in ObjectArray) 
		{
			messageQueue.addMessage ("Adding asset: " + o.name,3);
			objectList.Add (o.name.GetHashCode(), (GameObject)o);
		}
	}

}
