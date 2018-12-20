using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[Range(0f, 10f)]
	public float moveSpeed;
	public float xMin, xMax, zMin, zMax;


	private Rigidbody rb;
	private Animator animator;
	private bool facingRight;


	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		animator = GetComponent<Animator>();
		facingRight = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		float moveX = Input.GetAxis("Horizontal");
		float moveZ = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3(moveX, 0.0f, moveZ);
		rb.velocity = movement * moveSpeed;
		rb.position = new Vector3(
									Mathf.Clamp(rb.position.x, xMin, xMax),
									transform.position.y,
									Mathf.Clamp(rb.position.z, zMin, zMax));

		animator.SetFloat("Speed", movement.sqrMagnitude);

		if (moveX < 0 && facingRight)
		{
			Flip();
		}
		else if (moveX > 0 && !facingRight)
		{
			Flip();
		}
	}

	private void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
