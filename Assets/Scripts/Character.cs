using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

	public float maxSpeed = 3f;
	bool facingRight = true;

	Animator anim;

	private bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;
	public float jumpForce;

	public Action actionOne;
	public bool canActionOneInAir;
	public Action actionTwo;
	public bool canActionTwoInAir;

	private bool doLeft;
	private bool doRight;
	private bool doJump;
	private bool doActionOne;
	private bool doActionTwo;


	// Use this for initialization
	void Start () 
	{
		anim = GetComponentInChildren<Animator>();
	}

	// Update is called once per frame
	void FixedUpdate () 
	{

		grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool("Ground", grounded);

		anim.SetFloat("vertVelocity", rigidbody2D.velocity.y);

		float move = 0;
		if (doLeft) 
		{ 
			move = -1; 
		} else if (doRight) {
			move = 1;
		}

		anim.SetFloat("Speed", Mathf.Abs (move));
		rigidbody2D.velocity = new Vector2(move * maxSpeed, rigidbody2D.velocity.y);

		if (move > 0 && !facingRight)
			Flip ();
		else if (move < 0 && facingRight)
			Flip ();

		doLeft = false;
		doRight = false;
	}

	void Update () 
	{
		if (grounded && doJump) 
		{
			anim.SetBool("Ground", false);
			anim.SetTrigger("Jump");
			rigidbody2D.AddForce(new Vector2(0, jumpForce));
		}



		if (doActionOne)
		{
			if (canActionOneInAir || grounded)
			{
				anim.SetTrigger("ActionOne");
				actionOne.performAction();
			}
		}

		if (doActionTwo)
		{
			if (canActionTwoInAir || grounded)
			{
				anim.SetTrigger("ActionTwo");
				actionTwo.performAction();
			}
		}

		doJump = false;
		doActionOne = false;
		doActionTwo = false;
	}

	public bool FacingRight ()
	{
		return facingRight;
	}

	public void Flip () 
	{
		facingRight = !facingRight;
		Vector3 theScale = anim.gameObject.transform.localScale;
		theScale.x *= -1;
		anim.gameObject.transform.localScale = theScale;
	}

	public void MoveLeft()
	{
		doLeft = true;
	}

	public void MoveRight()
	{
		doRight = true;
	}

	public bool Grounded() {
		return grounded;
	}

	public void Jump()
	{
		doJump = true;
	}

	public void Attack()
	{
		doActionOne = true;
	}

	public void TriggerAction1()
	{
		doActionOne = true;
	}

	public void TriggerAction2()
	{
		doActionTwo = true;
	}
}
