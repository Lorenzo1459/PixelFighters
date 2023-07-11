using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour {

	public PhotonView view;

	public CharacterController2D controller;
	public Animator animator;

	public float runSpeed = 40f;

	float horizontalMove = 0f;
	bool jump = false;
	bool dash = false;

	//bool dashAxis = false;

	public void Start()
	{
		view = GetComponent<PhotonView>();
	}

	// Update is called once per frame
	void Update () {
		if(view.IsMine){

			horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
			if(horizontalMove != 0){
				animator.SetBool("IsMoving", true);
			}
			else if(horizontalMove == 0){				
				animator.SetBool("IsMoving", false);			
			}

			//animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

			if (Input.GetKeyDown(KeyCode.UpArrow))
			{
				jump = true;
			}

			if (Input.GetKeyDown(KeyCode.C))
			{
				dash = true;
			}

			/*if (Input.GetAxisRaw("Dash") == 1 || Input.GetAxisRaw("Dash") == -1) //RT in Unity 2017 = -1, RT in Unity 2019 = 1
			{
				if (dashAxis == false)
				{
					dashAxis = true;
					dash = true;
				}
			}
			else
			{
				dashAxis = false;
			}
			*/
		}


	}

	public void OnFall()
	{
		animator.SetBool("IsJumping", true);
	}

	public void OnLanding()
	{
		animator.SetBool("IsJumping", false);
	}

	void FixedUpdate ()
	{
		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, jump, dash);
		jump = false;
		dash = false;
	}
}
