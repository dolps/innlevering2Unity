using UnityEngine;
using System.Collections;

public class QuestionBoxHit : MonoBehaviour {
	public GameObject player;
	public Sprite deadSprite;
	public GameObject powerPrefab;
	public int powersInstantiated = 0;
	private SpriteRenderer spriteRenderer;
	Animation qBoxAnim;
	// Use this for initialization
	void Start () {
		player = GetComponent<GameObject> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
	}
	void OnTriggerEnter2D(Collider2D other){
		spriteRenderer.sprite = deadSprite;
		if (powersInstantiated < 1) {
			Instantiate (powerPrefab, new Vector2(transform.position.x,transform.position.y+0.01f),transform.rotation);
			powersInstantiated++;
		}
	}
}
