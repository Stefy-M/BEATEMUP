using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour {

	public bool playerInSight;
	public float targetDistance;

	public bool playerOnRight;
	public Vector3 playerRelativePosition;

	public GameObject player;
	public GameObject target;
	// Use this for initialization
	void Awake () {
		player = GameObject.FindGameObjectWithTag("Player");
		
		
	}
	
	// Update is called once per frame
	void Update () {
		target = player;
		targetDistance = Vector3.Distance(target.transform.position, gameObject.transform.position);
		playerRelativePosition = player.transform.position - gameObject.transform.position;

		if (playerRelativePosition.x < 0)
		{
			playerOnRight = false;
		}
		else if (playerRelativePosition.x > 0)
		{
			playerOnRight = true;
		}
	}

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject == player)
		{
			playerInSight = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject == player)
		{
			playerInSight = false;
		}
	}
}
