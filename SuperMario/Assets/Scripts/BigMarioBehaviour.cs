using UnityEngine;
using System.Collections;

public class BigMarioBehaviour : MonoBehaviour {
	float speed = 0.9f;
	public float jumpingPower = 70;
	public KeyCode runButton;
	Animator anim;
	Rigidbody2D rb;
	bool facingRight = true;
	bool flipped;
	bool notJumping;
	float moveDir;
	// state 2 = large
	private int state = 2;
	
	public Sprite deadSprite;
	
	private GoombaBehaviour goombaBehave;
	
	void Awake () {
		anim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody2D> ();
	}
	void Update(){
		notJumping = rb.velocity.y == 0;
		if (Input.GetButtonDown ("Jump") && notJumping) {
			rb.AddForce (new Vector2(0,jumpingPower));
		}
	}
	void FixedUpdate () {
		moveDir = Input.GetAxis("Horizontal");
		if (notJumping) {
			CheckForMoveDirection (moveDir);
		}
		rb.velocity = new Vector2 (moveDir * speed,rb.velocity.y);
		float moving = Mathf.Abs (moveDir);
		if (notJumping && (moving > 0.1f)) {
			anim.SetFloat ("isWalking", moving);
		}
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
				Destroy(source);
			}
		} else if (other.gameObject.tag == "PowerUp") {
			UpTheAnte();
		}
	}
	void KillPlayer(){
		Debug.Log("killing player");
			state = 1;
	}
	// mario has 3 states in this game small,big,fire -> 1,2,3
	void UpTheAnte(){
		Debug.Log("needs input for fireMario");
	}
	public int getState(){
		return state;
	}
	public void setDefaultState(){
		this.state = 2;
	}
}
