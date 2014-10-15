using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

	public float maxSpeed = 3f;
	bool facingRight = true;

	Animator anim;

	public bool flying = false;
	public float flyingAccel = 0.1f;

	public bool canJump = true;
	private bool grounded = false;
	public Transform groundCheckLeft;
	public Transform groundCheckRight;
	float groundRadius = 0.02f;
	public LayerMask whatIsGround;
	public float jumpForce;

	public Action actionOne;
	public bool canActionOneInAir;
	public float actionOneCooldown;
	private float actionOneTimeLeft;

	public Action actionTwo;
	public bool canActionTwoInAir;
	public float actionTwoCooldown;
	private float actionTwoTimeLeft;
	
	private bool doLeft;
	private bool doRight;
	private bool doJump;
	private bool doActionOne;
	private bool doActionTwo;

	private bool isDead;


	// Use this for initialization
	void Start () 
	{
		anim = GetComponentInChildren<Animator>();
	}

	// Update is called once per frame
	void FixedUpdate () 
	{
		if(isDead)
			return;

		if (canJump) {
			grounded = (Physics2D.OverlapCircle(groundCheckLeft.position, groundRadius, whatIsGround) ||
						Physics2D.OverlapCircle(groundCheckRight.position, groundRadius, whatIsGround));
			anim.SetBool("Ground", grounded);
		}

		anim.SetFloat("vertVelocity", rigidbody2D.velocity.y);

		float move = 0;
		if (doLeft) 
		{ 
			doLeft = false;
			move = -1; 
		} else if (doRight) {
			doRight = false;
			move = 1;
		}

		anim.SetFloat("Speed", Mathf.Abs (move));
		if (flying) {
			float newSpeed = rigidbody2D.velocity.x + flyingAccel * move;
			if (Mathf.Abs(newSpeed) <= maxSpeed)
				rigidbody2D.velocity = new Vector2(newSpeed, rigidbody2D.velocity.y);
		} else
			rigidbody2D.velocity = new Vector2(move * maxSpeed, rigidbody2D.velocity.y);

		if (facingRight ? move < 0 : move > 0)
			Flip ();
	}

	void Update () 
	{
		if(isDead)
			return;

		actionOneTimeLeft = Mathf.Max(0, actionOneTimeLeft - Time.deltaTime);
		actionTwoTimeLeft = Mathf.Max(0, actionTwoTimeLeft - Time.deltaTime);
		
		if (canJump && grounded && doJump) 
		{
			doJump = false;
			anim.SetBool("Ground", false);
			anim.SetTrigger("Jump");
			rigidbody2D.AddForce(new Vector2(0, jumpForce));
		}
		doJump = false;

		if (doActionOne)
		{
			doActionOne = false;
			if ((actionOneTimeLeft <= 0f) && (canActionOneInAir || grounded))
			{
				anim.SetTrigger("ActionOne");
				actionOne.performAction();
				actionOneTimeLeft = actionOneCooldown;
			}
		}

		if (doActionTwo)
		{
			doActionTwo = false;
			if ((actionTwoTimeLeft <= 0f) && (canActionTwoInAir || grounded))
			{
				anim.SetTrigger("ActionTwo");
				actionTwo.performAction();
				actionTwoTimeLeft = actionOneCooldown;
			}
		}
	}

	public bool FacingRight ()
	{
		return facingRight;
	}

	public void Flip () 
	{
		if (isDead)
			return;

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

	public void Kill()
	{
		isDead = true;
		anim.SetTrigger("Die");
		Invoke("Fade",30);
	}
	
	public void Fade (){
		anim.SetTrigger("Fade");
		Invoke("myDestroy",15);
	}
	
	public void myDestroy (){
		Destroy(gameObject);
	}

}
