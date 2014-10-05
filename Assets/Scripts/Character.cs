using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

	public float maxSpeed = 3f;
    public float speedChange = 1f;
	bool facingRight = true;

	Animator anim;

	public bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.01f;
	public LayerMask whatIsGround;
	public float jumpForce;
    
    private float nextMove;
    private float curSpeed;
	private bool doJump;
	private bool doAttack;


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

		anim.SetFloat("vSpeed", rigidbody2D.velocity.y);
        
        //		float move = Input.GetAxis("Horizontal");
        float move = 0;
        move = nextMove;
		anim.SetFloat("Speed", Mathf.Abs (move));
        rigidbody2D.AddForce(new Vector2(move*speedChange*10, 0));
        float velx = Mathf.Clamp(rigidbody2D.velocity.x, -maxSpeed, maxSpeed);
        rigidbody2D.velocity = new Vector2(velx, rigidbody2D.velocity.y); 

		if (move > 0 && !facingRight)
			Flip ();
		else if (move < 0 && facingRight)
			Flip ();
	}

	void Update () 
	{
		if (grounded && doJump) 
		{
			anim.SetBool("Ground", false);
			rigidbody2D.AddForce(new Vector2(0, jumpForce));
		}

		doJump = false;
	}

	void Flip () 
	{
		facingRight = !facingRight;
		Vector3 theScale = anim.gameObject.transform.localScale;
		theScale.x *= -1;
		anim.gameObject.transform.localScale = theScale;
	}

	public void SetMove(float vel)
	{
        nextMove = vel;
	}

	public void Jump()
	{
		doJump = true;
	}

	public void Attack()
	{
		doAttack = true;
	}
}
