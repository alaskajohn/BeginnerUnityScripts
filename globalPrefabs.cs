//John Pile Jr, 2014 | www.johnpile.com
//For use in Unity3D, tested with version 4.x
//Demonstration of global prefabs
//Note requires messageQueue: https://github.com/alaskajohn/BeginnerUnityScripts/blob/master/messageQueue.cs
//Instructions for use: http://prof.johnpile.com/2014/07/20/globalprefabs

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class globalPrefabs : ScriptableObject
{
	public static Dictionary<int, Object> objectList = new Dictionary<int, Object>();
	private static Object emptyObj;

	public static Object getPrefab(string objName)
	{
		Object obj;

		if (objectList.TryGetValue(objName.GetHashCode(), out obj))
			return obj;
		else 
		{
			messageQueue.addMessage ("Object not found: " + objName, 3);
			return (emptyObj);
		}
	}
	
	public static void LoadAll(string pPath) 
	{
		Object[] ObjectArray = Resources.LoadAll(pPath);
		
		foreach (Object o in ObjectArray) 
		{
			messageQueue.addMessage ("Adding asset: " + o.name,3);
			objectList.Add (o.name.GetHashCode(), (Object)o);
		}

		CreateEmptyPrefab();
	}

	static void CreateEmptyPrefab()
	{
		string emptyObjectName = "empty_object_01";
		objectList.TryGetValue(emptyObjectName.GetHashCode(), out emptyObj);

#if UNITY_EDITOR
		//When running in the editor, this first time this is run it will create a default hot-pink cube as a prefab
		//At future executions, it will use the same prefab (whether in the editor or not)
		if (emptyObj != null)
		{
			if (emptyObj.name.GetHashCode() == emptyObjectName.GetHashCode()) return;
		}
		else 
		{
			emptyObj = PrefabUtility.CreateEmptyPrefab("Assets/Resources/Prefabs/"+emptyObjectName+".prefab");

			GameObject tempObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
			tempObj.AddComponent<Rigidbody>();
			tempObj.name = emptyObjectName;
			tempObj.renderer.material.color = new Color(1.0f, 0.0f, 0.5f);

			PrefabUtility.ReplacePrefab(tempObj, emptyObj, ReplacePrefabOptions.ConnectToPrefab);	
		}
#endif

	}
	
}
