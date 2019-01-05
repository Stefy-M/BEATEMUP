using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[Range(0f, 10f)]
	public float moveSpeed;
	public float knockBackForce;
	public bool isBlocking;
	public bool canMove;
	public float attackMovementSpeed;
	public float knockedDownTime;
	private float walkMoveSpeed;
	public float xMin, xMax, zMin, zMax;
	public GameObject attack1Box, attack2Box, attack3Box;

	public Sprite attack1SpriteHitFrame, attack2SpriteHitFrame, attack3SpriteHitFrame;
	SpriteRenderer currentSprite;


	private Rigidbody rb;
	private Animator animator;
	
	private AnimatorStateInfo curStateInfo;
	private bool facingRight;




	//Used to record which animator state currently in
	static int currentState;
	static int idleState = Animator.StringToHash("Base Layer.Idle");
	static int walkState = Animator.StringToHash("Base Layer.Walk");
	static int jumpState = Animator.StringToHash("Base Layer.Jump");
	static int tauntState = Animator.StringToHash("Base Layer.Taunt");
	static int hurtState = Animator.StringToHash("Base Layer.Hurt");
	static int attack1State = Animator.StringToHash("Base Layer.Attack1");
	static int attack2State = Animator.StringToHash("Base Layer.Attack2");
	static int attack3State = Animator.StringToHash("Base Layer.Attack3");
	static int attack4State = Animator.StringToHash("Base Layer.Attack4");
	static int blockState = Animator.StringToHash("Base Layer.Block");

	// Use this for initialization
	void Start () {
		
		rb = GetComponent<Rigidbody>();
		currentSprite = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();
		facingRight = true;
		canMove = true;
		walkMoveSpeed = moveSpeed;
		//standPos = transform.position;
	}

	private void Update()
	{
		curStateInfo = animator.GetCurrentAnimatorStateInfo(0);
		currentState = curStateInfo.fullPathHash;
		

		if (currentState == idleState)
			Debug.Log("Idle State");
		if (currentState == walkState)
			Debug.Log("Walk State");
		if (currentState == jumpState)
			Debug.Log("Jump State");
		if (currentState == tauntState)
			Debug.Log("Taunt State");
		if (currentState == hurtState)
			Debug.Log("Hurt State");
		if (currentState == attack1State)
			Debug.Log("Attack1 State");
		if (currentState == attack2State)
			Debug.Log("Attack2 State");
		if (currentState == attack3State)
			Debug.Log("Attack3 State");
		if (currentState == attack4State)
			Debug.Log("Attack4 State");
		if (currentState == blockState)
			Debug.Log("Block State");


		//---Control Speed based on commands-----------------

		if (currentState == idleState || currentState == walkState)
		{
			walkMoveSpeed = moveSpeed;
		}
		else
		{
			walkMoveSpeed = attackMovementSpeed;
		}
		
	}
    // Update is called once per frame
    void FixedUpdate()
    {


        //-------MOVEMENT---------------------------------
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveX, 0.0f, moveZ);
        rb.velocity = movement * walkMoveSpeed;
        rb.position = new Vector3(
                                    Mathf.Clamp(rb.position.x, xMin, xMax),
                                    transform.position.y,
                                    Mathf.Clamp(rb.position.z, zMin, zMax));

        animator.SetFloat("Speed", movement.sqrMagnitude);

        if (moveX < 0 && facingRight && canMove == true)
        {
            Flip();
        }
        else if (moveX > 0 && !facingRight && canMove == true)
        {
            Flip();
        }

        //------------COMBO ATTACK------------------------
        if (Input.GetMouseButton(0))
        {
            animator.SetBool("Attack", true);
        }
        else
        {
            animator.SetBool("Attack", false);
        }

        if (attack1SpriteHitFrame == currentSprite.sprite)
        {
            attack1Box.gameObject.SetActive(true);
        }
        else if (attack2SpriteHitFrame == currentSprite.sprite)
        {
            attack2Box.gameObject.SetActive(true);
        }
        else if (attack3SpriteHitFrame == currentSprite.sprite)
        {
            attack3Box.gameObject.SetActive(true);
        }
        else
        {
            attack1Box.gameObject.SetActive(false);
            attack2Box.gameObject.SetActive(false);
            attack3Box.gameObject.SetActive(false);
        }

        //---BLOCK--------------------
        if (Input.GetMouseButton(2))
        {
			
            animator.SetBool("Block", true);
            isBlocking = true;
        }
        else
        {
            animator.SetBool("Block", false);
            isBlocking = false;
        }

        //---TEST HURT-----------------------------
        if (Input.GetKeyDown(KeyCode.Q))
        {
            animator.SetBool("IsHit", true);
        }
        else { animator.SetBool("IsHit", false); }

        //----KNOCKED DOWN--------------------------
        if (Input.GetKeyDown(KeyCode.E))
        {

            StartCoroutine(KnockedDown());

        }
    }

		//------JUMP-------------------------------
		

	 IEnumerator KnockedDown()
	{
		float initY = transform.position.y;
		Vector3 newPos = new Vector3(transform.position.x, -.25f, transform.position.z);
		animator.Play("Fall");
		transform.position = newPos;
		canMove = false;

		if (facingRight == false)
		{
			rb.AddForce(transform.right * knockBackForce);
			//transform.position.y = -0.013;
		}
		else if (facingRight == true)
		{
			rb.AddForce(transform.right * (-1 * knockBackForce));
		}
		yield return new WaitForSeconds(knockedDownTime);
		animator.Play("Idle");
		canMove = true;
		
		
	}

	 void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
