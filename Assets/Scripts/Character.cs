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

	public int currentStamina;
	public int MaxStamina;
	public int StaminaPerTick;

	public Action[] actions;
	public bool[] canActionInAir;
	public int[] actionCost;
	public float[] actionCoolDown;
	private float[] actionTimeLeft;
	
	private bool doLeft;
	private bool doRight;
	private bool doJump;
	private bool[] doAction;

	private bool isDead;

	// Use this for initialization
	void Start () 
	{
		currentStamina = MaxStamina;
		anim = GetComponentInChildren<Animator>();
		doAction = new bool[actions.Length];
		actionTimeLeft = new float[actions.Length];
		if ((actions.Length != canActionInAir.Length) ||
						(actions.Length != actionCost.Length) ||
						(actions.Length != actionCoolDown.Length)) 
		{
			Debug.LogError(gameObject.ToString() + " action arrays are unequal length");
		}
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

		addStamina (StaminaPerTick);
	}

	void Update () 
	{
		if(isDead)
			return;

		tickActionTime();
		
		if (canJump && grounded && doJump) 
		{
			doJump = false;
			anim.SetBool("Ground", false);
			anim.SetTrigger("Jump");
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.y, jumpForce);
		}

		doJump = false;

		for (int i = 0; i < doAction.Length; i++) {
			if (doAction[i])
			{
				performAction(i);
			}
		}

		resetActionFlags ();
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

	public void TriggerAction1()
	{
		doAction [0] = true;
	}

	public void TriggerAction2()
	{
		doAction [1] = true;
	}

	public void TriggerAction(int number)
	{
		doAction [number] = true;
	}

	public void Kill()
	{
		isDead = true;
		anim.SetTrigger("Die");
		Invoke("Fade",30);
	}
	
	public void Fade (){
		anim.SetTrigger("Fade");
		Invoke("myDestroy",5);
	}
	
	public void myDestroy (){
		Destroy(gameObject);
	}

	public void addStamina(int amount)
	{
		currentStamina = Mathf.Min (MaxStamina, currentStamina + amount);
	}

	private void resetActionFlags() 
	{
		for (int i = 0; i < doAction.Length; i++) {
			doAction[i] = false;
		}
	}

	private void tickActionTime()
	{
		for (int i = 0; i < actionTimeLeft.Length; i++) 
		{
			actionTimeLeft [i] = Mathf.Max (0, actionTimeLeft [i] - Time.deltaTime);
		}
	}

	private void performAction(int n)
	{
		doAction[n] = false;
		if ((actionTimeLeft [n] <= 0f) && 
			(canActionInAir [n] || grounded) &&
			(currentStamina >= actionCost [n])) 
		{
			anim.SetTrigger ("Action" + n);
			actions [n].performAction ();
			actionTimeLeft [n] = actionCoolDown [n];
			currentStamina = Mathf.Max(0, currentStamina - actionCost[n]);
		}
	}
}
