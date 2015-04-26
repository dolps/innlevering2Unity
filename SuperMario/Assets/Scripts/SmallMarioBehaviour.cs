using UnityEngine;
using System.Collections;

public class SmallMarioBehaviour : MonoBehaviour {
	float stdSpeed = 0.9f;
	float walkSpeed = 0.9f;
	public KeyCode runButton;
	float runningSpeed = 1.3f;
	bool walking;
	bool idle;
	public float jumpingPower = 70;
	Animator anim;
	Rigidbody2D rb;
	bool facingRight = true;
	bool flipped;
	bool notJumping;
	float moveDir;
	private int state = 1;
	
	public Sprite deadSprite;
	private SpriteRenderer spriteRenderer;
	
	private GoombaBehaviour goombaBehave;
	void Awake () {
		anim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody2D> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}
	void Update(){
		moveDir = Input.GetAxis ("Horizontal");
		notJumping = rb.velocity.y == 0;
		if (Input.GetKey (runButton) && notJumping && Mathf.Abs(moveDir) > 0) {
			stdSpeed = runningSpeed;
		} else {
			stdSpeed = walkSpeed;
		}
		if (Input.GetButtonDown ("Jump") && notJumping) {
			rb.AddForce (new Vector2(0,jumpingPower));
		}
		DoAnimation ();
	}
	void DoAnimation(){
		if (notJumping) {
			CheckForMoveDirection (moveDir);
			// hvis farten er større enn abs 0.9 da løper vi
			anim.SetFloat ("isWalking", Mathf.Abs (moveDir * stdSpeed));
		} else {
			anim.SetFloat("isWalking",0);
		}
	}
	void FixedUpdate () {
		rb.velocity = new Vector2 (moveDir * stdSpeed,rb.velocity.y);
	}
	void CheckForMoveDirection(float move){
		if (move < 0) {
			if(facingRight && !flipped)
				FlipPlayer();
			flipped = true;
		}
		if (move > 0 && flipped) {
			FlipPlayer();
			flipped = false;
		}
	}
	
	void FlipPlayer(){
		transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
	}
	void OnCollisionEnter2D(Collision2D other){
		GameObject source = other.gameObject;
		if (source.tag == "Enemy") {
			goombaBehave = source.GetComponent<GoombaBehaviour>();

			if(goombaBehave.Isalive()){
				KillPlayer();
			}
		} else if (other.gameObject.tag == "PowerUp") {
			UpTheAnte();
		}
	}
	void KillPlayer(){
		this.anim.enabled = false;
		this.spriteRenderer.sprite = deadSprite;
		rb.AddForce (new Vector2 (0, 100));
	}
	// mario has 3 states in this game small,big,fire -> 1,2,3
	void UpTheAnte(){
		state = 2;

	}
	public int getState(){
		return state;
	}
	public void setDefaultState(){
		this.state = 1;
	}
}
