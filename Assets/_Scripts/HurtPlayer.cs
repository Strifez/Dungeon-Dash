using UnityEngine;
using System.Collections;

public class HurtPlayer : MonoBehaviour {

	public SpriteRenderer spriteRenderer;
	public Rigidbody2D rigidbody2D;

	// Use this for initialization
	void Start () {
		//this.rigidbody2D = gameObject.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D otherCollider){
		if (otherCollider.gameObject.CompareTag ("Player")) {
			Destroy (spriteRenderer);
			Destroy (rigidbody2D);
		}
	}
}
