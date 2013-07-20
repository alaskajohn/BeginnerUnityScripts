//John Pile Jr, 2013
//For Use in Unity3D, tested with version 4.x
//Demonstration of simple Message Queue
//Requires pileMessageQueue Script

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class myMessage
{
	public float m_exitTime;
	public string m_message;
}

public class MessageQueue : MonoBehaviour 
{
	
	private static List<myMessage> theQueue;
	
		void Start () 
	{	
		theQueue = new List<myMessage>();
	}
	
	public static void addMessage(string pMessage, float pDuration)
	{
		myMessage tempMessage = new myMessage();
		tempMessage.m_message = pMessage;
		tempMessage.m_exitTime = Time.time + pDuration;
		theQueue.Add(tempMessage);
	}
	
		void Update () 
	{
		for (int i = theQueue.Count - 1; i>=0; i--)
		{
			if (Time.time > theQueue[i].m_exitTime)
				theQueue.RemoveAt(i);
		}
	}
	
	void OnGUI()
	{
		int yPos=25;
		
		GUI.Label(new Rect(25, yPos, 200, 25), "Message Queue:");
		yPos+=25;
		
		foreach (myMessage m in theQueue)
		{
			GUI.Label(new Rect(25, yPos, 200, 25), m.m_message);
			yPos+=25;
		}
	}
}
