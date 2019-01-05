using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWalk : MonoBehaviour {

	public float enemySpeed;
	public float enemyCurrentSpeed;
	public bool facingRight;
	

	public GameObject spriteObject;

	Animator animator;
	NavMeshAgent navMesh;
	EnemySight enemySight;
	
	// Use this for initialization
	void Awake () {
		navMesh = GetComponent<NavMeshAgent>();
		enemySight = GetComponent<EnemySight>();
		animator = spriteObject.GetComponent<Animator>();

		navMesh.speed = enemySpeed;
	}
	
	// Update is called once per frame
	void Update () {
		if (enemySight.playerInSight == true)
		{
			navMesh.SetDestination(enemySight.target.transform.position);
			navMesh.updateRotation = false;
			animator.SetBool("Walk", true);

			if (enemySight.targetDistance < 0.5f)
			{
				animator.SetBool("Walk", false);
			}

			if (enemySight.playerOnRight == true && facingRight)
			{
				Flip();
			}
			else if (enemySight.playerOnRight == false && !facingRight)
			{
				Flip();
			}

		}

		
	}

	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
