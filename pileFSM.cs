using UnityEngine;
using System.Collections;

//John Pile Jr, 2013
//For Use in Unity3D, tested with version 4.x
//Demonstration of simple Finite State Machine (apply to a game object and link to player's transform)
//Requires pileMessageQueue Script

public class FiniteStateMachine : MonoBehaviour {

	public enum EnemyStates
	{
		sleeping = 0,
		following = 1,
		retreating = 3,
	}
	
	public Transform player;
	public float followSpeed = 4.0f;
	public float retreatSpeed = 4.0f;
	public float acceleration = 0.5f;

	public string enemyName = "Unamed";

	public EnemyStates currentState = EnemyStates.sleeping;
	
	// Use this for initialization
	void Start () 
	{	
		followSpeed = 8.0f;
		retreatSpeed = 8.0f;
		acceleration = 1.0f;
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		Vector3 distance = player.position - transform.position;			
		
		switch(currentState)
		{
		case (EnemyStates.sleeping):
			//Logic for when sleeping
			rigidbody.velocity *= 0;
			
			
			if (distance.magnitude < 20)
			{
				currentState = EnemyStates.following;
				MessageQueue.addMessage(enemyName + " is following you.", 3);
			}
			
			break;
		case (EnemyStates.following):
			//Logic for when following
			
			transform.LookAt(player);
			
			if (rigidbody.velocity.magnitude < followSpeed)
				rigidbody.velocity = (transform.forward * acceleration);
			
			
			if (distance.magnitude < 5)
			{
				currentState = EnemyStates.retreating;
				MessageQueue.addMessage(enemyName + " is running away.", 3);
			}
			
			break;
		case (EnemyStates.retreating):
			//Logic for when running away
			
			transform.LookAt(player); //Move Backwards
		
			if (rigidbody.velocity.magnitude < retreatSpeed)
				rigidbody.velocity -= (transform.forward * acceleration);


			if (distance.magnitude > 25)
			{
				currentState = EnemyStates.sleeping;
				MessageQueue.addMessage(enemyName + " is sleeping.", 3);
			}
			
			break;
		}
			
	}
}
