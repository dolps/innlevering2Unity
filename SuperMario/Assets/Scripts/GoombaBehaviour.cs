using UnityEngine;
using System.Collections;

public class GoombaBehaviour : MonoBehaviour {
	public Sprite deadSprite;
	private SpriteRenderer spriteRender;
	private float speed = -0.01f;
	private bool alive = true;
	private BoxCollider2D box;
	private Animator anim;

	// Use this for initialization
	void Start () {
		spriteRender = GetComponent <SpriteRenderer>();
		box = GetComponent<BoxCollider2D> ();
		anim = GetComponent <Animator>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (alive) {
			transform.position = new Vector2 (transform.position.x + speed, transform.position.y);
		}
	}
	void OnCollisionEnter2D(Collision2D other){
		speed = speed * -1;
	}
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player") {
			KillEnemy();
		}
	}
	void KillEnemy(){
		alive = false;
		anim.enabled = false;
		Rigidbody2D rb = GetComponent<Rigidbody2D> ();
		rb.mass = 1000;
		spriteRender.sprite = deadSprite;
		GetComponent<EdgeCollider2D> ().enabled = true;
		box.size = new Vector2 (0.3f, 0.3f);
		Invoke("DestroyEnemy",1);
	}
	public bool Isalive(){
		return alive;
	}
	void DestroyEnemy(){
		Destroy(gameObject);
		Destroy(this);
	}
}
