using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {
	
	public float maxSpeed = 10f;
	public float jump = 500f;
	bool facingRight = true;

	Animator anim;

	bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;
	private Rigidbody2D rb2D;


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		rb2D = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool ("Ground", grounded);

		float move = Input.GetAxis ("Horizontal");
		
		anim.SetFloat ("speed", Mathf.Abs (move));

		rb2D.velocity = new Vector2 (move * maxSpeed, rb2D.velocity.y);

		if (move > 0 && !facingRight)
			Flip ();
		else if (move < 0 && facingRight)
			Flip ();
	}

	void Update()
	{
		if (grounded && Input.GetKeyDown(KeyCode.Space))
		{
			rb2D.AddForce(new Vector2(0,jump));
		}
	}

	void Flip ()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
