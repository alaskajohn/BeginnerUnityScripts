//John Pile Jr, 2013 | www.johnpile.com
//For use in Unity3D, tested with version 4.x
//Demonstration of simple message queue

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class cMessage
{
	public float m_exitTime;
	public string m_message;

	public cMessage(string p_msg, float p_exitTime)
	{
		m_message = p_msg;
		m_exitTime = p_exitTime;
	}
}

public class messageQueue : MonoBehaviour 
{
	private static List<cMessage> m_queue = new List<cMessage>();
	private bool m_enabled = true;

	public Vector2 queueLocation = new Vector2(25, 25); //in pixels
	public Vector2 messageSize = new Vector2(200, 25);  //in pixels

	public static void addMessage(string pMessage, float pDuration)
	{
		cMessage tempMessage = new cMessage(pMessage, Time.time + pDuration);
		m_queue.Add(tempMessage);
	}

	void Update () 
	{
		for (int i = m_queue.Count - 1; i>=0; i--)
		{
			if (Time.time > m_queue[i].m_exitTime)
				m_queue.RemoveAt(i);
		}

		if (Input.GetButtonDown("Fire2"))
		    m_enabled = !m_enabled;
	}

	void OnGUI()
	{
		if (!m_enabled) return;
		float yPos = queueLocation.y;
		foreach (cMessage m in m_queue)
		{
			GUI.Label(new Rect(queueLocation.x, yPos, messageSize.x, messageSize.y), m.m_message);
			yPos+=messageSize.y;
		}
	}
}
